using BookingHotels.BLL.DTO;
namespace BookingHotels.BLL.Interfaces
{
    public interface IBookingService
    {
        void CreateBooking(BookingDTO bookingDto);
        void Dispose();
    }
}

