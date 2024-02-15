using AutoMapper;
using Campground.Services.Campgrounds.Api.Write.Commands.Bookings.Delete;
using Campground.Services.Campgrounds.Infrastructure.Data.Unit_of_Work;
using MediatR;

namespace Campground.Services.Campgrounds.Api.Write.Commands.Campgrounds.Delete
{
    internal sealed class DeleteCampgroundCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<DeleteCampgroundCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        public async Task<Unit> Handle(DeleteCampgroundCommand command, CancellationToken cancellationToken)
        {
            await _unitOfWork.CampgroundRepository.DeleteByIdAsync(command.CampgroundId);
            await _unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }
}
