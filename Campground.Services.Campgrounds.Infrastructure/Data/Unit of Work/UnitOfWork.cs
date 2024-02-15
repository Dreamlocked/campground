using Campground.Services.Campgrounds.Infrastructure.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campground.Services.Campgrounds.Infrastructure.Data.Unit_of_Work
{
    public class UnitOfWork(CampgroundContext dbContext) : IUnitOfWork
    {
        private readonly CampgroundContext _dbContext = dbContext;
        private CampgroundRepository? _campgroundRepository;
        private UserRepository? _userRepository;
        private ImageRepository? _imageRepository;
        private ReviewRepository? _reviewRepository;
        private NotificationRepository? _notificationRepository;
        private BookingRepository? _bookingRepository;

        public CampgroundRepository CampgroundRepository => _campgroundRepository ??= new CampgroundRepository(_dbContext);
        public UserRepository UserRepository => _userRepository ??= new UserRepository(_dbContext);
        public ImageRepository ImageRepository => _imageRepository ??= new ImageRepository(_dbContext);
        public ReviewRepository ReviewRepository => _reviewRepository ??= new ReviewRepository(_dbContext);
        public NotificationRepository NotificationRepository => _notificationRepository ??= new NotificationRepository(_dbContext);
        public BookingRepository BookingRepository => _bookingRepository ??= new BookingRepository(_dbContext);

        public async Task CompleteAsync() => await _dbContext.SaveChangesAsync();

        private bool _disposed;

        public async ValueTask DisposeAsync()
        {
            await DisposeAsync(true);

            GC.SuppressFinalize(this);
        }

        protected virtual async ValueTask DisposeAsync(bool disposing)
        {
            if(!_disposed)
            {
                if(disposing)
                {
                    await _dbContext.DisposeAsync();
                }
                _disposed = true;
            }
        }
    }
}
