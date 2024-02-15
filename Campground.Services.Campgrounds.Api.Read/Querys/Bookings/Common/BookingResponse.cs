using Campground.Services.Campgrounds.Api.Read.Querys.Campgrounds.Common;
using Campground.Services.Campgrounds.Api.Read.Querys.Users.Common;

namespace Campground.Services.Campgrounds.Api.Read.Querys.Bookings.Common
{
    public record BookingResponse(
        Guid Id,
        UserResponse User,
        CampgroundsResponse Campground,
        bool Attended,
        bool Paid,
        DateTime ArrivingDate,
        DateTime LeavingDate,
        DateTime CreateAt
    );
}
