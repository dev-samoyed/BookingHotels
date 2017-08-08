using System;
using BookingHotels.DAL.EF;
using BookingHotels.Domain.Interfaces;
using BookingHotels.DAL.Entities;

namespace BookingHotels.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private BaseRepository<Hotel> hotelRepository;
        private BaseRepository<Room> roomRepository;
        private BaseRepository<Feedback> feedbackRepository;
        private BaseRepository<Booking> bookingRepository;

        private MyDbContext context;
        public EFUnitOfWork(string connectionString)
        {
            context = new MyDbContext(connectionString);
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
        // Save
        public void Save()
        {
            context.SaveChanges();
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