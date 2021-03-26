using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HRM.Models;

namespace HRM.MVC.Models
{
    public class HRMMVCContext : DbContext
    {
        public HRMMVCContext (DbContextOptions<HRMMVCContext> options)
            : base(options)
        {
        }

        public DbSet<HRM.Models.DepartmentView> DepartmentView { get; set; }

        public DbSet<HRM.Models.UserModel> UserModel { get; set; }
    }
}
