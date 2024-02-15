using AutoMapper;
using Campground.Services.Campgrounds.Api.Read.Querys.Notifications.Common;
using Campground.Services.Campgrounds.Api.Read.Querys.Reviews.Common;
using Campground.Services.Campgrounds.Api.Read.Querys.Reviews.GetAll;
using Campground.Services.Campgrounds.Infrastructure.Data.Unit_of_Work;
using MediatR;

namespace Campground.Services.Campgrounds.Api.Read.Querys.Notifications.GetAllByIdUser
{
    internal sealed class GetAllByIdUserNotificationsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllByIdUserNotificationsQuery, IReadOnlyList<NotificationsResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        public async Task<IReadOnlyList<NotificationsResponse>> Handle(GetAllByIdUserNotificationsQuery query, CancellationToken cancellationToken)
        {
            var notifications = await _unitOfWork.NotificationRepository.GetManyAsync(n => n.UserId == query.UserId);
            return notifications.Select(_mapper.Map<NotificationsResponse>).ToList();
        }
    }
}
