using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Description;
using HMS.BAL;
using HMS.Models.Models;

namespace HMS.WebApi.Controllers
{
    [BasicAuthentication]
    public class HotelsController : ApiController
    {
        private readonly IHotelsManager db;
        public HotelsController(IHotelsManager hotelsManager)
        {
            db = hotelsManager;
        }


        // GET: api/Hotels
        [Route("api/Hotels")]
        public IHttpActionResult GetHotels()
        {
            return Ok(db.GetHotels());
        }

        // GET: api/Hotels/5
        [ResponseType(typeof(HotelsModel))]
        [Route("api/Hotels/{id:int}")]
        public IHttpActionResult GetHotels(int id)
        {
            HotelsModel hotelsModel = db.GetHotel(id);
            if (hotelsModel == null)
            {
                return NotFound();
            }

            return Ok(hotelsModel);
        }

        // POST: api/Hotels
        [Route("api/Hotels")]
        public IHttpActionResult PostHotels([FromBody]HotelsModel hotelsModel)
        {
            string username = Thread.CurrentPrincipal.Identity.Name;
            hotelsModel.CreatedBy= username;
            hotelsModel.CreatedDate = DateTime.Now.Date;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string s=db.AddHotel(hotelsModel);
            var response = new
            {
                response = s
            };
            return Json(response);  //Json() returns status Code 200 automatically
        }
        [Route("api/Hotels/Edit/{id:int}")]
        public IHttpActionResult PutHotels(int id,[FromBody] HotelsModel hotelsModel)
        {
            string username = Thread.CurrentPrincipal.Identity.Name;
            hotelsModel.UpdatedBy = username;
            hotelsModel.UpdatedDate = DateTime.Now.Date;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string s = db.EditHotel(id,hotelsModel);

            var response = new
            {
                response = s
            };
            return Json(response);  //Json() returns status Code 200 automatically
        }

    }
}