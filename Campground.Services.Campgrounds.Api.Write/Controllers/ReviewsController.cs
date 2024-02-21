using Campground.Services.Campgrounds.Api.Write.Commands.Bookings.Delete;
using Campground.Services.Campgrounds.Api.Write.Commands.Bookings.Update;
using Campground.Services.Campgrounds.Api.Write.Commands.Campgrounds.Create;
using Campground.Services.Campgrounds.Api.Write.Commands.Reviews.Create;
using Campground.Services.Campgrounds.Api.Write.Commands.Reviews.Delete;
using Campground.Services.Campgrounds.Api.Write.Commands.Reviews.Update;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Campground.Services.Campgrounds.Api.Write.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController(ISender mediator) : ControllerBase
    {
        private readonly ISender _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReviewCommand command)
        {
            var createResult = await _mediator.Send(command);
            return Ok(createResult);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateReviewCommand command)
        {
            if(command.Id != id) return BadRequest("Don't match command.Id and id url");
            var updateResult = await _mediator.Send(command);
            return Ok(updateResult);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, [FromBody] DeleteReviewCommand command)
        {
            if(command.Id != id) return BadRequest("Don't match command.Id and id url");
            var deleteResult = await _mediator.Send(command);
            return Ok(deleteResult);
        }
    }
}
