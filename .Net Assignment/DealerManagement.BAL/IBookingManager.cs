using DealerManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerManagement.BAL
{
    public interface IBookingManager
    {
        IEnumerable<BookingView> GetBookings(string username);
        BookingView GetBooking(int? id);
        void AddBooking(string name, BookingView booking);
        void UpdateBooking(BookingView booking);
        void RemoveBooking(int id);
        IEnumerable<BookingView> GetBookings();
        void ChangeStatus(int? id, string status);
    }
}
