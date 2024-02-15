using Campground.Services.Campgrounds.Api.Write.Commands.Campgrounds.Delete;
using Campground.Services.Campgrounds.Infrastructure.Data.Unit_of_Work;
using MediatR;

namespace Campground.Services.Campgrounds.Api.Write.Commands.Reviews.Delete
{
    internal sealed class DeleteReviewCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteCampgroundCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Unit> Handle(DeleteCampgroundCommand command, CancellationToken cancellationToken)
        {
            await _unitOfWork.ReviewRepository.DeleteByIdAsync(command.Id);
            await _unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }
}
