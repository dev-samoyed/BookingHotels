using BookingHotels.DAL.EF;
using BookingHotels.DAL.Entities;
using BookingHotels.DAL.Interfaces;
using BookingHotels.DAL.Repositories;

namespace UserStore.DAL.Repositories
{
    public class ClientManager : IClientManager
    {
        public MyIdentityDbContext Database { get; set; }
        public ClientManager(MyIdentityDbContext db)
        {
            Database = db;
        }

        public void Create(ClientProfile item)
        {
            Database.ClientProfiles.Add(item);
            Database.SaveChanges();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}