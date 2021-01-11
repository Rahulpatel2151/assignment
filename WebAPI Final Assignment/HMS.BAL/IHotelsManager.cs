using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Models.Models;
namespace HMS.BAL
{
    public interface IHotelsManager
    {
        IEnumerable<HotelsModel> GetHotels();
        IEnumerable<RoomsModel> GetRooms();
        IEnumerable<RoomsModel> GetFilteredRooms(string city,string pincode,string category,decimal price);
        IEnumerable<BookingsModel> GetBookings();
        HotelsModel GetHotel(int id);
        RoomsModel GetRoom(int id); 
        BookingsModel GetBooking(int id);
        string AddHotel(HotelsModel hotelsModel);
        string EditHotel(int id,HotelsModel hotelsModel);
        string AddRoom(RoomsModel roomsModel);
        string EditRoom(int id, RoomsModel roomsModel);
        string BookRoom(BookingsModel bookingsModel);
        bool IsAvailable(DateTime date, int id);
        string ChangeBookingDate(int bookingId, DateTime date);
        string ChangeBookingStatus(int bookingId, string status);
        string DeleteBooking(int id);
    }
}
