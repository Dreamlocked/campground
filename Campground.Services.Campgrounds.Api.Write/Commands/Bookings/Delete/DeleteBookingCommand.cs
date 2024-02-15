using MediatR;

namespace Campground.Services.Campgrounds.Api.Write.Commands.Bookings.Delete
{
    public record DeleteBookingCommand(Guid BookingId) : IRequest<Unit>;
}
