using DealerManagement.DAL.Repository;
using DealerManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerManagement.BAL
{
    public class VehicleManager:IVehicleManager
    {
        IVehicleRepository _vehicleRepository;
        public VehicleManager(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }
        public IEnumerable<VehicleView> getVehicles(string username)
        {
            return _vehicleRepository.getVehicles(username);

        }

        public VehicleView getVehicle(int? id)
        {
            return _vehicleRepository.getVehicle(id);
        }

        public void addVehicle(string name, VehicleView vehicles)
        {
            _vehicleRepository.addVehicle(name, vehicles);
        }

        public void updateVehicle(VehicleView vehicles)
        {
            _vehicleRepository.updateVehicle(vehicles);
        }

        public void removeVehicle(int id)
        {
            _vehicleRepository.removeVehicle(id);

        }
    }
}
