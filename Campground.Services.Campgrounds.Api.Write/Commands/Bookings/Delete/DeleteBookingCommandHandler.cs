using AutoMapper;
using Campground.Services.Campgrounds.Infrastructure.Data.Unit_of_Work;
using MediatR;

namespace Campground.Services.Campgrounds.Api.Write.Commands.Bookings.Delete
{
    internal sealed class DeleteBookingCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteBookingCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<Unit> Handle(DeleteBookingCommand command, CancellationToken cancellationToken)
        {
            await _unitOfWork.BookingRepository.DeleteByIdAsync(command.Id);
            await _unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }
}
