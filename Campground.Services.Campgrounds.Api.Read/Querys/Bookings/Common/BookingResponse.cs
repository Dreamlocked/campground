using Campground.Services.Campgrounds.Api.Read.Querys.Campgrounds.Common;
using Campground.Services.Campgrounds.Api.Read.Querys.Users.Common;

namespace Campground.Services.Campgrounds.Api.Read.Querys.Bookings.Common
{
    public class BookingResponse
    {
        public Guid Id { get; set; }
        public UserResponse User { get; set; }
        public CampgroundsResponse Campground { get; set; }
        public bool Attended { get; set; }
        public bool Paid { get; set; }
        public DateTime ArrivingDate { get; set; }
        public DateTime LeavingDate { get; set; }
        public DateTime CreateAt { get; set; }
    }

}
