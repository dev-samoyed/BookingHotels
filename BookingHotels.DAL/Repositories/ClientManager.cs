using BookingHotels.DAL.EF;
using BookingHotels.Domain.Interfaces;
using BookingHotels.Domain.Repositories;

namespace BookingHotels.DAL.Repositories
{
    public class ClientManager : IClientManager
    {
        public MyDbContext context { get; set; }
        public ClientManager(MyDbContext db)
        {
            context = db;
        }

        public void Create(ClientProfile item)
        {
            context.ClientProfiles.Add(item);
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}