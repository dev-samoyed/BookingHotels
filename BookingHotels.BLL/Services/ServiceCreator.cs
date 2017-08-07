using BookingHotels.BLL.Interfaces;
using BookingHotels.BLL.Services;
using BookingHotels.DAL.Repositories;

namespace BookingHotels.BLL.Services
{
    public class ServiceCreator : IServiceCreator
    {
        public IUserService CreateUserService(string connection)
        {
            return new UserService(new IdentityUnitOfWork(connection));
        }
    }
}