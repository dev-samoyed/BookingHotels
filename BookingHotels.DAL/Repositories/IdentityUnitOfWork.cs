using BookingHotels.DAL.EF;
using BookingHotels.Domain.Interfaces;
using System;
using System.Threading.Tasks;
using BookingHotels.Domain.Identity;
using BookingHotels.DAL.Identity;

namespace BookingHotels.DAL.Repositories
{
    public class IdentityUnitOfWork : IUnitOfWorkIdentity
    {
        private MyDbContext context;
        private ApplicationUserManager applicationUserManager;
        private ApplicationRoleManager applicationRoleManager;

        public IdentityUnitOfWork(string connectionString)
        {
            context = new MyDbContext(connectionString);
            applicationUserManager = new ApplicationUserManager(new CustomUserStore(context));
            applicationRoleManager = new ApplicationRoleManager(new CustomRoleStore(context));
        }

        public ApplicationUserManager ApplicationUserManager
        {
            get { return applicationUserManager; }
        }


        public ApplicationRoleManager ApplicationRoleManager
        {
            get { return applicationRoleManager; }
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    applicationUserManager.Dispose();
                    applicationRoleManager.Dispose();
                }
                this.disposed = true;
            }
        }
    }
}