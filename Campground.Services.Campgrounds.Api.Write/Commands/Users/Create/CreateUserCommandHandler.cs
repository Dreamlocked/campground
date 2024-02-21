
using AutoMapper;
using Campground.Services.Campgrounds.Api.Write.Utils;
using Campground.Services.Campgrounds.Domain.Entities;
using Campground.Services.Campgrounds.Domain.Utils;
using Campground.Services.Campgrounds.Infrastructure.Data.Unit_of_Work;
using Campground.Services.Campgrounds.Infrastructure.Queue.Models;
using MediatR;

namespace Campground.Services.Campgrounds.Api.Write.Commands.Users.Create
{
    internal sealed class CreateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, MessageSender messageSender) : IRequestHandler<CreateUserCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly MessageSender _messageSender = messageSender;

        public async Task<Unit> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(command);

            user.Password = Encript.GetSHA256Hash(command.Password + user.Salt);

            try
            {
                await _messageSender.SendEmailMessage(new Email()
                {
                    To = user.Email!,
                    Subject = "Welcome to Campground",
                    Body = "Welcome to Campground! We are glad to have you as a member of our community."
                });
            }
            catch(Exception ex) { Console.WriteLine(ex.Message); }

            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }
}
