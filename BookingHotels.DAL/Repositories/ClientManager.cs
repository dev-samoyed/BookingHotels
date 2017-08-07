using BookingHotels.DAL.EF;
using BookingHotels.Domain.Interfaces;
using BookingHotels.Domain.Repositories;

namespace BookingHotels.DAL.Repositories
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