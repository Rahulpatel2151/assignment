using AutoMapper;
using DealerManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerManagement.DAL
{
    public static class AutoMapperConfig
    {
        public static IMapper Mapper { get; private set; }
        public static void init()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Vehicles, VehicleView>();
                cfg.CreateMap<VehicleView, Vehicles>();
                cfg.CreateMap<Customers, UserView>();
                cfg.CreateMap<UserRegistration, Customers>();
                cfg.CreateMap<Mechanics, MechanicView>();
                cfg.CreateMap<MechanicView, Mechanics>();
                cfg.CreateMap<Services, ServiceView>();
                cfg.CreateMap<ServiceView, Services>();
                cfg.CreateMap<Bookings, BookingView>();
                cfg.CreateMap<BookingView, Bookings>();
            });
            Mapper = config.CreateMapper();
        }
    }
}
