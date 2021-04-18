using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnitAssignment7;
namespace NUnitAssignment7.Tests
{
    [TestFixture]
    public class TestCases
    {
        [Test]
        public void TestFactorial()
        {
            //Arrange
            int n = 5;
            int expected = 120;
            //Act
            int actual = FirstQuestion.Factorial(n);
            //Assert
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestDiscount()
        {
            //Arrange
            int amount1=12000;
            float expected1 = 6.5f;
            int amount2 = 8000;
            float expected2 = 5.0f;
            int amount3 = 14000;
            float expected3 = 5.0f;
            //Act
            float actual1 = FirstQuestion.Discount(amount1);
            float actual2 = FirstQuestion.Discount(amount2);
            float actual3 = FirstQuestion.Discount(amount3);

            //Assert
            Assert.AreEqual(expected1, actual1);
            Assert.AreEqual(expected2, actual2);
            Assert.AreNotEqual(expected3, actual3);
        }

        
        [Test]
        [TestCase("+",10,20,30.0f)]
        [TestCase("-", 20, 10, 10.0f)]
        [TestCase("*", 20, 10, 200.0f)]
        [TestCase("/", 20, 10, 2.0f)]
        [TestCase("@", 20, 10, 0)]
        public void TestCalculator(string sign,int a,int b,float expected)
        {
            //Arrange
            //Act
            float actual = FirstQuestion.Calculator(sign,a,b);
            //Assert
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void TestSumOfN()
        {
            //Arrange
            int n = 10;
            int expected = 55;
            //Act
            int actual = FirstQuestion.SumOfN(n);
            //Assert
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestTotal()
        {
            //Arrange
            List<int> arr = new List<int>() {1,2,3,4,5,6 };
            int expected = 21;
            //Act
            int actual = FirstQuestion.Total(arr);
            //Assert
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestGetEmployee()
        {
            //Arrange
            int id = 7;

            //Act + Assert
            Assert.Throws<Exception>(() => ERP.GetEmployee(id));
        }
        [Test]
        public void TestGetEmployeeName()
        {
            //Arrange
            int id = 6;
            //Act + Assert
            Assert.Throws<NullReferenceException>(() => ERP.GetEmployeeName(id));
        }
        [Test]
        public void TestCalculatorException()
        {
            //Arrange
            int a = 10;
            int b = 0;
            //Act + Assert
            Assert.Throws<DivideByZeroException>(() => FirstQuestion.Calculator("/",a,b));
        }
        [Test]
        public async Task TestTotalEmployee()
        {
            //Arrange
            int expected = 4;
            //Act
            int actual = await ERP.TotalEmployee();
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public async Task TestGetHighestPaidEmployee()
        {
            //Arrange
            int expected = 50000;
            //Act
            Employee actual = await ERP.GetHighestPaidEmployee();
            Assert.AreEqual(expected, actual.Salary);
        }
    }
}
