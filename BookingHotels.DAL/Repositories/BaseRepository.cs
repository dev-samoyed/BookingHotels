using BookingHotels.DAL.EF;
using BookingHotels.Domain.Interfaces;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;

namespace BookingHotels.DAL.Repositories
{
    class BaseRepository<T> : IRepository<T> where T : class
    {
        private MyDbContext Context;
        // Declare generic DbSet<T> (which may be Hotels, Rooms or other DbSet from MyDbContext)
        private DbSet<T> DbSet;

        public BaseRepository(MyDbContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return DbSet;
        }

        public T Get(Guid? id)
        {
            return DbSet.Find(id);
        }

        public void Create(T entity)
        {
            DbSet.Add(entity);
        }

        public void Update(T entity)
        {
            var entityEntry = Context.Entry(entity);
            if (entityEntry.State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
                entityEntry.State = EntityState.Modified;
        }

        public IEnumerable<T> SearchFor(Func<T, Boolean> predicate)
        {
            return GetAll().Where(predicate);
        }

        public void Delete(Guid id)
        {
            T entity = DbSet.Find(id);
            if (entity != null)
                DbSet.Remove(entity);
        }
    }
}