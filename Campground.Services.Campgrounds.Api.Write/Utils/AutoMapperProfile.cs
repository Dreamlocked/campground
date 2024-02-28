using AutoMapper;
using Campground.Services.Campgrounds.Api.Write.Commands.Bookings.Create;
using Campground.Services.Campgrounds.Api.Write.Commands.Bookings.Update;
using Campground.Services.Campgrounds.Api.Write.Commands.Campgrounds.Create;
using Campground.Services.Campgrounds.Api.Write.Commands.Campgrounds.Update;
using Campground.Services.Campgrounds.Api.Write.Commands.Notifications.Create;
using Campground.Services.Campgrounds.Api.Write.Commands.Reviews.Create;
using Campground.Services.Campgrounds.Api.Write.Commands.Reviews.Update;
using Campground.Services.Campgrounds.Api.Write.Commands.Users.Create;
using Campground.Services.Campgrounds.Domain.Entities;
using Campground.Services.Campgrounds.Domain.Utils;
using System.Security.Claims;

namespace Campground.Services.Campgrounds.Api.Write.Utils
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateCampgroundCommand, Domain.Entities.Campground>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreateAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Images, opt => opt.Ignore());

            CreateMap<UpdateCampgroundCommand, Domain.Entities.Campground>()
                .ForMember(dest => dest.Images, opt => opt.Ignore());

            CreateMap<CreateUserCommand, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.Salt, opt => opt.MapFrom(src => Encript.GenerateSalt()));

            CreateMap<CreateBookingCommand, Booking>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));

            CreateMap<UpdateBookingCommand, Booking>();

            CreateMap<CreateNotificationCommand, Notification>()
                .ForMember(dest => dest.CreateAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Viewed, opt => opt.MapFrom(src => false));

            CreateMap<CreateReviewCommand, Review>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreateAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UpdateReviewCommand, Review>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

        }
    }
}
