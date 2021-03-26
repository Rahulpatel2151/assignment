using AutoMapper;
using HRM.DAL.Database;
using HRM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRM.DAL
{
    class AutoMapperConfig: Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Employee, EmployeeView>();
            CreateMap<EmployeeView, Employee>();
            CreateMap<Department, DepartmentView>();
            CreateMap<DepartmentView, Department>();
        }
    }
}
