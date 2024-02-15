using AutoMapper;
using Campground.Services.Campgrounds.Api.Read.Querys.Users.Common;
using Campground.Services.Campgrounds.Infrastructure.Data.Repository.Base;
using Campground.Services.Campgrounds.Infrastructure.Data.Unit_of_Work;
using MediatR;

namespace Campground.Services.Campgrounds.Api.Read.Querys.Users.GetAll
{
    internal sealed class GetAllUsersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllUsersQuery, IReadOnlyList<UserResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<IReadOnlyList<UserResponse>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
        {
            var users = await _unitOfWork.UserRepository.GetAllAsync();

            return users.Select(_mapper.Map<UserResponse>).ToList();
        }
    }
}
