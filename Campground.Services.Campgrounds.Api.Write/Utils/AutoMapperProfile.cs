﻿using AutoMapper;
using Campground.Services.Campgrounds.Api.Write.Commands.Bookings.Create;
using Campground.Services.Campgrounds.Api.Write.Commands.Bookings.Update;
using Campground.Services.Campgrounds.Api.Write.Commands.Campgrounds.Create;
using Campground.Services.Campgrounds.Api.Write.Commands.Campgrounds.Update;
using Campground.Services.Campgrounds.Api.Write.Commands.Users.Create;
using Campground.Services.Campgrounds.Domain.Entities;
using Campground.Services.Campgrounds.Domain.Utils;
using System.Security.Claims;

namespace Campground.Services.Campgrounds.Api.Write.Utils
{
    public class AutoMapperProfile :Profile
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AutoMapperProfile(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            CreateMap<CreateCampgroundCommand, Domain.Entities.Campground>()
                .ForMember(dest => dest.HostId, opt => opt.MapFrom(src => GetUserId()))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.Images, opt => opt.Ignore());

            CreateMap<UpdateCampgroundCommand, Domain.Entities.Campground>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Images, opt => opt.Ignore());

            CreateMap<CreateUserCommand, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.Salt, opt => opt.MapFrom(src => Encript.GenerateSalt()));

            CreateMap<CreateBookingCommand, Booking>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => GetUserId()))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));

            CreateMap<UpdateBookingCommand, Booking>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }

        private Guid GetUserId() => Guid.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }
}
