using System;
using System.Collections.Generic;
using System.Linq;
using BookingHotels.Domain.Entities;
using BookingHotels.DAL.EF;
using BookingHotels.Domain.DALInterfaces;
using System.Data.Entity;
 
namespace BookingHotels.DAL.Repositories
{
    public class BookingRepository : IRepository<Booking>
    {
        private MyDbContext db;

        public BookingRepository(MyDbContext context)
        {
            this.db = context;
        }

        public IEnumerable<Booking> GetAll()
        {
            return db.Bookings;
        }

        public Booking Get(Guid? id)
        {
            return db.Bookings.Find(id);
        }

        public void Create(Booking booking)
        {
            db.Bookings.Add(booking);
        }

        public void Update(Booking booking)
        {
            db.Entry(booking).State = EntityState.Modified;
        }

        public IEnumerable<Booking> Find(Func<Booking, Boolean> predicate)
        {
            return db.Bookings.Where(predicate).ToList();
        }

        public void Delete(Guid id)
        {
            Hotel hotel = db.Hotels.Find(id);
            if (hotel != null)
                db.Hotels.Remove(hotel);
        }
    }
}