using System;
using System.Linq;
using Xunit;

namespace StringExtension.Tests
{
    public class StringExtensionTest
    {
        [Fact]
        public void TestReverseCase()
        {
            string inputString = "Rahul";
            string expected = "rAHUL";
            string actual = inputString.ReverseCase();
            Assert.Equal(actual, expected);
        }
        [Fact]
        public void TestTitleCase()
        {
            string inputString = "rahul patel";
            string expected = "Rahul Patel";
            string actual = inputString.TitleCase();
            Assert.Equal(actual, expected);
        }
        
        [Fact]
        public void TestIsAllLowerTrue()
        {
            string inputString = "rahul";
            bool expected = true;
            bool actual = inputString.IsAllLower();
            Assert.True(actual==expected);
        }
        [Fact]
        public void TestIsAllLowerFalse()
        {
            string inputString = "Rahul";
            bool expected = false;
            bool actual = inputString.IsAllLower();
            Assert.True(actual == expected);
        }
        [Fact]
        public void TestCapitalize()
        {
            string inputString = "Rahul";
            string expected = "Rahul";
            string actual = inputString.Capitalize();
            Assert.True(actual == expected);
        }
        [Fact]
        public void TestIsAllUpperTrue()
        {
            string inputString = "RAHUL";
            bool expected = true;
            bool actual = inputString.IsAllUpper();
            Assert.True(actual == expected);
        }
        [Fact]
        public void TestIsAllUpperFalse()
        {
            string inputString = "Rahul";
            bool expected = false;
            bool actual = inputString.IsAllUpper();
            Assert.True(actual == expected);
        }
        [Fact]
        public void TestIsNumericTrue()
        {
            string inputString = "123";
            bool expected = true;
            bool actual = inputString.IsNumeric();
            Assert.True(actual == expected);
        }
        [Fact]
        public void TestIsNumericFalse()
        {
            string inputString = "1qwer";
            bool expected = false;
            bool actual = inputString.IsNumeric();
            Assert.True(actual == expected);
        }
        [Fact]
        public void TestRemoveLast()
        {
            string inputString = "Rahul";
            string expected = "Rahu";
            string actual = inputString.RemoveLast();
            Assert.True(actual == expected);
        }
        [Fact]
        public void TestWordCount()
        {
            string inputString = "Rahul patel";
            int expected = 2;
            int actual = inputString.WordCount();
            Assert.True(actual == expected);
        }
        [Fact]
        public void TestToIntCanConvert()
        {
            string inputString = "123";
            int expected = 123;
            int actual = inputString.ToInt();
            Assert.True(actual == expected);
        }
        [Fact]
        public void TestToIntCannotConvert()
        {
            string inputString = "Rahul";
            int expected = 0;
            int actual = inputString.ToInt();
            Assert.True(actual == expected);
        }
    }

}
