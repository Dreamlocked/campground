using AutoMapper;
using Campground.Services.Campgrounds.Api.Read.Querys.Bookings.Common;
using Campground.Services.Campgrounds.Api.Read.Querys.Bookings.GetById;
using Campground.Services.Campgrounds.Api.Read.Querys.Campgrounds.Common;
using Campground.Services.Campgrounds.Api.Read.Querys.Reviews.Common;
using Campground.Services.Campgrounds.Infrastructure.Data.Unit_of_Work;
using MediatR;

namespace Campground.Services.Campgrounds.Api.Read.Querys.Reviews.GetAll
{
    internal sealed class GetAllByCampgroundIdReviewQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllByCampgroundIdReviewQuery, IReadOnlyList<ReviewsResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<IReadOnlyList<ReviewsResponse>> Handle(GetAllByCampgroundIdReviewQuery query, CancellationToken cancellationToken)
        {
            var reviews = await _unitOfWork.ReviewRepository.GetAllByCampgroundId(query.CampgroundId);
            return reviews.Select(_mapper.Map<ReviewsResponse>).ToList();
        }
    }
}
