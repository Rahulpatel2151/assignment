using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using PassengerManagement.Data;
using PassengerManagement.Models;

namespace PassengerManagement.Controllers
{
    public class PassengersController : ApiController
    {
        private IPassengerRepository _passengerRepository;
        public PassengersController(IPassengerRepository passengerRepository)
        {
            _passengerRepository = passengerRepository;
        }
        // GET: api/Passengers
        public IHttpActionResult GetPassengers()
        {
            return Ok(_passengerRepository.GetPassengers());
        }

        // GET: api/Passengers/5
        [ResponseType(typeof(Passenger))]
        public IHttpActionResult GetPassenger(int id)
        {
            Passenger passenger = _passengerRepository.GetByPassengerId(id);
            if (passenger == null)
            {
                return NotFound();
            }

            return Ok(passenger);
        }

        // PUT: api/Passengers/5
        public IHttpActionResult PutPassenger(int id, Passenger passenger)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != passenger.Id)
            {
                return BadRequest();
            }

            Passenger updatedPassenger = _passengerRepository.EditPassenger(passenger);

            return Ok(updatedPassenger);
        }

        // POST: api/Passengers
        [ResponseType(typeof(Passenger))]
        public IHttpActionResult PostPassenger(Passenger passenger)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Passenger addedPassenger=_passengerRepository.AddPassenger(passenger);
            return Ok(addedPassenger);
        }

        // DELETE: api/Passengers/5
        [ResponseType(typeof(Passenger))]
        public IHttpActionResult DeletePassenger(int id)
        {
            Passenger passenger =_passengerRepository.GetByPassengerId(id);
            if (passenger == null)
            {
                return NotFound();
            }

            bool status=_passengerRepository.Delete(id);
            if (status)
            {
                return Ok(passenger);
            }
            else
            {
                return BadRequest();
            }
        }

    }
}