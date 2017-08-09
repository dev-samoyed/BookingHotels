using BookingHotels.DAL.EF;
using BookingHotels.Domain.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Threading.Tasks;
using BookingHotels.Domain.Identity;
using BookingHotels.Domain.Entities;
using BookingHotels.DAL.Identity;

namespace BookingHotels.DAL.Repositories
{
    public class IdentityUnitOfWork : IUnitOfWorkIdentity
    {
        private MyDbContext context;

        private ApplicationUserManager userManager;
        private ApplicationRoleManager roleManager;

        public IdentityUnitOfWork(string connectionString)
        {
            context = new MyDbContext(connectionString);
            userManager = new ApplicationUserManager(new CustomUserStore(context));
            roleManager = new ApplicationRoleManager(new CustomRoleStore(context));
        }

        public ApplicationUserManager UserManager
        {
            get { return userManager; }
        }


        public ApplicationRoleManager RoleManager
        {
            get { return roleManager; }
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
                    userManager.Dispose();
                    roleManager.Dispose();
                }
                this.disposed = true;
            }
        }
    }
}