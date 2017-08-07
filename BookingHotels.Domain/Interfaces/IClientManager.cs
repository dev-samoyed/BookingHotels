using BookingHotels.Domain.Repositories;
using System;

namespace BookingHotels.Domain.Interfaces
{
    // User profiles manager interface
    public interface IClientManager : IDisposable
    {
        void Create(ClientProfile item);
    }
}
