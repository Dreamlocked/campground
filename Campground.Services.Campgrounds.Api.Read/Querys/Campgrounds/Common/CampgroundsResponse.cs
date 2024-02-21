namespace Campground.Services.Campgrounds.Api.Read.Querys.Campgrounds.Common
{
    public class CampgroundsResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal PricePerNight { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime? CreateAt { get; set; }
        public decimal Rating { get; set; }
        public List<ImagesResponse> Images { get; set; }
    }
}
