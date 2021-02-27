using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringExtension
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = "Rahul";
            
            Console.WriteLine(s.ReverseCase());
            Console.WriteLine(s.TitleCase());
            Console.WriteLine(s.IsAllLower());
            Console.WriteLine(s.Capitalize());
            Console.WriteLine(s.IsAllUpper());
            Console.WriteLine(s.IsNumeric());
            Console.WriteLine(s.Capitalize());
            Console.WriteLine(s.RemoveLast());
            Console.WriteLine(s.WordCount());
            Console.WriteLine(s.ToInt());


        }
    }
}
