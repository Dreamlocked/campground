using MediatR;

namespace Campground.Services.Campgrounds.Api.Write.Commands.Bookings.Update
{
    public record UpdateBookingCommand(
        Guid Id,
        DateTime ArrivingDate,
        DateTime LeavingDate
    ) : IRequest<Unit>;
}
