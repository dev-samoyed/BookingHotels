using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingHotels.DAL.Entities;
using BookingHotels.Domain.DALInterfaces;

namespace BookingHotels.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Hotel> Hotels { get; }
        IRepository<Room> Rooms { get; }
        IRepository<Feedback> Feedbacks { get; }
        IRepository<Booking> Bookings { get; }
        
        void Save();
    }
}

