using AutoMapper;
using HMS.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.DAL
{
   public class HotelsRepository: IHotelsRepository
    {
        private WebAPIFinalAssignmentEntities db = new WebAPIFinalAssignmentEntities();

        public string AddHotel(HotelsModel hotelsModel)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<HotelsModel, Hotels>();
            });
            IMapper mapper = config.CreateMapper();
            Hotels _hotel = mapper.Map<HotelsModel, Hotels>(hotelsModel);
            db.Hotels.Add(_hotel);
            db.SaveChanges();
            return "Added Successfully";
        }

        public string AddRoom(RoomsModel roomsModel)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<RoomsModel, Rooms>();
            });
            IMapper mapper = config.CreateMapper();
            Rooms room = mapper.Map<RoomsModel, Rooms>(roomsModel);
            db.Rooms.Add(room);
            db.SaveChanges();
            return "Added Successfully";
        }

        public HotelsModel GetHotel(int id)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Hotels, HotelsModel>();
            });
            IMapper mapper = config.CreateMapper();
            Hotels hotel = db.Hotels.Find(id);
            HotelsModel _hotel = mapper.Map<Hotels,HotelsModel>(hotel);
            return _hotel;
        }

        public IEnumerable<HotelsModel> GetHotels()
        {
            IEnumerable<Hotels> hlist = db.Hotels.ToList();
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Hotels, HotelsModel>();
            });
            IMapper mapper = config.CreateMapper();
            IEnumerable<HotelsModel> hotellist = hlist.Select(x =>mapper.Map<Hotels,HotelsModel>(x)).ToList();
            return hotellist.OrderBy(x=>x.HotelName);
        }

        public RoomsModel GetRoom(int id)
        {
            Rooms r = db.Rooms.Find(id);
            if (r == null)
            {
                return null;
            }
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Rooms, RoomsModel>();
            });
            IMapper mapper = config.CreateMapper();
            RoomsModel room =  mapper.Map<Rooms, RoomsModel>(r);
            return room;
        }

        public IEnumerable<RoomsModel> GetRooms()
        {
            IEnumerable<Rooms> rlist = db.Rooms.ToList();
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Rooms, RoomsModel>();
            });
            IMapper mapper = config.CreateMapper();
            IEnumerable<RoomsModel> rooms = rlist.Select(x => mapper.Map<Rooms, RoomsModel>(x)).ToList();
            return rooms.OrderBy(x=>x.Price);
        }
        public bool IsAvailable(DateTime date,int id)
        {
            if (db.Rooms.Find(id) != null)
            {
                var b = db.Bookings.Where(x => x.RoomId == id && x.BookingDate == date && !(x.Status.Equals("Cancelled", StringComparison.CurrentCultureIgnoreCase) || x.Status.Equals("Deleted", StringComparison.CurrentCultureIgnoreCase)));
                if (b.ToList().Count == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public IEnumerable<BookingsModel> GetBookings()
        {
            IEnumerable<Bookings> bookings= db.Bookings.ToList();
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Bookings, BookingsModel>();
            });
            IMapper mapper = config.CreateMapper();
            IEnumerable<BookingsModel> _bookings = bookings.Select(x => mapper.Map<Bookings, BookingsModel>(x)).ToList();
            return _bookings;
        }
        public string BookRoom(BookingsModel bookingsModel)
        {
            if (this.IsAvailable(bookingsModel.BookingDate, bookingsModel.RoomId))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<BookingsModel, Bookings>();
                });

                IMapper mapper = config.CreateMapper();
                Bookings _bookings = mapper.Map<BookingsModel, Bookings>(bookingsModel);
                try
                {
                    db.Bookings.Add(_bookings);
                    db.SaveChanges();
                    return "Successfully Booked";
                }
                catch
                {
                    return "Booking Failed";
                }
            }
            else
            {
                return "Room already booked for this date";
            }
        }
        public string ChangeBookingDate(int bookingId, DateTime date)
        {
            Bookings bookings = db.Bookings.Find(bookingId);
            if (this.IsAvailable(date,bookings.RoomId))
            {
                try
                {
                    bookings.BookingDate = date;
                    db.Entry(bookings).State = EntityState.Modified;
                    db.SaveChanges();
                    return "Booking date is changed";
                }
                catch
                {
                    return "Failed to Update the date";
                }
            }
            else
            {
                return "Room is already booked for this date";
            }
        }

        public string ChangeBookingStatus(int bookingId, string status)
        {
            Bookings bookings = db.Bookings.Find(bookingId);
            try
            {
                bookings.Status = status;
                db.Entry(bookings).State = EntityState.Modified;
                db.SaveChanges();
                return "Status of Booking Successfully Changed";
            }
            catch
            {
                return "Falied to Change the status";
            }
        }

        public string DeleteBooking(int id)
        {
            Bookings bookings = db.Bookings.Find(id);
            if (bookings != null)
            {
                try
                {
                    bookings.Status = "Deleted";
                    db.Entry(bookings).State = EntityState.Modified;
                    db.SaveChanges();
                    return "Successfully Deleted";
                }
                catch
                {
                    return "Not Deleted";
                }
            }
            else
            {
                return "Booking not Found";
            }
        }

        public BookingsModel GetBooking(int id)
        {
            Bookings bookings = db.Bookings.Find(id);
            if (bookings != null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Bookings, BookingsModel>();
                });
                IMapper mapper = config.CreateMapper();
                BookingsModel _bookings = mapper.Map<Bookings, BookingsModel>(bookings);
                return _bookings;
            }
            else
            {
                BookingsModel bookingsModel = new BookingsModel();
                return bookingsModel;
            }
        }

        public IEnumerable<RoomsModel> GetFilteredRooms(string city, string pincode, string category, decimal price)
        {
            IEnumerable<Rooms> rooms = db.Rooms.ToList();
            if (city != null)
            {
                rooms = rooms.Where(x=>x.Hotels.City.Equals(city, StringComparison.CurrentCultureIgnoreCase));
            }
            if (pincode != null)
            {
                rooms = rooms.Where(x => x.Hotels.PinCode.Equals(pincode, StringComparison.CurrentCultureIgnoreCase));
            }
            if (category != null)
            {
                rooms = rooms.Where(x => x.Category.Equals(category, StringComparison.CurrentCultureIgnoreCase));
            }
            rooms = rooms.Where(x => x.Price <= price);
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Rooms, RoomsModel>();
            });
            IMapper mapper = config.CreateMapper();
            IEnumerable<RoomsModel> _rooms = rooms.Select(x => mapper.Map<Rooms, RoomsModel>(x)).ToList();
            return _rooms;
        }

        public string EditHotel(int id,HotelsModel hotelsModel)
        {
            Hotels hotels = db.Hotels.Find(id);
            if (hotels == null)
            {
                return "Hotel not found";
            }
            else
            {
                hotels.HotelName = hotelsModel.HotelName;
                hotels.Address = hotelsModel.Address;
                hotels.City = hotelsModel.City;
                hotels.PinCode = hotelsModel.PinCode;
                hotels.ContactNumber = hotelsModel.ContactNumber;
                hotels.ContactPerson = hotelsModel.ContactPerson;
                hotels.Website = hotelsModel.Website;
                hotels.Facebook = hotelsModel.Facebook;
                hotels.Twitter = hotelsModel.Twitter;
                hotels.IsActive = hotelsModel.IsActive;
                hotels.UpdatedBy = hotelsModel.UpdatedBy;
                hotels.UpdatedDate = hotelsModel.UpdatedDate;
                db.Entry(hotels).State = EntityState.Modified;
                db.SaveChanges();
                return "Hotel Details Modified";

            }

        }
        public string EditRoom(int id, RoomsModel roomsModel)
        {
            Rooms rooms = db.Rooms.Find(id);
            if (rooms == null)
            {
                return "Hotel not found";
            }
            else
            {
                rooms.RoomName = roomsModel.RoomName;
                rooms.HotelId = roomsModel.HotelId;
                rooms.Category = roomsModel.Category;
                rooms.Price = roomsModel.Price;
                rooms.IsActive = roomsModel.IsActive;
                rooms.UpdatedBy = roomsModel.UpdatedBy;
                rooms.UpdatedDate = roomsModel.UpdatedDate;
                db.Entry(rooms).State = EntityState.Modified;
                db.SaveChanges();
                return "Room Details Modified";

            }

        }
    }
}
