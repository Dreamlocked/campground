using AutoMapper;
using Campground.Services.Campgrounds.Domain.Entities;
using Campground.Services.Campgrounds.Infrastructure.Data.Unit_of_Work;
using MediatR;
using System.Security.Claims;

namespace Campground.Services.Campgrounds.Api.Write.Commands.Bookings.Create
{
    internal sealed class CreateBookingCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateBookingCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Unit> Handle(CreateBookingCommand command, CancellationToken cancellationToken)
        {
            var booking = _mapper.Map<Booking>(command);

            await _unitOfWork.BookingRepository.AddAsync(booking);
            await _unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }
}
