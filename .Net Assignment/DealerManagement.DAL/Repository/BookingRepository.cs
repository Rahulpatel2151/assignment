
using AutoMapper;
using DealerManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerManagement.DAL.Repository
{
    public class BookingRepository:IBookingRepository
    {
        private DealerManagementEntities db = new DealerManagementEntities();
        private IUserRepository _userrepository;
        private readonly IMapper mapper;
        public BookingRepository(IUserRepository userRepository)
        {
            _userrepository = userRepository;
            AutoMapperConfig.init();
            mapper = AutoMapperConfig.Mapper;
        }
        public IEnumerable<BookingView> GetBookings(string username)
        {
            UserView user = _userrepository.findUser(username);
            IEnumerable<Bookings> blist = db.Bookings.Where(m => m.UserId == user.Id).AsEnumerable();
            IEnumerable<BookingView> bookings = blist.Select(x => mapper.Map<Bookings, BookingView>(x)).ToList();

            return bookings;
        }

        public BookingView GetBooking(int? id)
        {
            Bookings b = db.Bookings.Find(id);
            if (b == null)
            {
                return null;
            }
            BookingView booking = mapper.Map<Bookings, BookingView>(b);
            return booking;

        }

        public void AddBooking(string name, BookingView booking)
        {
            UserView user = _userrepository.findUser(name);
            Bookings bookings = mapper.Map<BookingView, Bookings>(booking);
            bookings.UserId = user.Id;
            bookings.Status = "Pending";
            db.Bookings.Add(bookings);
            db.SaveChanges();

        }

        public void UpdateBooking(BookingView booking)
        {
            Bookings bookings = db.Bookings.Find(booking.Id);
            bookings.ServiceId = booking.ServiceId;
            bookings.UserId = booking.UserId;
            bookings.VehicleId = booking.VehicleId;
            bookings.StartTime = booking.StartTime;
            bookings.Status = booking.Status;
            bookings.EndTime = booking.EndTime;
            db.Entry(bookings).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void RemoveBooking(int id)
        {
            Bookings b = db.Bookings.Find(id);
            db.Bookings.Remove(b);
            db.SaveChanges();

        }

        public IEnumerable<BookingView> GetBookings()
        {
            IEnumerable<Bookings> blist = db.Bookings;
            IEnumerable<BookingView> bookings = blist.Select(x => mapper.Map<Bookings, BookingView>(x)).ToList();

            return bookings;
        }

        public void ChangeStatus(int? id, string status)
        {
            Bookings b = db.Bookings.Find(id);
            b.Status = status;
            db.Entry(b).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
