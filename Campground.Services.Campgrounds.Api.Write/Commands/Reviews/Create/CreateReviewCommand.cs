using MediatR;

namespace Campground.Services.Campgrounds.Api.Write.Commands.Reviews.Create
{
    public record CreateReviewCommand(
        Guid BookingId,
        string Comment,
        int Rating
        ) : IRequest<Unit>;
}
