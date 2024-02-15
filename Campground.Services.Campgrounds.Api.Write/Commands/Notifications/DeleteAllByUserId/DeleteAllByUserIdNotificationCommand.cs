using MediatR;

namespace Campground.Services.Campgrounds.Api.Write.Commands.Notifications.Delete
{
    public record DeleteAllByUserIdNotificationCommand(
        Guid UserId
        ) : IRequest<Unit>;
}
