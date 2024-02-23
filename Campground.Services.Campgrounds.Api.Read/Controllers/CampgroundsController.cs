using Campground.Services.Campgrounds.Api.Read.Querys.Campgrounds.GetAll;
using Campground.Services.Campgrounds.Api.Read.Querys.Campgrounds.GetById;
using Campground.Services.Campgrounds.Api.Read.Querys.Users.GetAll;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Campground.Services.Campgrounds.Api.Read.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CampgroundsController(ISender mediator) : ControllerBase
    {
        private readonly ISender _mediator = mediator;

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var campgrounds = await _mediator.Send(new GetAllCampgroundQuery());
            return Ok(campgrounds);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(Guid id)
        {
            var campground = await _mediator.Send(new GetByIdCampgroundQuery(id));
            return Ok(campground);
        }
    }
}
