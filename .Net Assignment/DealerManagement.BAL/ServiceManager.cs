using DealerManagement.DAL.Repository;
using DealerManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerManagement.BAL
{
    public class ServiceManager:IServiceManager
    {
        IServiceRepository _serviceRepository;
        public ServiceManager(IServiceRepository serviceRepsitory)
        {
            _serviceRepository = serviceRepsitory;
        }
        public IEnumerable<ServiceView> GetServices()
        {
            return _serviceRepository.GetServices();

        }

        public ServiceView GetService(int? id)
        {
            return _serviceRepository.GetService(id);
        }

        public void AddService(ServiceView service)
        {
            _serviceRepository.AddService(service);
        }

        public void UpdateService(ServiceView service)
        {
            _serviceRepository.UpdateService(service);
        }

        public void RemoveService(int id)
        {
            _serviceRepository.RemoveService(id);

        }
    }
}
