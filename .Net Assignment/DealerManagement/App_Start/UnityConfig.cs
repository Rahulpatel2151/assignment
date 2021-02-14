using DealerManagement.BAL;
using DealerManagement.BAL.Helper;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace DealerManagement
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IUserManager, UserManager>();
            container.RegisterType<IVehicleManager, VehicleManager>();
            container.RegisterType<IMechanicManager, MechanicManager>();
            container.RegisterType<IServiceManager, ServiceManager>();
            container.RegisterType<IBookingManager, BookingManager>();

            container.AddNewExtension<UnityHelper>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}