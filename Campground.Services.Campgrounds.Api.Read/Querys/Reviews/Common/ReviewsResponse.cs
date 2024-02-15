﻿using Campground.Services.Campgrounds.Api.Read.Querys.Users.Common;

namespace Campground.Services.Campgrounds.Api.Read.Querys.Reviews.Common
{
    public record ReviewsResponse(
        Guid Id,
        string Comment,
        int Rating,
        DateTime CreateAt,
        UserResponse User
        );
}
