using PassengerManagement.Data;
using PassengerManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassengerManagement.Data
{
    public class PassengerRepository : IPassengerRepository
    {
        readonly Dictionary<int, Passenger> _passengerList = new Dictionary<int, Passenger>();
        public PassengerRepository()
        {
            _passengerList.Add(1, new Passenger() { Id = 1, FirstName = "Data_firstname1", LastName = "Data_lastname1", Phone="123456789" });
            _passengerList.Add(2, new Passenger() { Id = 2, FirstName = "Data_firstname2", LastName = "Data_lastname2", Phone = "123456789" });
            _passengerList.Add(3, new Passenger() { Id = 3, FirstName = "Data_firstname3", LastName = "Data_lastname3", Phone = "123456789" });
        }
        public Passenger GetByPassengerId(int id)
        {
            return _passengerList.FirstOrDefault(x => x.Key == id).Value;
        }
        public IList<Passenger> GetPassengers()
        {
            return _passengerList.Select(x => x.Value).ToList();
        }
        public Passenger AddPassenger(Passenger passenger)
        {
            int newId = !GetPassengers().Any() ? 1 : GetPassengers().Max(x => x.Id) + 1;
            passenger.Id = newId;
            _passengerList.Add(newId, passenger);
            return passenger;
        }

        public Passenger EditPassenger(Passenger passenger)
        {
            Passenger obj = GetByPassengerId(passenger.Id);
            if (obj == null)
                return null;
            _passengerList[obj.Id] = passenger;
            return passenger;
        }

        public bool Delete(int Id)
        {
            var result = _passengerList.Remove(Id);
            return result;
        }
    }
}