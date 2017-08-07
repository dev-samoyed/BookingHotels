using BookingHotels.DAL.Repositories;
using System;

namespace BookingHotels.DAL.Interfaces
{
    // User profiles manager interface
    public interface IClientManager : IDisposable
    {
        void Create(ClientProfile item);
    }
}
