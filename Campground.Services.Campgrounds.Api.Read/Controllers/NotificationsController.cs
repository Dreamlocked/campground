using Campground.Services.Campgrounds.Api.Read.Querys.Campgrounds.GetById;
using Campground.Services.Campgrounds.Api.Read.Querys.Notifications.GetAllByIdUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Campground.Services.Campgrounds.Api.Read.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController(ISender mediator) : ControllerBase
    {
        private readonly ISender _mediator = mediator;

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var campground = await _mediator.Send(new GetAllByIdUserNotificationsQuery(id));
            return Ok(campground);
        }
    }
}
