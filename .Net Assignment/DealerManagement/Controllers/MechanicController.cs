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
    public class MechanicController : Controller
    {
        IMechanicManager _mechanicManager;
        public MechanicController(IMechanicManager mechanicManager)
        {
            _mechanicManager = mechanicManager;
        }
        // GET: Mechanic
        public ActionResult Index()
        {
            return View(_mechanicManager.GetMechanics());
        }

        // GET: Mechanic/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MechanicView mechanic = _mechanicManager.GetMechanic(id);
            if (mechanic == null)
            {
                return HttpNotFound();
            }
            return View(mechanic);
        }

        // GET: Mechanic/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Mechanic/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Email,Phone,Make")] MechanicView mechanicview)
        {
            if (ModelState.IsValid)
            {
                _mechanicManager.AddMechanic(mechanicview);
                return RedirectToAction("Index");
            }

            return View(mechanicview);
        }

        // GET: Mechanic/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MechanicView mechanic = _mechanicManager.GetMechanic(id);
            if (mechanic == null)
            {
                return HttpNotFound();
            }
            return View(mechanic);
        }

        // POST: Mechanic/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Email,Phone,Make")] MechanicView mechanicview)
        {
            if (ModelState.IsValid)
            {
                _mechanicManager.UpdateMechanic(mechanicview);
                return RedirectToAction("Index");
            }
            return View(mechanicview);
        }

        // GET: Mechanic/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MechanicView mechanic = _mechanicManager.GetMechanic(id);
            if (mechanic == null)
            {
                return HttpNotFound();
            }
            return View(mechanic);
        }

        // POST: Mechanic/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _mechanicManager.RemoveMechanic(id);
            return RedirectToAction("Index");
        }

    }
}
