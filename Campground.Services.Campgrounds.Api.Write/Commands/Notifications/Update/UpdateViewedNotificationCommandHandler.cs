using AutoMapper;
using Campground.Services.Campgrounds.Api.Write.Commands.Bookings.Update;
using Campground.Services.Campgrounds.Infrastructure.Data.Unit_of_Work;
using MediatR;

namespace Campground.Services.Campgrounds.Api.Write.Commands.Notifications.Update
{
    internal sealed class UpdateViewedNotificationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateViewedNotificationCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        public async Task<Unit> Handle(UpdateViewedNotificationCommand command, CancellationToken cancellationToken)
        {
            var notification = await _unitOfWork.NotificationRepository.GetByIdAsync(command.Id);
            notification.Viewed = true;

            await _unitOfWork.NotificationRepository.UpdateAsync(notification);
            await _unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }
}
