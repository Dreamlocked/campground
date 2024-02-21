using Campground.Services.Campgrounds.Api.Read.Querys.Reviews.Common;
using Campground.Services.Campgrounds.Api.Read.Querys.Users.Common;

namespace Campground.Services.Campgrounds.Api.Read.Querys.Campgrounds.Common
{
    public class CampgroundResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public decimal PricePerNight { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime CreateAt { get; set; }
        public UserResponse Host { get; set; }
        public List<ImagesResponse> Images { get; set; }
        public List<ReviewsResponse> Reviews { get; set; }
    }


}
