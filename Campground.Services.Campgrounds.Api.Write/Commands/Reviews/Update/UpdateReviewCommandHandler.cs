using AutoMapper;
using Campground.Services.Campgrounds.Domain.Entities;
using Campground.Services.Campgrounds.Infrastructure.Data.Unit_of_Work;
using MediatR;

namespace Campground.Services.Campgrounds.Api.Write.Commands.Reviews.Update
{
    internal sealed class UpdateReviewCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateReviewCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        public async Task<Unit> Handle(UpdateReviewCommand command, CancellationToken cancellationToken)
        {
            var review = _mapper.Map<Review>(command);

            await _unitOfWork.ReviewRepository.UpdateAsync(review);
            await _unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }
}
