using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NUnitAssignment8.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [OrderedTest(1)]
        public void TestGetEmployees()
        {
            //Arrange
            int total = 4;

            //Act + Assert
            Assert.That(ERP.GetEmployees(),Has.Count.EqualTo(total)
                        .And.All.Property("Name").Not.Null
                        .And.All.Property("Salary").GreaterThanOrEqualTo(20000));
        }
        [OrderedTest(3)]
        public void TestGetEmployee()
        {
            //Arrange
            int id = 2;
            string name = "abc2";
            //Act
            Employee actual = ERP.GetEmployee(id);
            //Assert
            Assert.That(actual, Has.Property("Name").EqualTo(name));
        }
        [OrderedTest(4)]
        public void TestGetEmployeesByDesignation()
        {
            //Arrange
            string designation = "SE";
            //Act

            //Act
            List<Employee> actualList = ERP.GetEmployeesByDesignation(designation);
            //Assert
            Assert.That(actualList, Has.Count.EqualTo(2)
                                       .And.All.Property("Salary").GreaterThanOrEqualTo(20000)
                                       .And.All.Property("Salary").LessThanOrEqualTo(40000));
        }
        [OrderedTest(2)]
        public void TestGetEmployeeName()
        {
            //Arrange
            int id = 1;
            string name = "abc1";
            //Act + Assert
            Assert.That(ERP.GetEmployeeName(id),Is.EqualTo(name));
        }


        [OrderedTest(0)]
        public void TestGetHighestPaidEmployee()
        {
            //Arrange
            Employee expected = new Employee() { Id = 3, Name = "abc3", Salary = 50000, Designation = "TL" };
            //Act
            Employee actual =  ERP.GetHighestPaidEmployee();
            Assert.That(actual,Has.Property("Name").EqualTo(expected.Name)
                                  .And.Property("Designation").EqualTo(expected.Designation));
        }
        [TestCaseSource(sourceName: "TestSource")]
        public void MainTest(TestStructure test)
        {
            test.Test();
        }
        public static IEnumerable<TestCaseData> TestSource
        {
            get
            {
                var assembly = Assembly.GetExecutingAssembly();
                Dictionary<int, List<MethodInfo>> methods = assembly
                .GetTypes()
                .SelectMany(x => x.GetMethods())
                .Where(y => y.GetCustomAttributes().OfType<OrderedTestAttribute>().Any())
                .GroupBy(z => z.GetCustomAttribute<OrderedTestAttribute>().Order)
                .ToDictionary(gdc => gdc.Key, gdc => gdc.ToList());
                foreach (var order in methods.Keys.OrderBy(x => x))
                {
                    foreach (var methodInfo in methods[order])
                    {
                        MethodInfo info = methodInfo;
                        yield return new TestCaseData(
                        new TestStructure
                        {
                            Test = () =>
                            {
                                object classInstance =
        Activator.CreateInstance(info.DeclaringType, null);
                                info.Invoke(classInstance, null);
                            }
                        }).SetName(methodInfo.Name);
                    }
                }
            }
        }
    }
    public class TestStructure
    {
        public Action Test;
    }
    public class OrderedTestAttribute : Attribute
    {
        public int Order { get; set; }
        public OrderedTestAttribute(int order)
        {
            this.Order = order;
        }
    }
}