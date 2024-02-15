using MediatR;

namespace Campground.Services.Campgrounds.Api.Write.Commands.Notifications.Update
{
    public record UpdateViewedNotificationCommand(Guid Id) : IRequest<Unit>;
}
