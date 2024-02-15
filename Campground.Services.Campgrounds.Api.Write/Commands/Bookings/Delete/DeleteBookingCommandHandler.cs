using AutoMapper;
using Campground.Services.Campgrounds.Infrastructure.Data.Unit_of_Work;
using MediatR;

namespace Campground.Services.Campgrounds.Api.Write.Commands.Bookings.Delete
{
    internal sealed class DeleteBookingCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<DeleteBookingCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        public async Task<Unit> Handle(DeleteBookingCommand command, CancellationToken cancellationToken)
        {
            await _unitOfWork.BookingRepository.DeleteByIdAsync(command.BookingId);
            await _unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }
}
