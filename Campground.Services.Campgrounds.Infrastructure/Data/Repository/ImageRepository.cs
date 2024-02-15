using Campground.Services.Campgrounds.Domain.Entities;
using Campground.Services.Campgrounds.Infrastructure.Data.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campground.Services.Campgrounds.Infrastructure.Data.Repository
{
    public class ImageRepository(CampgroundContext dbContext) : BaseRepository<Image>(dbContext) { }
}
