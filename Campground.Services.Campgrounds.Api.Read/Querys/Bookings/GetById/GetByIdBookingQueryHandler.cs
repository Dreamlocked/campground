using AutoMapper;
using Campground.Services.Campgrounds.Api.Read.Querys.Bookings.Common;
using Campground.Services.Campgrounds.Api.Read.Querys.Users.Common;
using Campground.Services.Campgrounds.Api.Read.Querys.Users.GetAll;
using Campground.Services.Campgrounds.Infrastructure.Data.Unit_of_Work;
using MediatR;

namespace Campground.Services.Campgrounds.Api.Read.Querys.Bookings.GetById
{
    internal sealed class GetByIdBookingQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetByIdBookingQuery, BookingResponse>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        public async Task<BookingResponse> Handle(GetByIdBookingQuery query, CancellationToken cancellationToken)
        {
            var booking = await _unitOfWork.BookingRepository.GetByIdWithDetails(query.Id);

            return _mapper.Map<BookingResponse>(booking);

        }
    }
}
