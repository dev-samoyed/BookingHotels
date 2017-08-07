using System;
using System.Collections.Generic;
using System.Linq;
using BookingHotels.DAL.Entities;
using BookingHotels.DAL.EF;
using BookingHotels.DAL.DALInterfaces;
using System.Data.Entity;
 
namespace BookingHotels.DAL.Repositories
{
    public class FeedbackRepository : IRepository<Feedback>
    {
        private MyDbContext db;

        public FeedbackRepository(MyDbContext context)
        {
            this.db = context;
        }

        public IEnumerable<Feedback> GetAll()
        {
            return db.Feedbacks;
        }

        public Feedback Get(Guid? id)
        {
            return db.Feedbacks.Find(id);
        }

        public void Create(Feedback feedback)
        {
            db.Feedbacks.Add(feedback);
        }

        public void Update(Feedback feedback)
        {
            db.Entry(feedback).State = EntityState.Modified;
        }

        public IEnumerable<Feedback> Find(Func<Feedback, Boolean> predicate)
        {
            return db.Feedbacks.Where(predicate).ToList();
        }

        public void Delete(Guid id)
        {
            Feedback feedback = db.Feedbacks.Find(id);
            if (feedback != null)
                db.Feedbacks.Remove(feedback);
        }
    }
}