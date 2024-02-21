using AutoMapper;
using Campground.Services.Campgrounds.Api.Read.Querys.Bookings.Common;
using Campground.Services.Campgrounds.Api.Read.Querys.Campgrounds.Common;
using Campground.Services.Campgrounds.Api.Read.Querys.Notifications.Common;
using Campground.Services.Campgrounds.Api.Read.Querys.Reviews.Common;
using Campground.Services.Campgrounds.Api.Read.Querys.Users.Common;
using Campground.Services.Campgrounds.Domain.Entities;

namespace Campground.Services.Campgrounds.Api.Read.Utils
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserResponse>();
            CreateMap<Review, ReviewsResponse>();
            CreateMap<Image, ImagesResponse>();

            CreateMap<Booking, ReviewsResponse>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));

            CreateMap<Domain.Entities.Campground, CampgroundResponse>()
                .ForMember(dest => dest.Reviews, opt => opt.MapFrom(src => src.Bookings.Select(b => b.Review)));

            CreateMap<Booking, BookingResponse>();

            CreateMap<Domain.Entities.Campground, CampgroundsResponse>()
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Bookings.Select(b => b.Review.Rating).DefaultIfEmpty(0).Average()));

            CreateMap<Review, ReviewsResponse>()
                .ForPath(dest => dest.User, opt => opt.MapFrom(src => src.Booking.User));

            CreateMap<Notification, NotificationsResponse>();

        }
    }
}
