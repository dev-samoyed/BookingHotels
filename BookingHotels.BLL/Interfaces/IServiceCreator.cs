namespace BookingHotels.BLL.Interfaces
{
    public interface IServiceCreator
    {
        // абстрактная фабрика
        IUserService CreateUserService(string connection);
    }
}
