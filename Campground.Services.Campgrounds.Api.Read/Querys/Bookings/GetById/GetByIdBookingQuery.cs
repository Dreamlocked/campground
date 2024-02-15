using Campground.Services.Campgrounds.Api.Read.Querys.Bookings.Common;
using Campground.Services.Campgrounds.Api.Read.Querys.Users.Common;
using MediatR;

namespace Campground.Services.Campgrounds.Api.Read.Querys.Bookings.GetById
{
    public record GetByIdBookingQuery(Guid Id) : IRequest<BookingResponse>;
}
