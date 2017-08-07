using System;
using System.Collections.Generic;
using System.Linq;
using BookingHotels.DAL.Entities;
using BookingHotels.DAL.EF;
using BookingHotels.DAL.DALInterfaces;
using System.Data.Entity;

namespace BookingHotels.DAL.Repositories
{
    public class HotelRepository : IRepository<Hotel>
    {
        private MyDbContext db;

        public HotelRepository(MyDbContext context)
        {
            this.db = context;
        }

        public IEnumerable<Hotel> GetAll()
        {
            return db.Hotels;
        }

        public Hotel Get(Guid? id)
        {
            return db.Hotels.Find(id);
        }

        public IEnumerable<Hotel> Find(Func<Hotel, Boolean> predicate)
        {
            return db.Hotels.Where(predicate).ToList();
        }

        public void Create(Hotel hotel)
        {
            db.Hotels.Add(hotel);
        }

        public void Update(Hotel hotel)
        {
            db.Entry(hotel).State = EntityState.Modified;
        }
        
        public void Delete(Guid id)
        {
            Hotel hotel = db.Hotels.Find(id);
            if (hotel != null)
                db.Hotels.Remove(hotel);
        }
    }
}