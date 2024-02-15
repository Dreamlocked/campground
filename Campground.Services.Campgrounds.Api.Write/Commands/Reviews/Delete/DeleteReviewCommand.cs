using MediatR;

namespace Campground.Services.Campgrounds.Api.Write.Commands.Reviews.Delete
{
    public record DeleteReviewCommand(Guid Id) : IRequest<Unit>;
}
