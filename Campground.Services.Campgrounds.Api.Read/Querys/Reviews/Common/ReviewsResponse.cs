using Campground.Services.Campgrounds.Api.Read.Querys.Users.Common;

namespace Campground.Services.Campgrounds.Api.Read.Querys.Reviews.Common
{
    public class ReviewsResponse
    {
        public Guid Id { get; init; }
        public string Comment { get; init; }
        public int Rating { get; init; }
        public DateTime CreateAt { get; init; }
        public UserResponse User { get; init; }
    }
}
