using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitAssignment9
{
    public class Statistics
    {
        ERP _erp;
        public Statistics(ERP erp)
        {
            _erp = erp;
        }
        public int TotalEmployees()
        {
            return _erp.TotalEmployee();
        }
        public int TotalEmployeeByDesignation(string designation)
        {
            int result = _erp.GetEmployeesByDesignation(designation).Count;
            return result;
        }
        public Employee GetHighestPaidEmployee()
        {
            return _erp.GetHighestPaidEmployee();
        }
        public List<Employee> GetEmployees()
        {
            return _erp.GetEmployees();
        }
    }
}
