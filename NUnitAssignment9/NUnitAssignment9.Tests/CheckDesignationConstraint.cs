using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace NUnitAssignment9.Tests
{
    public class CheckDesignationConstraint : NUnit.Framework.Constraints.Constraint
    {
        readonly string _designation;
       
        public CheckDesignationConstraint(string designation)
        {
            _designation = designation;
            
        }
        public override ConstraintResult ApplyTo<TActual>(TActual actual)
        {
            List<Employee> employees = actual as List<Employee>;
           foreach(Employee e in employees)
            {
                if (e.Designation != _designation)
                {
                    return new ConstraintResult(this, actual, ConstraintStatus.Error);
                }
            }
            return new ConstraintResult(this, actual, ConstraintStatus.Success);

        }
    }
    public class Is : NUnit.Framework.Is
    {
        public static CheckDesignationConstraint CheckDesignation(string designation)
        {
            return new CheckDesignationConstraint(designation);
        }
    }
}
