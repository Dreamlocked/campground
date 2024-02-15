using MediatR;

namespace Campground.Services.Campgrounds.Api.Write.Commands.Bookings.Create
{
    public record CreateBookingCommand(
        Guid CampgroundId,
        DateTime ArrivingDate,
        DateTime LeavingDate
    ) : IRequest<Unit>;
}
