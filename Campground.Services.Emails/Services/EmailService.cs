﻿using Campground.Services.Emails.Models;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Azure.Messaging.ServiceBus;
using System.Text.Json;
using Campground.Shared.Communication.AzureServiceBus.Interfaces;
using Campground.Shared.Communication.AzureServiceBus;

namespace Campground.Services.Emails.Services
{
    public class EmailService(IConfiguration configuration, AzureServiceBusHandler serviceBusHandler) : IMessageHandler
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly AzureServiceBusHandler _serviceBusHandler = serviceBusHandler;

        public async Task HandleMessageAsync(string message)
        {
            var email = JsonSerializer.Deserialize<Email>(message)!;
            SendEmail(email);
        }

        public async Task SendMessageAsync<T>(T message)
        {
            await _serviceBusHandler.SendMessageAsync(_configuration.GetSection("Queue:Email").Value!, message);
        }

        public void SendEmail(Email request)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(Environment.GetEnvironmentVariable("EmailUser") ?? _configuration["Email:User"]));
                email.To.Add(MailboxAddress.Parse(request.To));
                email.Subject = request.Subject;
                email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = request.Body };

                using var smtp = new SmtpClient();
                smtp.Connect(Environment.GetEnvironmentVariable("EmailHost") ?? _configuration["Email:Host"], 587, SecureSocketOptions.StartTls);
                smtp.Authenticate(Environment.GetEnvironmentVariable("EmailUser") ?? _configuration["Email:User"], Environment.GetEnvironmentVariable("EmailPassword") ?? _configuration["Email:Password"]);

                smtp.Send(email);
                smtp.Disconnect(true);
            }
            catch(Exception)
            {
                Console.WriteLine("No pudo enviarse el correo");
            }
        }
    }
}
