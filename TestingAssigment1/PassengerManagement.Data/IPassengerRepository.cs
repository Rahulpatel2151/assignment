using PassengerManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassengerManagement.Data
{
    public interface IPassengerRepository
    {
        IList<Passenger> GetPassengers();
        Passenger GetByPassengerId(int id);
        Passenger AddPassenger(Passenger passenger);
        Passenger EditPassenger(Passenger passenger);
        bool Delete(int Id);
    }
}
