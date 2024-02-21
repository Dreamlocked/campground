using Campground.Services.Campgrounds.Api.Read.Querys.Bookings.GetAllBookings;
using Campground.Services.Campgrounds.Api.Read.Querys.Bookings.GetById;
using Campground.Services.Campgrounds.Api.Read.Querys.Campgrounds.GetAll;
using Campground.Services.Campgrounds.Api.Read.Querys.Campgrounds.GetById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Campground.Services.Campgrounds.Api.Read.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController(ISender mediator) : ControllerBase
    {
        private readonly ISender _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bookings = await _mediator.Send(new GetAllBookingQuery());
            return Ok(bookings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var booking = await _mediator.Send(new GetByIdBookingQuery(id));
            return Ok(booking);
        }
    }
}
