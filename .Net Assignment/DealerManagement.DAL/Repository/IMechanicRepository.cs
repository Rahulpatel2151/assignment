using DealerManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerManagement.DAL.Repository
{
    public interface IMechanicRepository
    {
        MechanicView GetMechanic(int? id);
        void AddMechanic( MechanicView mechanic);
        void UpdateMechanic(MechanicView mechanic);
        void RemoveMechanic(int id);
        IEnumerable<MechanicView> GetMechanics();
    }
}
