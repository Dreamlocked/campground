using AutoMapper;
using Campground.Services.Campgrounds.Api.Write.Commands.Campgrounds.Delete;
using Campground.Services.Campgrounds.Domain.Entities;
using Campground.Services.Campgrounds.Infrastructure.Data.Unit_of_Work;
using MediatR;

namespace Campground.Services.Campgrounds.Api.Write.Commands.Notifications.Create
{
    internal sealed class CreateNotificationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateNotificationCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        public async Task<Guid> Handle(CreateNotificationCommand command, CancellationToken cancellationToken)
        {
            var booking = await _unitOfWork.BookingRepository.GetByIdWithDetails(command.BookingId);

            var notification = new Notification()
            {
                Id = Guid.NewGuid(),
                UserId = booking.Campground.HostId,
                //BookingId = booking.Id,
                CreateAt = DateTime.Now,
                Message = $"Se ha reservado {booking.Campground.Title}",
                Viewed = false
            };

            await _unitOfWork.NotificationRepository.AddAsync(notification);
            await _unitOfWork.CompleteAsync();

            return notification.UserId;
        }
    }
}
