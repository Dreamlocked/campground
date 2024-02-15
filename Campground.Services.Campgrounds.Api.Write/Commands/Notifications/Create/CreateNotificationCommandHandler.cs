using AutoMapper;
using Campground.Services.Campgrounds.Api.Write.Commands.Campgrounds.Delete;
using Campground.Services.Campgrounds.Domain.Entities;
using Campground.Services.Campgrounds.Infrastructure.Data.Unit_of_Work;
using MediatR;

namespace Campground.Services.Campgrounds.Api.Write.Commands.Notifications.Create
{
    internal sealed class CreateNotificationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateNotificationCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        public async Task<Unit> Handle(CreateNotificationCommand command, CancellationToken cancellationToken)
        {
            var notification = _mapper.Map<Notification>(command);

            await _unitOfWork.NotificationRepository.AddAsync(notification);
            await _unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }
}
