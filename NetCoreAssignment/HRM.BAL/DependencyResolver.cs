using HRM.DAL.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRM.BAL
{
    public static class DependencyResolver
    {
        public static void AddBusinessLayerDependency(ref IServiceCollection services)
        {
            DataDependencyResolver.AddDataLayerDependency(ref services);
            services.AddSingleton<IEmployeeRepository, EmployeeRepository>();
        }
    }
}
