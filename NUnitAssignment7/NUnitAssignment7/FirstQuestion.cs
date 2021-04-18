using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitAssignment7
{
    public static class FirstQuestion
    {
        public static int Factorial(int n)
        {
            int fact = 1;
            while (n > 1)
            {
                fact *= n;
                n -= 1;
            }
            return fact;
        }
        public static float Discount(int amount)
        {
            if (amount > 10000)
            {
                return 6.5f;
            }
            else{
                return 5.0f;
            }
        }
        public static float Calculator(string sign,int a,int b)
        {
            float result;
            switch (sign)
            {
                case "+":
                    result = a + b;
                    break;
                case "-":
                    result = a - b;
                    break;
                case "*":
                    result = a * b;
                    break;
                case "/":
                    result = a / b;
                    break;
                default:
                    result = 0;
                    break;
            }
            return result;
        }
        public static int SumOfN(int n)
        {
            int result=0;
            if (n > 0)
                for (int i = 1; i <= n; i++)
                {
                    result += i;
                }
            return result;
        }
        public static int Total(List<int> arr)
        {
            int result = 0;
            foreach(int i in arr)
            {
                result += i;
            }
            return result;
        }

    }
}
