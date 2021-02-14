using DealerManagement.DAL.Repository;
using DealerManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerManagement.BAL
{
    public class MechanicManager:IMechanicManager
    {
        IMechanicRepository _mechanicRepository;
        public MechanicManager(IMechanicRepository mechanicRepository)
        {
            _mechanicRepository = mechanicRepository;
        }
        public IEnumerable<MechanicView> GetMechanics()
        {
            return _mechanicRepository.GetMechanics();

        }

        public MechanicView GetMechanic(int? id)
        {
            return _mechanicRepository.GetMechanic(id);
        }

        public void AddMechanic(MechanicView mechanic)
        {
            _mechanicRepository.AddMechanic( mechanic);
        }

        public void UpdateMechanic(MechanicView mechanic)
        {
            _mechanicRepository.UpdateMechanic(mechanic);
        }

        public void RemoveMechanic(int id)
        {
            _mechanicRepository.RemoveMechanic(id);

        }
    }
}
