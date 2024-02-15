using AutoMapper;
using Campground.Services.Campgrounds.Api.Read.Querys.Bookings.Common;
using Campground.Services.Campgrounds.Api.Read.Querys.Campgrounds.Common;
using Campground.Services.Campgrounds.Api.Read.Querys.Notifications.Common;
using Campground.Services.Campgrounds.Api.Read.Querys.Reviews.Common;
using Campground.Services.Campgrounds.Api.Read.Querys.Users.Common;
using Campground.Services.Campgrounds.Domain.Entities;

namespace Campground.Services.Campgrounds.Api.Write.Utils
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<User, UserResponse>();
            CreateMap<Review, ReviewsResponse>();

            CreateMap<Domain.Entities.Campground, CampgroundResponse>()
                .ForMember(dest => dest.Reviews, opt => opt.MapFrom(src => src.Bookings.Select(b => b.Review)))
                .ForMember(dest => dest.Reviews.User, opt => opt.MapFrom(src => src.Bookings.Select(b => b.User)));

            CreateMap<Booking, BookingResponse>();

            CreateMap<Domain.Entities.Campground, CampgroundsResponse>()
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Bookings.Select(b => b.Review.Rating).Average()));

            CreateMap<Review, ReviewsResponse>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.Booking.User));

            CreateMap<Notification, NotificationsResponse>();

        }
    }
}
