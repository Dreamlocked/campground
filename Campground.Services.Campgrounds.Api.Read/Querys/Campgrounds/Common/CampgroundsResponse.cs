namespace Campground.Services.Campgrounds.Api.Read.Querys.Campgrounds.Common
{
    public record CampgroundsResponse(
        Guid Id,
        string Title,
        decimal PricePerNight,
        string Description,
        string Location,
        DateTime? CreateAt,
        decimal Rating,
        List<ImagesResponse> Images
        );
}
