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
    public class BookingRepository(CampgroundContext dbContext) : BaseRepository<Booking>(dbContext)
    {
        public async Task<Booking> GetByIdWithDetails(Guid id)
        {
            return await _dbContext.Bookings
                .Include(c => c.Campground)
                .Include(c => c.Review)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
