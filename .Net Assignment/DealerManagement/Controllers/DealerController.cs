using DealerManagement.BAL;
using DealerManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DealerManagement.Controllers
{
    public class DealerController : Controller
    {
        // GET: Dealer
        IBookingManager _bookingManager;
        IUserManager _userManager;
        IVehicleManager _VehicleManager;
        IServiceManager _serviceManager;
        public DealerController(IBookingManager bookingManager,IUserManager userManager,IVehicleManager vehicleManager,IServiceManager serviceManager)
        {
            _bookingManager = bookingManager;
            _userManager = userManager;
            _VehicleManager = vehicleManager;
            _serviceManager = serviceManager;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Bookings()
        {
            IEnumerable<BookingView> bookings = _bookingManager.GetBookings();
            return View(bookings);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BookingView bookings = _bookingManager.GetBooking(id);
            UserView user = _userManager.findUserById(bookings.UserId);
            ViewBag.UserEmail = user.Email;
            VehicleView vehicle = _VehicleManager.getVehicle(bookings.VehicleId);
            ViewBag.LicensePlate = vehicle.LicensePlate;
            ServiceView service = _serviceManager.GetService(bookings.ServiceId);
            ViewBag.ServiceName = service.ServiceName;
            if (bookings == null)
            {
                return HttpNotFound();
            }
            return View(bookings);
        }
        public ActionResult ChangeStatus(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingView bookings = _bookingManager.GetBooking(id);
            if (bookings == null)
            {
                return HttpNotFound();
            }
            return View(bookings);
        }
        [HttpPost]
        public ActionResult ChangeStatus(int? id,string status)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _bookingManager.ChangeStatus(id,status);

            return RedirectToAction("Index");
        }



    }
}