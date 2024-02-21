using AutoMapper;
using Campground.Services.Campgrounds.Domain.Entities;
using Campground.Services.Campgrounds.Infrastructure.Data.Unit_of_Work;
using MediatR;
using System.Security.Claims;

namespace Campground.Services.Campgrounds.Api.Write.Commands.Bookings.Create
{
    internal sealed class CreateBookingCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : IRequestHandler<CreateBookingCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public async Task<Guid> Handle(CreateBookingCommand command, CancellationToken cancellationToken)
        {
            var booking = _mapper.Map<Booking>(command);
            booking.UserId = Guid.Parse(_httpContextAccessor.HttpContext!.User.Claims.FirstOrDefault(c => c.Type == "Id")!.Value);

            await _unitOfWork.BookingRepository.AddAsync(booking);
            await _unitOfWork.CompleteAsync();

            return booking.Id;
        }
    }
}
