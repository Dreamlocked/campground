using Campground.Services.Campgrounds.Infrastructure.Queue.Models;
using Campground.Shared.Communication.AzureServiceBus;
using Campground.Shared.Communication.AzureServiceBus.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Campground.Services.Campgrounds.Api.Write.Utils
{
    public class MessageSender(AzureServiceBusHandler serviceBusHandler, IConfiguration configuration)
    {
        private readonly AzureServiceBusHandler _serviceBusHandler = serviceBusHandler;
        private readonly IConfiguration _configuration = configuration;
        public async Task SendEmailMessage(Email message)
        {
            await _serviceBusHandler.SendMessageAsync(_configuration.GetSection("Queue:Email").Value!, message);
        }
    }
}