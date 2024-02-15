using Campground.Services.Campgrounds.Api.Read.Querys.Notifications.Common;
using Campground.Services.Campgrounds.Api.Read.Querys.Reviews.Common;
using MediatR;

namespace Campground.Services.Campgrounds.Api.Read.Querys.Notifications.GetAllByIdUser
{
    public record GetAllByIdUserNotificationsQuery(Guid UserId) : IRequest<IReadOnlyList<NotificationsResponse>>;
}
