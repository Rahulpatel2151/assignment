using HMS.DAL;
using HMS.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HMS.BAL
{
    public class HotelsManager : IHotelsManager
    {
        IHotelsRepository _hotelsRepository;
        public HotelsManager(IHotelsRepository hotelsRepository)
        {
            _hotelsRepository = hotelsRepository;
        }

        public RoomsModel GetRoom(int id)
        {
            return _hotelsRepository.GetRoom(id);
        }

        public IEnumerable<RoomsModel> GetRooms()
        {
            return _hotelsRepository.GetRooms();
        }

        public string AddHotel(HotelsModel hotelsModel)
        {
            return _hotelsRepository.AddHotel(hotelsModel);
        }

        public IEnumerable<HotelsModel> GetHotels()
        {
            return _hotelsRepository.GetHotels();
        }

        public HotelsModel GetHotel(int id)
        {
            return _hotelsRepository.GetHotel(id);
        }

        public string AddRoom(RoomsModel roomsModel)
        {
            return _hotelsRepository.AddRoom(roomsModel);
        }

        public bool IsAvailable(DateTime date,int id)
        {
            return _hotelsRepository.IsAvailable(date,id);
        }
        public IEnumerable<BookingsModel> GetBookings()
        {
            return _hotelsRepository.GetBookings();
        }

        public string BookRoom(BookingsModel bookingsModel)
        {
            return _hotelsRepository.BookRoom(bookingsModel);
        }

        public string ChangeBookingDate(int bookingId, DateTime date)
        {
            return _hotelsRepository.ChangeBookingDate(bookingId, date);
        }

        public string ChangeBookingStatus(int bookingId, string status)
        {
            return _hotelsRepository.ChangeBookingStatus(bookingId, status);
        }

        public string DeleteBooking(int id)
        {
            return _hotelsRepository.DeleteBooking(id);
        }

        public BookingsModel GetBooking(int id)
        {
            return _hotelsRepository.GetBooking(id);
        }

        public IEnumerable<RoomsModel> GetFilteredRooms(string city, string pincode, string category, decimal price)
        {
            return _hotelsRepository.GetFilteredRooms(city, pincode, category, price);
        }

        public string EditHotel(int id,HotelsModel hotelsModel)
        {
            return _hotelsRepository.EditHotel(id,hotelsModel);
        }
        public string EditRoom(int id, RoomsModel roomsModel)
        {
            return _hotelsRepository.EditRoom(id, roomsModel);
        }
    }
}
