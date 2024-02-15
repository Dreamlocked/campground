using Campground.Services.Campgrounds.Api.Read.Querys.Reviews.Common;
using MediatR;

namespace Campground.Services.Campgrounds.Api.Read.Querys.Reviews.GetAll
{
    public record GetAllByCampgroundIdReviewQuery(Guid CampgroundId) : IRequest<IReadOnlyList<ReviewsResponse>>;
}
