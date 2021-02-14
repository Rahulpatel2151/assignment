using DealerManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerManagement.DAL.Repository
{
    public interface IServiceRepository
    {
        IEnumerable<ServiceView> GetServices();
        ServiceView GetService(int? id);
        void AddService(ServiceView service);
        void UpdateService(ServiceView service);
        void RemoveService(int id);
    }
}
