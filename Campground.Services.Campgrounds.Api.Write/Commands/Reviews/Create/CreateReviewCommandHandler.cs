using AutoMapper;
using Campground.Services.Campgrounds.Api.Write.Commands.Notifications.Create;
using Campground.Services.Campgrounds.Domain.Entities;
using Campground.Services.Campgrounds.Infrastructure.Data.Unit_of_Work;
using MediatR;

namespace Campground.Services.Campgrounds.Api.Write.Commands.Reviews.Create
{
    internal sealed class CreateReviewCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateReviewCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        public async Task<Unit> Handle(CreateReviewCommand command, CancellationToken cancellationToken)
        {
            var review = _mapper.Map<CreateReviewCommand, Review>(command);

            await _unitOfWork.ReviewRepository.AddAsync(review);
            await _unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }
}
