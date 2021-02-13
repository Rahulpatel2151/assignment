using AutoMapper;
using ProductManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.DAL
{
    public static class AutoMapperConfig
    {
        public static IMapper Mapper { get; private set; }
        public static void init()
        {
            var config = new MapperConfiguration(cfg =>
              {
                  cfg.CreateMap<ProductsModel, Products>();
                  cfg.CreateMap<Products, ProductsModel>();
                  cfg.CreateMap<Users, UserView>();
              });
            Mapper = config.CreateMapper();
        }
    }
}
