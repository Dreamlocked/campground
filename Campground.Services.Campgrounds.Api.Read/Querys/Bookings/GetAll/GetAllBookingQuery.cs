using Campground.Services.Campgrounds.Api.Read.Querys.Bookings.Common;
using MediatR;

namespace Campground.Services.Campgrounds.Api.Read.Querys.Bookings.GetAllBookings
{
    public record GetAllBookingQuery() : IRequest<IReadOnlyList<BookingResponse>>;
}
