using Azure.Messaging.ServiceBus;
using Campground.Services.Emails.Models;
using Campground.Shared.Communication.AzureServiceBus;
using System.Net.Http.Json;
using System.Text.Json;

namespace Campground.Services.Emails.Services.Consumer
{
    public class MessageReceiverConsumer
    {
        private readonly AzureServiceBusHandler _serviceBusHandler;
        private readonly ServiceBusProcessor _processor;
        private readonly EmailService _emailService;

        public MessageReceiverConsumer(AzureServiceBusHandler serviceBusHandler, IConfiguration configuration, EmailService emailService)
        {
            _serviceBusHandler = serviceBusHandler;
            _processor = _serviceBusHandler.CreateProcessor(configuration.GetSection("Queues:Email").Value);
            _serviceBusHandler.RegisterMessageHandler(_processor, MessageHandler, ErrorHandler);
            _emailService = emailService;
        }

        public async Task RegisterMessageHandlerAsync()
        {
            _processor.ProcessMessageAsync += MessageHandler;
            _processor.ProcessErrorAsync += ErrorHandler;
            await _processor.StartProcessingAsync();
        }

        private async Task MessageHandler(ProcessMessageEventArgs args)
        {
            string messageBody = args.Message.Body.ToString();
            Console.WriteLine($"Received message: {messageBody}");

            // Aquí puedes agregar la lógica para ejecutar una acción cuando recibes un mensaje
            var email = JsonSerializer.Deserialize<Email>(messageBody)!;

            _emailService.SendEmail(email);

            // Completa el mensaje
            await args.CompleteMessageAsync(args.Message);
        }


        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine($"Message handler encountered an exception {args.Exception}.");
            return Task.CompletedTask;
        }
    }

}
