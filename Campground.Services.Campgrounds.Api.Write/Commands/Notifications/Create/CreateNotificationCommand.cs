using MediatR;

namespace Campground.Services.Campgrounds.Api.Write.Commands.Notifications.Create
{
    public record CreateNotificationCommand(
        Guid UserId,
        string Message
        ) : IRequest<Unit>;
}
