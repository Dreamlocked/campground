using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;


namespace Campground.Services.Campgrounds.Infrastructure.WebSockets
{
    public class MessageHub : Hub
    {
        public override async Task OnConnectedAsync()
        {

            await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId} has joined");
        }
    }
}
