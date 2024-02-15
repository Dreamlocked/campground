using AutoMapper;
using Campground.Services.Campgrounds.Api.Write.Commands.Bookings.Delete;
using Campground.Services.Campgrounds.Domain.Entities;
using Campground.Services.Campgrounds.Infrastructure.Data.Unit_of_Work;
using MediatR;

namespace Campground.Services.Campgrounds.Api.Write.Commands.Bookings.Update
{
    internal sealed class UpdateBookingCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateBookingCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        public async Task<Unit> Handle(UpdateBookingCommand command, CancellationToken cancellationToken)
        {
            var booking = _mapper.Map<Booking>(command);

            await _unitOfWork.BookingRepository.UpdateAsync(booking);
            await _unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }
}
