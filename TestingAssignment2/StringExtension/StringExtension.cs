using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringExtension
{
    public static class StringExtension
    {
        public static string ReverseCase(this string inputString)
        {
            char[] charArray = inputString.ToCharArray();
            for (int i= 0; i < charArray.Length;i++)
            {
                if (char.IsUpper(charArray[i]))
                {
                    charArray[i] = char.ToLower(charArray[i]);
                }
                else
                {
                    charArray[i] = char.ToUpper(charArray[i]);
                }
            }
            return new string(charArray);
        }
        public static string TitleCase(this string inputString)
        {
            if (inputString == null) return inputString;

            String[] words = inputString.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length == 0) continue;

                Char firstChar = Char.ToUpper(words[i][0]);
                String rest = "";
                if (words[i].Length > 1)
                {
                    rest = words[i].Substring(1).ToLower();
                }
                words[i] = firstChar + rest;
            }
            return String.Join(" ", words);
        }
        public static bool IsAllLower(this string inputString)
        {
            char[] charArray = inputString.ToCharArray();
            for (int i = 0; i < charArray.Length; i++)
            {
                if (char.IsUpper(charArray[i]))
                {
                    return false;
                }
                else
                {
                    continue;
                }
            }
            return true;
        }
        public static string Capitalize(this string inputString)
        {
            string newString= inputString.ToLower();
            char[] charArray = newString.ToCharArray();
            charArray[0] = char.ToUpper(charArray[0]);
            return new string(charArray);
        }
        public static bool IsAllUpper(this string inputString)
        {
            char[] charArray = inputString.ToCharArray();
            for (int i = 0; i < charArray.Length; i++)
            {
                if (char.IsLower(charArray[i]))
                {
                    return false;
                }
                else
                {
                    continue;
                }
            }
            return true;
        }
        public static bool IsNumeric(this string inputString)
        {
            return inputString.All(char.IsDigit);
        }
        public static string RemoveLast(this string inputString)
        {
            return inputString.Remove(inputString.Length-1,1);
        }
        public static int WordCount(this string inputString)
        {
            if (inputString == null) return 0;

            String[] words = inputString.Split(' ');

            return words.Length;
        }
        public static int ToInt(this string inputString)
        {
            try
            {
                return Convert.ToInt32(inputString);
            }
            catch
            {
                return 0;
            }
        }
    }
}
