namespace Campground.Services.Campgrounds.Api.Read.Querys.Notifications.Common
{
    public record NotificationsResponse(
        Guid Id,
        string Message,
        DateTime CreateAt,
        bool Viewed
        );
}
