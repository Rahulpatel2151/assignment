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
    public class MechanicRepository : IMechanicRepository
    {
        private DealerManagementEntities db = new DealerManagementEntities();
        private readonly IMapper mapper;
        public MechanicRepository()
        {
            AutoMapperConfig.init();
            mapper = AutoMapperConfig.Mapper;
        }
        public void AddMechanic( MechanicView mechanic)
        {
            Mechanics mechanics = mapper.Map<MechanicView, Mechanics>(mechanic);
            db.Mechanics.Add(mechanics);
            db.SaveChanges();
        }

        
        public IEnumerable<MechanicView> GetMechanics()
        {
            IEnumerable<Mechanics> mlist = db.Mechanics;
            IEnumerable<MechanicView> mechanics = mlist.Select(x => mapper.Map<Mechanics, MechanicView>(x)).ToList();

            return mechanics;
        }

        public MechanicView GetMechanic(int? id)
        {
            Mechanics v = db.Mechanics.Find(id);
            if (v == null)
            {
                return null;
            }
            MechanicView mechanic = mapper.Map<Mechanics, MechanicView>(v);
            return mechanic;

        }

        public void UpdateMechanic(MechanicView mechanics)
        {
            Mechanics mechanic = db.Mechanics.Find(mechanics.Id);
            mechanic.Name = mechanics.Name;
            mechanic.Email = mechanics.Email;
            mechanic.Phone = mechanics.Phone;
            mechanic.Make = mechanics.Make;
           
            db.Entry(mechanic).State = EntityState.Modified;
        }

        public void RemoveMechanic(int id)
        {
            Mechanics v = db.Mechanics.Find(id);
            db.Mechanics.Remove(v);
            db.SaveChanges();

        }

    }
}
