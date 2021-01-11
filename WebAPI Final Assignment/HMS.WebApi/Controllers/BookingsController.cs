using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using HMS.Models.Models;
using HMS.BAL;
namespace HMS.WebApi.Controllers
{
    [BasicAuthentication]
    public class BookingsController : ApiController
    {
        private IHotelsManager _hotelsManager;
        public BookingsController(IHotelsManager hotelsManager)
        {
            _hotelsManager = hotelsManager;
        }
        // GET: api/Bookings
        [Route("api/bookings")]
        public IHttpActionResult GetBookings()
        {
            return Ok(_hotelsManager.GetBookings());
        }

        // GET: api/Bookings/5
        [ResponseType(typeof(BookingsModel))]
        public IHttpActionResult GetBookingsModel(int id)
        {
            BookingsModel bookingsModel = _hotelsManager.GetBooking(id);
            if (bookingsModel.Id != id)
            {
                return NotFound();
            }

            return Ok(bookingsModel);
        }

        [Route("api/bookings/changedate/{bookingId:int}")]
        public IHttpActionResult PutBookingDate(int bookingId,[FromBody]string date)
        {
            try
            {
                DateTime d = Convert.ToDateTime(date).Date;
                string s = _hotelsManager.ChangeBookingDate(bookingId, d);
                var response = new
                {
                    response = s
                };
                return Json(response);  //Json() returns status Code 200 automatically
            }
            catch
            {
                return BadRequest("Invalid Date");
            }
        }
        [Route("api/bookings/changestatus/{bookingId:int}")]
        public IHttpActionResult PutBookingsStatus(int bookingId, [FromBody] string status)
        {
            if (status.Equals("Definitive", StringComparison.CurrentCultureIgnoreCase)|| status.Equals("Cancelled", StringComparison.CurrentCultureIgnoreCase))
            {
                try
                {
                    string s = _hotelsManager.ChangeBookingStatus(bookingId, status);
                    var response = new
                    {
                        response = s
                    };
                    return Json(response);  //Json() returns status Code 200 automatically
                }
                catch
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest("Invalid Status");
            }
        }

        // POST: api/Bookings
        [Route("api/Bookings")]
        public IHttpActionResult PostBookingsModel(BookingsModel bookingsModel)
        {
            bookingsModel.BookingDate = Convert.ToDateTime(bookingsModel.BookingDate).Date;
            if (bookingsModel.Status == null)
            {
                bookingsModel.Status = "Booked";
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string status = _hotelsManager.BookRoom(bookingsModel);
            var response = new
            {
                response = status
            };
            return Json(response);  //Json() returns status Code 200 automatically
        }

        // DELETE: api/Bookings/5
        public IHttpActionResult DeleteBooking(int id)
        {
            string bookingstatus = _hotelsManager.DeleteBooking(id);

            var response = new
            {
                response = bookingstatus
            };
            return Json(response);  //Json() returns status Code 200 automatically
        }

        
    }
}