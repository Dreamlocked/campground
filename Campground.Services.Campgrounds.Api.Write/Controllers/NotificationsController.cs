using Campground.Services.Campgrounds.Api.Write.Commands.Campgrounds.Delete;
using Campground.Services.Campgrounds.Api.Write.Commands.Campgrounds.Update;
using Campground.Services.Campgrounds.Api.Write.Commands.Notifications.Update;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Campground.Services.Campgrounds.Api.Write.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController(ISender mediator) : ControllerBase
    {
        private readonly ISender _mediator = mediator;

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateViewedNotificationCommand command)
        {
            if(command.Id != id) return BadRequest("Don't match command.Id and id url");
            var updateResult = await _mediator.Send(command);
            return Ok(updateResult);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, [FromBody] DeleteCampgroundCommand command)
        {
            if(command.Id != id) return BadRequest("Don't match command.Id and id url");
            var deleteResult = await _mediator.Send(command);
            return Ok(deleteResult);
        }

    }
}
