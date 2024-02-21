using Campground.Services.Campgrounds.Api.Write.Commands.Bookings.Create;
using Campground.Services.Campgrounds.Api.Write.Commands.Bookings.Delete;
using Campground.Services.Campgrounds.Api.Write.Commands.Bookings.Update;
using Campground.Services.Campgrounds.Api.Write.Commands.Notifications.Create;
using Campground.Services.Campgrounds.Infrastructure.WebSockets;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Campground.Services.Campgrounds.Api.Write.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController(ISender mediator, IHubContext<MessageHub> hubContext) : ControllerBase
    {
        private readonly ISender _mediator = mediator;
        private readonly IHubContext<MessageHub> _hubContext = hubContext;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookingCommand command)
        {
            var bookingId = await _mediator.Send(command);
            var hostId = await _mediator.Send(new CreateNotificationCommand(bookingId));
            await _hubContext.Clients.All.SendAsync("notification", bookingId);
            await _hubContext.Clients.User(hostId.ToString()).SendAsync("notification", bookingId);
            return Ok(bookingId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBookingCommand command)
        {
            if(command.Id != id) return BadRequest("Don't match command.Id and id url");
            var updateResult = await _mediator.Send(command);
            return Ok(updateResult);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, [FromBody] DeleteBookingCommand command)
        {
            if(command.Id != id) return BadRequest("Don't match command.Id and id url");
            var deleteResult = await _mediator.Send(command);
            return Ok(deleteResult);
        }
    }
}
