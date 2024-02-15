using AutoMapper;
using Campground.Services.Campgrounds.Api.Write.Commands.Notifications.Create;
using Campground.Services.Campgrounds.Infrastructure.Data.Unit_of_Work;
using MediatR;

namespace Campground.Services.Campgrounds.Api.Write.Commands.Notifications.Delete
{
    internal sealed class DeleteAllByUserIdNotificationCommandHanlder(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<DeleteAllByUserIdNotificationCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<Unit> Handle(DeleteAllByUserIdNotificationCommand command, CancellationToken cancellationToken)
        {
            await _unitOfWork.NotificationRepository.DeleteManyAsync(notification => notification.UserId == command.UserId);
            await _unitOfWork.CompleteAsync();
            return Unit.Value;
        }
    }
}
