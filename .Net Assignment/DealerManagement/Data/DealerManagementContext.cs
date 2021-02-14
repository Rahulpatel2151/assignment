using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DealerManagement.Data
{
    public class DealerManagementContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public DealerManagementContext() : base("name=DealerManagementContext")
        {
        }

        public System.Data.Entity.DbSet<DealerManagement.Models.VehicleView> VehicleView { get; set; }

        public System.Data.Entity.DbSet<DealerManagement.Models.MechanicView> MechanicView { get; set; }

        public System.Data.Entity.DbSet<DealerManagement.Models.ServiceView> ServiceViews { get; set; }

        public System.Data.Entity.DbSet<DealerManagement.Models.BookingView> BookingViews { get; set; }
    }
}
