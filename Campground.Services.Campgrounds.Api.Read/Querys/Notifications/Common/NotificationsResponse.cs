namespace Campground.Services.Campgrounds.Api.Read.Querys.Notifications.Common
{
    public class NotificationsResponse
    {
        public Guid Id { get; init; }
        public string Message { get; init; }
        public DateTime CreateAt { get; init; }
        public bool Viewed { get; init; }
    }
}
