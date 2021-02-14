using DealerManagement.DAL.Repository;
using DealerManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerManagement.BAL
{
    public class BookingManager : IBookingManager
    {
        IBookingRepository _bookingRepository;
        public BookingManager(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }
        public void AddBooking(string name, BookingView booking)
        {
            _bookingRepository.AddBooking(name, booking);
        }

        public void ChangeStatus(int? id, string status)
        {
            _bookingRepository.ChangeStatus(id,status);
        }

        public BookingView GetBooking(int? id)
        {
            return _bookingRepository.GetBooking(id);
        }

        public IEnumerable<BookingView> GetBookings(string username)
        {
            return _bookingRepository.GetBookings(username);
        }

        public IEnumerable<BookingView> GetBookings()
        {
            return _bookingRepository.GetBookings();
        }

        public void RemoveBooking(int id)
        {
            _bookingRepository.RemoveBooking(id);
        }

        public void UpdateBooking(BookingView booking)
        {
            _bookingRepository.UpdateBooking(booking);
        }
    }
}
