using System;
using BookingHotels.Domain.Entities;
using BookingHotels.Domain.Identity;
using System.Threading.Tasks;

namespace BookingHotels.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Hotel> Hotels { get; }
        IRepository<Room> Rooms { get; }
        IRepository<Feedback> Feedbacks { get; }
        IRepository<Booking> Bookings { get; }
        IRepository<RoomImage> RoomImages { get; }
        ApplicationUserManager ApplicationUserManager { get; }
        ApplicationRoleManager ApplicationRoleManager { get; }
        Task SaveAsync();
        void Save();
    }
}

