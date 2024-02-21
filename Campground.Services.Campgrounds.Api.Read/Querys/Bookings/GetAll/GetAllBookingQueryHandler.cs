using AutoMapper;
using Campground.Services.Campgrounds.Api.Read.Querys.Bookings.Common;
using Campground.Services.Campgrounds.Api.Read.Querys.Bookings.GetAllBookings;
using Campground.Services.Campgrounds.Api.Read.Querys.Bookings.GetById;
using Campground.Services.Campgrounds.Api.Read.Querys.Campgrounds.Common;
using Campground.Services.Campgrounds.Domain.Entities;
using Campground.Services.Campgrounds.Infrastructure.Data.Unit_of_Work;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Campground.Services.Campgrounds.Api.Read.Querys.Bookings.GetAll
{
    internal sealed class GetAllBookingQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllBookingQuery, IReadOnlyList<BookingResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<IReadOnlyList<BookingResponse>> Handle(GetAllBookingQuery request, CancellationToken cancellationToken)
        {
            var bookings = await _unitOfWork.BookingRepository.GetAllWithDetails();

            return bookings.Select(_mapper.Map<BookingResponse>).ToList();
        }
    }
}
