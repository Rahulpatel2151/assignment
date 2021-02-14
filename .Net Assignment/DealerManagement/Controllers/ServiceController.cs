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
    public class ServiceController : Controller
    {
        IServiceManager _serviceManager;
        public ServiceController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        // GET: Service
        public ActionResult Index()
        {

            return View(_serviceManager.GetServices());
        }

        // GET: Service/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceView service = _serviceManager.GetService(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // GET: Service/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Service/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ServiceName,Price,Duration,Status")] ServiceView serviceView)
        {
            if (ModelState.IsValid)
            {
                _serviceManager.AddService(serviceView);
                return RedirectToAction("Index");
            }

            return View(serviceView);
        }

        // GET: Service/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceView service = _serviceManager.GetService(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // POST: Service/Edit/5
       [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ServiceName,Price,Duration,Status")] ServiceView serviceView)
        {
            if (ModelState.IsValid)
            {
                _serviceManager.UpdateService(serviceView);
                return RedirectToAction("Index");
            }
            return View(serviceView);
        }

        // GET: Service/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceView service = _serviceManager.GetService(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // POST: Service/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _serviceManager.RemoveService(id);
            return RedirectToAction("Index");
        }

    }
}
