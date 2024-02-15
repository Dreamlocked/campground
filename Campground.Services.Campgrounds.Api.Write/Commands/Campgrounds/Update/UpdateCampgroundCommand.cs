using MediatR;

namespace Campground.Services.Campgrounds.Api.Write.Commands.Campgrounds.Update
{
    public record UpdateCampgroundCommand(
        Guid Id,
        string Title,
        decimal Latitude,
        decimal Longitude,
        decimal PricePerNight,
        string Description,
        string Location,
        List<Guid> actualImages,
        List<IFormFile> Images
        ) : IRequest<Unit>;
}
