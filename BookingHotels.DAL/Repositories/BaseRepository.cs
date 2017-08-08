using BookingHotels.DAL.EF;
using BookingHotels.DAL.Entities;
using BookingHotels.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingHotels.DAL.Repositories
{
    class BaseRepository<T> : IRepository<T> where T : class
    {
        private MyDbContext Context;
        private DbSet<T> DbSet;



        public BaseRepository(MyDbContext context)
        {
            // context.Register(this);
            Context = context;
            DbSet = context.Set<T>;
        }

        public virtual IQueryable<T> GetAll()
        {
            return DbSet;
        }

        public IEnumerable<T> GetAll()
        {
           
            return db.;
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




public abstract class GenericRepository<C, T> :
    IGenericRepository<T> where T : class where C : DbContext, new()
{

    private C _entities = new C();
    public C Context
    {

        get { return _entities; }
        set { _entities = value; }
    }

    public virtual IQueryable<T> GetAll()
    {

        IQueryable<T> query = _entities.Set<T>();
        return query;
    }

    public IQueryable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
    {

        IQueryable<T> query = _entities.Set<T>().Where(predicate);
        return query;
    }

    public virtual void Add(T entity)
    {
        _entities.Set<T>().Add(entity);
    }

    public virtual void Delete(T entity)
    {
        _entities.Set<T>().Remove(entity);
    }

    public virtual void Edit(T entity)
    {
        _entities.Entry(entity).State = System.Data.EntityState.Modified;
    }

    public virtual void Save()
    {
        _entities.SaveChanges();
    }
}