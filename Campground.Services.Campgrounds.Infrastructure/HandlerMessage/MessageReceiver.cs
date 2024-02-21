using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campground.Shared.Communication.AzureServiceBus.Interfaces;
using Campground.Shared.Communication.AzureServiceBus;
using System.Text.Json;
using Campground.Services.Campgrounds.Infrastructure.Data.Repository;
using Campground.Services.Campgrounds.Infrastructure.HandlerMessage.Models;
using Campground.Services.Campgrounds.Infrastructure.Data.Unit_of_Work;
using Microsoft.Extensions.DependencyInjection;

namespace Campground.Services.Campgrounds.Infrastructure.HandlerMessage
{
    public class MessageReceiver(IServiceScopeFactory serviceScopeFactory) : IMessageHandler
    {
        private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;
        public async Task HandleMessageAsync(string message)
        {
            using(var scope = _serviceScopeFactory.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var billing = JsonSerializer.Deserialize<Billing>(message)!;
                var booking = await unitOfWork.BookingRepository.GetByIdAsync(Guid.Parse(billing.BookingId!));
                booking!.Paid = true;
                await unitOfWork.BookingRepository.UpdateAsync(booking);
                await unitOfWork.CompleteAsync();
            }
        }
    }
}
