using AutoMapper;
using DealerManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerManagement.DAL.Repository
{
    public class VehicleRepository: IVehicleRepository
    {
        private DealerManagementEntities db = new DealerManagementEntities();
        private IUserRepository _userrepository;
        private readonly IMapper mapper;
        public VehicleRepository(IUserRepository userRepository)
        {
            _userrepository = userRepository;
            AutoMapperConfig.init();
            mapper = AutoMapperConfig.Mapper;
        }
        public IEnumerable<VehicleView> getVehicles(string username)
        {
            UserView user = _userrepository.findUser(username);
            IEnumerable<Vehicles> vlist = db.Vehicles.Where(m => m.CustomerId == user.Id).AsEnumerable();
            IEnumerable<VehicleView> vehicle = vlist.Select(x => mapper.Map<Vehicles, VehicleView>(x)).ToList();

            return vehicle;
        }

        public VehicleView getVehicle(int? id)
        {
            Vehicles v = db.Vehicles.Find(id);
            if (v == null)
            {
                return null;
            }
            VehicleView vehicle = mapper.Map<Vehicles, VehicleView>(v);
            return vehicle;

        }

        public void addVehicle(string name, VehicleView vehicles)
        {
            UserView user = _userrepository.findUser(name);
            Vehicles vehicle = mapper.Map<VehicleView, Vehicles>(vehicles);
            vehicle.CustomerId = user.Id;
            db.Vehicles.Add(vehicle);
            db.SaveChanges();

        }

        public void updateVehicle(VehicleView vehicles)
        {
            Vehicles vehicle = db.Vehicles.Find(vehicles.Id);
            vehicle.ChassisNo = vehicles.ChassisNo;
            vehicle.CustomerId = vehicles.CustomerId;
            vehicle.LicensePlate = vehicles.LicensePlate;
            vehicle.Make = vehicles.Make;
            vehicle.Model = vehicles.Model;
            vehicle.RegistrationDate = vehicles.RegistrationDate;
            db.Entry(vehicle).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void removeVehicle(int id)
        {
            Vehicles v = db.Vehicles.Find(id);
            db.Vehicles.Remove(v);
            db.SaveChanges();

        }
    }
}
