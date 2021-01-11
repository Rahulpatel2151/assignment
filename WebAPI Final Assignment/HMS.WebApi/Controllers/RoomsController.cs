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
using System.Web.Http.Results;
using HMS.BAL;
using HMS.Models.Models;

namespace HMS.WebApi.Controllers
{
    [BasicAuthentication]
    public class RoomsController : ApiController
    {
        private IHotelsManager _hotelsManager ;
        public RoomsController(IHotelsManager hotelsManager)
        {
            _hotelsManager = hotelsManager;
        }

        // GET: api/Rooms
        [Route("api/rooms")]
        public IEnumerable<RoomsModel> GetRooms()
        {
            return _hotelsManager.GetRooms();
        }

        //// GET: api/Rooms/5
        [ResponseType(typeof(RoomsModel))]
        [Route("api/rooms/{id:int}")]
        public IHttpActionResult GetRooms([FromUri]int id)
        {
            RoomsModel roomsModel = _hotelsManager.GetRoom(id);
            if (roomsModel == null)
            {
                return NotFound();
            }

            return Ok(roomsModel);
        }

        [Route("api/findrooms/{city?}/{pincode?}/{category?}/{price?}")]
        public IHttpActionResult GetFilteredRooms( string city=null,string pincode=null,string category=null,decimal price=decimal.MaxValue)
        {
            IEnumerable<RoomsModel> roomsModels = _hotelsManager.GetFilteredRooms(city,pincode,category,price);
            if (roomsModels == null)
            {
                return NotFound();
            }

            return Ok(roomsModels);
        }

        // POST: api/Rooms
        [Route("api/rooms/")]
        public IHttpActionResult PostRooms(RoomsModel roomsModel)
        {
            string username = Thread.CurrentPrincipal.Identity.Name;
            roomsModel.CreatedBy = username;
            roomsModel.CreatedDate = DateTime.Now.Date;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string s=_hotelsManager.AddRoom(roomsModel);

            var response = new
            {
                response = s
            };
            return Json(response);  //Json() returns status Code 200 automatically       
        }
            [Route("api/rooms/availability/{id:int}")]
        public IHttpActionResult GetAvailability(int id,[FromBody]string date)
        {
            DateTime d;
            try
            {
                d = Convert.ToDateTime(date).Date;
                bool availability=_hotelsManager.IsAvailable(d, id);
                var response = new
                {
                    available = availability
                };
                return Json(response);  //Json() returns status Code 200 automatically
            }
            catch
            {
                return BadRequest("Invalid Date");
            }
        }
        [Route("api/Rooms/Edit/{id:int}")]
        public IHttpActionResult PutRooms(int id, [FromBody] RoomsModel roomsModel)
        {
            string username = Thread.CurrentPrincipal.Identity.Name;
            roomsModel.UpdatedBy = username;
            roomsModel.UpdatedDate = DateTime.Now.Date;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string s = _hotelsManager.EditRoom(id, roomsModel);
            var response = new
            {
                response = s
            };
            return Json(response);  //Json() returns status Code 200 automatically
        }
    }
}