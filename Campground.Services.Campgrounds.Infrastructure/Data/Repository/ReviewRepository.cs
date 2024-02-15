using Campground.Services.Campgrounds.Domain.Entities;
using Campground.Services.Campgrounds.Infrastructure.Data.Repository.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campground.Services.Campgrounds.Infrastructure.Data.Repository
{
    public class ReviewRepository(CampgroundContext dbContext) : BaseRepository<Review>(dbContext)
    {
        public async Task<List<Review>?> GetAllByCampgroundId(Guid campgroundId)
        {
            return await _dbContext.Reviews
                .Include(r => r.Booking)
                .ThenInclude(b => b.User)
                .Where(r => r.Booking.CampgroundId == campgroundId)
                .ToListAsync();
        }
    }
}
