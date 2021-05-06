using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace NUnitAssignment9.Tests
{
    public class Tests
    {
        ERP _erp;
        [SetUp]
        public void Setup()
        {
            _erp = new ERP();
        }

        [Test]
        public void TestSEEmployeesByDesignation()
        {
            //Arrange
            string designation = "SE";
            //Act

            List<Employee> actualList = _erp.GetEmployeesByDesignation(designation);
            //Assert
            Assert.That(actualList, Is.CheckDesignation(designation));
        }
        [Test]
        public void TestTLEmployeesByDesignation()
        {
            //Arrange
            string designation = "TL";
            //Act

            List<Employee> actualList = _erp.GetEmployeesByDesignation(designation);
            //Assert
            Assert.That(actualList, Is.CheckDesignation(designation));
        }
        [Test]
        public void TestGetEmployees()
        {
            //Arrange
            var employees = new List<Employee>()
            {
                new Employee(){Id=1,Name="abc1",Salary=40000,Designation="SE"},
                new Employee(){Id=2,Name="abc2",Salary=35000,Designation="SE"},
            };
            var mock = new Mock<ERP>();
            mock.Setup(x => x.GetEmployees()).Returns(employees);
            //Act
            Statistics s = new Statistics(mock.Object);
            List<Employee> actual = s.GetEmployees();
            //Assert
            Assert.That(Is.Equals(actual.Count, employees.Count));
        }
        [Test]
        public void TestTLTotalEmployeesByDesignation()
        {
            //Arrange
            string designation = "TL";
             var employees = new List<Employee>()
            {
                new Employee(){Id=1,Name="abc1",Salary=40000,Designation="TL"},
                new Employee(){Id=2,Name="abc2",Salary=35000,Designation="TL"},
            };
            var mock = new Mock<ERP>();
            mock.Setup(x => x.GetEmployeesByDesignation(designation)).Returns(employees);
            //Act
            Statistics s = new Statistics(mock.Object);
            int actual = s.TotalEmployeeByDesignation(designation);
            //Assert
            Assert.That(actual, NUnit.Framework.Is.EqualTo(2));
        }
        [Test]
        public void TestSETotalEmployeesByDesignation()
        {
            //Arrange
            string designation = "SE";
            var employees = new List<Employee>()
            {
                new Employee(){Id=1,Name="abc1",Salary=40000,Designation="SE"},
                new Employee(){Id=2,Name="abc2",Salary=35000,Designation="SE"},
            };
            var mock = new Mock<ERP>();
            mock.Setup(x => x.GetEmployeesByDesignation(designation)).Returns(employees);
            //Act
            Statistics s = new Statistics(mock.Object);
            int actual = s.TotalEmployeeByDesignation(designation);
            //Assert
            Assert.That(actual, NUnit.Framework.Is.EqualTo(2));
        }
        [Test]
        public void TestSETotalEmployees()
        {
            //Arrange
           
            var mock = new Mock<ERP>();
            mock.Setup(x => x.TotalEmployee()).Returns(3);
            //Act
            Statistics s = new Statistics(mock.Object);
            int actual = s.TotalEmployees();
            //Assert
            Assert.That(actual, NUnit.Framework.Is.EqualTo(3));
        }
        [Test]
        public void TestGetHighestPaidEmployee()
        {
            //Arrange
            var employee = new Employee()
            {
                Id=1,Name="abc1",Salary=40000,Designation="TL"
            };
            var mock = new Mock<ERP>();
            mock.Setup(x => x.GetHighestPaidEmployee()).Returns(employee);
            //Act
            Statistics s = new Statistics(mock.Object);
            Employee actual = s.GetHighestPaidEmployee();
            //Assert
            Assert.That(Is.Equals(actual.Id, employee.Id));
        }
        
    }
}