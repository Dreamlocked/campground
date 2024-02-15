using System;
using System.Collections.Generic;
using System.Linq;
using Azure.Messaging.ServiceBus;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Campground.Shared.Communication.AzureServiceBus
{
    public class AzureServiceBusHandler
    {
        private readonly ServiceBusClient _client;
        private readonly IConfiguration _configuration;

        public AzureServiceBusHandler(IConfiguration configuration)
        {
            _configuration = configuration;
            string connectionString = _configuration.GetConnectionString("AzureServiceBus")!;
            _client = new ServiceBusClient(connectionString);
        }

        public ServiceBusProcessor CreateProcessor(string queueName)
        {
            return _client.CreateProcessor(queueName);
        }

        public void RegisterMessageHandler(ServiceBusProcessor processor, Func<ProcessMessageEventArgs, Task> messageHandler, 
            Func<ProcessErrorEventArgs, Task> errorHandler)
        {
            processor.ProcessMessageAsync += messageHandler;
            processor.ProcessErrorAsync += errorHandler;
        }

        public async Task SendMessageAsync(string queueName, string message)
        {
            ServiceBusSender sender = _client.CreateSender(queueName);
            await sender.SendMessageAsync(new ServiceBusMessage(message));
        }

        public async Task<string> ReceiveMessageAsync(string queueName)
        {
            ServiceBusReceiver receiver = _client.CreateReceiver(queueName);
            ServiceBusReceivedMessage receivedMessage = await receiver.ReceiveMessageAsync();
            return receivedMessage.Body.ToString();
        }
    }
}
