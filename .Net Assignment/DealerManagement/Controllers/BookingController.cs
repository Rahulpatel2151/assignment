using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DealerManagement.BAL;
using DealerManagement.Data;
using DealerManagement.Models;

namespace DealerManagement.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        IBookingManager _bookingManager;
        IUserManager _userManager;
        IVehicleManager _vehicleManager;
        IServiceManager _serviceManager;
        public BookingController(IBookingManager bookingManager, IUserManager userManager,IVehicleManager vehicleManager,IServiceManager serviceManager)
        {
            _userManager = userManager;
            _bookingManager = bookingManager;
            _vehicleManager = vehicleManager;
            _serviceManager = serviceManager;
        }
        // GET: Booking
        public ActionResult Index()
        {
            return View(_bookingManager.GetBookings(User.Identity.Name));
        }

        // GET: Booking/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingView booking = _bookingManager.GetBooking(id);
            if (booking.UserId != _userManager.findUser(User.Identity.Name).Id)
            {
                return HttpNotFound();
            }
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: Booking/Create
        public ActionResult Create()
        {
            List<SelectListItem> VehicleId=new List<SelectListItem>();
            foreach (var item in _vehicleManager.getVehicles(User.Identity.Name))
            {
                VehicleId.Add(new SelectListItem { Text = item.Model, Value = ""+ item.Id });

            }
            List<SelectListItem> ServiceId = new List<SelectListItem>();
            foreach (var item in _serviceManager.GetServices())
            {
                ServiceId.Add(new SelectListItem { Text = item.ServiceName, Value = "" + item.Id });

            }
            ViewData["VehicleId"] = VehicleId;
            ViewData["ServiceId"] = ServiceId;

            return View();
        }

        // POST: Booking/Create
       [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ServiceId,VehicleId,StartTime,EndTime,Status")] BookingView booking)
        {
            List<SelectListItem> VehicleId = new List<SelectListItem>();
            foreach (var item in _vehicleManager.getVehicles(User.Identity.Name))
            {
                VehicleId.Add(new SelectListItem { Text = item.Model, Value = "" + item.Id });

            }
            List<SelectListItem> ServiceId = new List<SelectListItem>();
            foreach (var item in _serviceManager.GetServices())
            {
                ServiceId.Add(new SelectListItem { Text = item.ServiceName, Value = "" + item.Id });

            }
            ViewData["VehicleId"] = VehicleId;
            ViewData["ServiceId"] = ServiceId;
            if (ModelState.IsValid)
            {
                booking.UserId = _userManager.findUser(User.Identity.Name).Id;
                _bookingManager.AddBooking(User.Identity.Name, booking);
                return RedirectToAction("Index");
            }

            return View(booking);

        }

        // GET: Booking/Edit/5
        public ActionResult Edit(int? id)
        {
            List<SelectListItem> VehicleId = new List<SelectListItem>();
            foreach (var item in _vehicleManager.getVehicles(User.Identity.Name))
            {
                VehicleId.Add(new SelectListItem { Text = item.Model, Value = "" + item.Id });

            }
            List<SelectListItem> ServiceId = new List<SelectListItem>();
            foreach (var item in _serviceManager.GetServices())
            {
                ServiceId.Add(new SelectListItem { Text = item.ServiceName, Value = "" + item.Id });

            }
            ViewData["VehicleId"] = VehicleId;
            ViewData["ServiceId"] = ServiceId;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            BookingView booking = _bookingManager.GetBooking(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Booking/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,ServiceId,VehicleId,StartTime,EndTime,Status")] BookingView booking)
        {
            List<SelectListItem> VehicleId = new List<SelectListItem>();
            foreach (var item in _vehicleManager.getVehicles(User.Identity.Name))
            {
                VehicleId.Add(new SelectListItem { Text = item.Model, Value = "" + item.Id });

            }
            List<SelectListItem> ServiceId = new List<SelectListItem>();
            foreach (var item in _serviceManager.GetServices())
            {
                ServiceId.Add(new SelectListItem { Text = item.ServiceName, Value = "" + item.Id });

            }
            ViewData["VehicleId"] = VehicleId;
            ViewData["ServiceId"] = ServiceId;
            UserView u = _userManager.findUser(User.Identity.Name);
            if (u != null)
            {

                if (booking.UserId != u.Id)
                {
                    return HttpNotFound();
                }
                if (ModelState.IsValid)
                {
                    _bookingManager.UpdateBooking(booking);
                    return RedirectToAction("Index");
                }
                return View(booking);
            }
            else
            {
                return HttpNotFound();
            }

        }

        // GET: Booking/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id != _userManager.findUser(User.Identity.Name).Id)
            {
                return HttpNotFound();
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingView booking = _bookingManager.GetBooking(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Booking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BookingView booking = _bookingManager.GetBooking(id);

            if (booking.UserId != _userManager.findUser(User.Identity.Name).Id)
            {
                return HttpNotFound();
            }
            _bookingManager.RemoveBooking(id);
            return RedirectToAction("Index");
        }
    }
}
