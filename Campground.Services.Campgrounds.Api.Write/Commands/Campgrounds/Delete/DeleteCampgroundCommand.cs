using MediatR;

namespace Campground.Services.Campgrounds.Api.Write.Commands.Campgrounds.Delete
{
    public record DeleteCampgroundCommand(Guid Id) : IRequest<Unit>;
}
