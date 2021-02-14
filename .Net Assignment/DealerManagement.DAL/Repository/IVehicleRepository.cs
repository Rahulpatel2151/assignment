using DealerManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerManagement.DAL.Repository
{
    public interface IVehicleRepository
    {
        IEnumerable<VehicleView> getVehicles(string username);
        VehicleView getVehicle(int? id);
        void addVehicle(string name, VehicleView vehicles);
        void updateVehicle(VehicleView vehicles);
        void removeVehicle(int id);
    }
}
