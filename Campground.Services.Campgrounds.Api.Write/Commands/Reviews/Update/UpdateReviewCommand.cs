using MediatR;

namespace Campground.Services.Campgrounds.Api.Write.Commands.Reviews.Update
{
    public record UpdateReviewCommand(
        Guid Id,
        string Comment,
        int Rating
        ) : IRequest<Unit>;
}
