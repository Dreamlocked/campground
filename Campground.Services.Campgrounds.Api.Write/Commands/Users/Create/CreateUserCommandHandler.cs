
using AutoMapper;
using Campground.Services.Campgrounds.Domain.Entities;
using Campground.Services.Campgrounds.Domain.Utils;
using Campground.Services.Campgrounds.Infrastructure.Data.Unit_of_Work;
using MediatR;

namespace Campground.Services.Campgrounds.Api.Write.Commands.Users.Create
{
    internal sealed class CreateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateUserCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        public async Task<Unit> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(command);

            user.Password = Encript.GetSHA256Hash(command.Password + user.Salt);

            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }
}
