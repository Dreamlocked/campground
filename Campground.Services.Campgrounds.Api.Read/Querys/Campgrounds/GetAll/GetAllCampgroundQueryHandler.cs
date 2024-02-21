using AutoMapper;
using Campground.Services.Campgrounds.Api.Read.Querys.Campgrounds.Common;
using Campground.Services.Campgrounds.Api.Read.Querys.Campgrounds.GetById;
using Campground.Services.Campgrounds.Api.Read.Querys.Users.Common;
using Campground.Services.Campgrounds.Infrastructure.Data.Unit_of_Work;
using MediatR;

namespace Campground.Services.Campgrounds.Api.Read.Querys.Campgrounds.GetAll
{
    internal sealed class GetAllCampgroundQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllCampgroundQuery, IReadOnlyList<CampgroundsResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        public async Task<IReadOnlyList<CampgroundsResponse>> Handle(GetAllCampgroundQuery request, CancellationToken cancellationToken)
        {
            var campgrounds = await _unitOfWork.CampgroundRepository.GetAllWithDetails();
            return campgrounds.Select(_mapper.Map<CampgroundsResponse>).ToList();
        }
    }
}
