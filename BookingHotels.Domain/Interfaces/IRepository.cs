using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingHotels.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(Guid? id);
        IEnumerable<T> SearchFor(Func<T, Boolean> predicate);
        void Create(T item);
        void Update(T item);
        void Delete(Guid id);
    }
}
