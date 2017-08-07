using System;
using System.Collections.Generic;
using System.Linq;
using BookingHotels.DAL.Entities;
using BookingHotels.DAL.EF;
using BookingHotels.DAL.DALInterfaces;
using System.Data.Entity;

namespace BookingHotels.DAL.Repositories
{
    public class RoomRepository : IRepository<Room>
    {
        private MyDbContext db;

        public RoomRepository(MyDbContext context)
        {
            this.db = context;
        }

        public IEnumerable<Room> GetAll()
        {
            return db.Rooms;
        }

        public Room Get(Guid? id)
        {
            return db.Rooms.Find(id);
        }

        public void Create(Room room)
        {
            db.Rooms.Add(room);
        }

        public void Update(Room room)
        {
            db.Entry(room).State = EntityState.Modified;
        }

        public IEnumerable<Room> Find(Func<Room, Boolean> predicate)
        {
            return db.Rooms.Where(predicate).ToList();
        }

        public void Delete(Guid id)
        {
            Room room = db.Rooms.Find(id);
            if (room != null)
                db.Rooms.Remove(room);
        }
    }
}