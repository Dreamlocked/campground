using Campground.Services.Campgrounds.Domain.Entities;
using MediatR;

namespace Campground.Services.Campgrounds.Api.Write.Commands.Bookings.Create
{
    public record CreateBookingCommand(
        Guid CampgroundId,
        DateTime ArrivingDate,
        DateTime LeavingDate
    ) : IRequest<Guid>;
}
