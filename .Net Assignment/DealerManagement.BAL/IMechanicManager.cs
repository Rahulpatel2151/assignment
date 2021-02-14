using DealerManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerManagement.BAL
{
    public interface IMechanicManager
    {
        IEnumerable<MechanicView> GetMechanics();
        MechanicView GetMechanic(int? id);
        void AddMechanic( MechanicView mechanic);
        void UpdateMechanic(MechanicView mechanic);
        void RemoveMechanic(int id);
    }
}
