using System;
using BookingHotels.Domain.Entities;

namespace BookingHotels.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Hotel> Hotels { get; }
        IRepository<Room> Rooms { get; }
        IRepository<Feedback> Feedbacks { get; }
        IRepository<Booking> Bookings { get; }
        IRepository<RoomImage> RoomImages { get; }
        
        void Save();
    }
}

