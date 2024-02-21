using Campground.Services.Billings.Domain.Entities;
using Campground.Shared.Communication.AzureServiceBus;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campground.Services.Billings.Infrastructure.HandlerMessage
{
    public class MessageSender(AzureServiceBusHandler serviceBusHandler, IConfiguration configuration)
    {
        private readonly AzureServiceBusHandler _serviceBusHandler = serviceBusHandler;
        private readonly IConfiguration _configuration = configuration;

        public async Task SendBillingMessage(Billing message)
        {
            await _serviceBusHandler.SendMessageAsync(_configuration.GetSection("Queue:Billing").Value!, message);
        }
    }
}
