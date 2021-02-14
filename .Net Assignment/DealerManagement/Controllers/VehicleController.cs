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
    public class VehicleController : Controller
    {
        IVehicleManager _vehicleManager;
        IUserManager _userManager;
        public VehicleController(IVehicleManager vehicleManager, IUserManager userManager)
        {
            _userManager = userManager;
            _vehicleManager = vehicleManager;
        }
        // GET: Vehicle
        public ActionResult Index()
        {
            return View(_vehicleManager.getVehicles(User.Identity.Name));
        }

        // GET: Vehicle/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleView vehicle = _vehicleManager.getVehicle(id);
            if (vehicle.CustomerId != _userManager.findUser(User.Identity.Name).Id)
            {
                return HttpNotFound();
            }
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // GET: Vehicle/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vehicle/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,LicensePlate,ChassisNo,RegistrationDate,Make,Model")] VehicleView vehicles)
        {
            if (ModelState.IsValid)
            {
                vehicles.CustomerId = _userManager.findUser(User.Identity.Name).Id;
                _vehicleManager.addVehicle(User.Identity.Name, vehicles);
                return RedirectToAction("Index");
            }

            return View(vehicles);

        }

        // GET: Vehicle/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleView vehicles = _vehicleManager.getVehicle(id);
            if (vehicles == null)
            {
                return HttpNotFound();
            }
            return View(vehicles);
        }

        // POST: Vehicle/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CustomerId,LicensePlate,ChassisNo,RegistrationDate,Make,Model")] VehicleView vehicles)
        {
            UserView u = _userManager.findUser(User.Identity.Name);
            if (u != null)
            {
               
                if (vehicles.CustomerId != u.Id)
                {
                    return HttpNotFound();
                }
                if (ModelState.IsValid)
                {
                    _vehicleManager.updateVehicle(vehicles);
                    return RedirectToAction("Index");
                }
                return View(vehicles);
            }
            else
            {
                return HttpNotFound();
            }
            
        }

        // GET: Vehicle/Delete/5
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
            VehicleView vehicles = _vehicleManager.getVehicle(id);
            if (vehicles == null)
            {
                return HttpNotFound();
            }
            return View(vehicles);
        }

        // POST: Vehicle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VehicleView v = _vehicleManager.getVehicle(id);

            if (v.CustomerId != _userManager.findUser(User.Identity.Name).Id)
            {
                return HttpNotFound();
            }
            _vehicleManager.removeVehicle(id);
            return RedirectToAction("Index");
        }
    }
}
