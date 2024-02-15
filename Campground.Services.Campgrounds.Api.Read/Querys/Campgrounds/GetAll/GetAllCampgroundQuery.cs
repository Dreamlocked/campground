using Campground.Services.Campgrounds.Api.Read.Querys.Campgrounds.Common;
using MediatR;

namespace Campground.Services.Campgrounds.Api.Read.Querys.Campgrounds.GetAll
{
    public record GetAllCampgroundQuery : IRequest<IReadOnlyList<CampgroundsResponse>>;
}
