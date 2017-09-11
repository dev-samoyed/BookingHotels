using System;
using BookingHotels.DAL.EF;
using BookingHotels.Domain.Interfaces;
using BookingHotels.Domain.Entities;
using BookingHotels.Domain.Identity;
using BookingHotels.DAL.Identity;
using System.Threading.Tasks;

namespace BookingHotels.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private BaseRepository<Hotel> hotelRepository;
        private BaseRepository<Room> roomRepository;
        private BaseRepository<Feedback> feedbackRepository;
        private BaseRepository<Booking> bookingRepository;
        private BaseRepository<RoomImage> roomImageRepository;
        private ApplicationUserManager applicationUserManager;
        private ApplicationRoleManager applicationRoleManager;
        private MyDbContext context;
        public UnitOfWork(string connectionString)
        {
            context = new MyDbContext(connectionString);
            applicationUserManager = new ApplicationUserManager(new CustomUserStore(context));
            applicationRoleManager = new ApplicationRoleManager(new CustomRoleStore(context));
        }
        public IRepository<Hotel> Hotels
        {
            get
            {
                if (hotelRepository == null)
                    hotelRepository = new BaseRepository<Hotel>(context);
                return hotelRepository;
            }
        }
        public IRepository<Room> Rooms
        {
            get
            {
                if (roomRepository == null)
                    roomRepository = new BaseRepository<Room>(context);
                return roomRepository;
            }
        }
        public IRepository<Feedback> Feedbacks
        {
            get
            {
                if (feedbackRepository == null)
                    feedbackRepository = new BaseRepository<Feedback>(context);
                return feedbackRepository;
            }
        }
        public IRepository<Booking> Bookings
        {
            get
            {
                if (bookingRepository == null)
                    bookingRepository = new BaseRepository<Booking>(context);
                return bookingRepository;
            }
        }
        public IRepository<RoomImage> RoomImages
        {
            get
            {
                if (roomImageRepository == null)
                    roomImageRepository = new BaseRepository<RoomImage>(context);
                return roomImageRepository;
            }
        }
        public ApplicationUserManager ApplicationUserManager
        {
            get { return applicationUserManager; }
        }


        public ApplicationRoleManager ApplicationRoleManager
        {
            get { return applicationRoleManager; }
        }

        // Save
        public void Save()
        {
            context.SaveChanges();
        }
        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
        // Dispose
        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                this.disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}