using System;
using BookingHotels.DAL.EF;
using BookingHotels.Domain.DALInterfaces;
using BookingHotels.Domain.Entities;

namespace BookingHotels.DAL.Repositories
{
    // Using Unit of Work in Repository Pattern
    // todo: переделать в Generic repository
    public class EFUnitOfWork : IUnitOfWork
    {
        private MyDbContext db;
        private HotelRepository hotelRepository;
        private RoomRepository roomRepository;
        private FeedbackRepository feedbackRepository;
        private BookingRepository bookingRepository;

        public EFUnitOfWork(string connectionString)
        {
            db = new MyDbContext(connectionString);
        }

        public IRepository<Hotel> Hotels
        {
            get
            {
                if (hotelRepository == null)
                    hotelRepository = new HotelRepository(db);
                return hotelRepository;
            }
        }
        public IRepository<Room> Rooms
        {
            get
            {
                if (roomRepository == null)
                    roomRepository = new RoomRepository(db);
                return roomRepository;
            }
        }
        public IRepository<Feedback> Feedbacks
        {
            get
            {
                if (feedbackRepository == null)
                    feedbackRepository = new FeedbackRepository(db);
                return feedbackRepository;
            }
        }
        public IRepository<Booking> Bookings
        {
            get
            {
                if (bookingRepository == null)
                    bookingRepository = new BookingRepository(db);
                return bookingRepository;
            }
        }
        // Save
        public void Save()
        {
            db.SaveChanges();
        }
        // Dispose
        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
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