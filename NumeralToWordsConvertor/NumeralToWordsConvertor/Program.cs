using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumeralToWordsConvertor
{
    class Program
    {
        static Dictionary<int, string> numerals = new Dictionary<int, string> { {0, "zero"}, { 1,"one"}, {2,"two"},{ 3, "three" },{ 4, "four" },
                                                                              { 5, "five" }, { 6, "six" },{ 7, "seven" },{ 8, "eight" },{ 9, "nine"},
                                                                              { 10, "ten" },{ 11, "eleven" },{ 12, "twelve" },{ 13, "thirteen" },{ 14, "fourteen" },
                                                                              { 15, "fifteen" },{ 16, "sixteen" },{ 17, "seventeen" },{ 18, "eighteen" },{ 19, "nineteen" },
                                                                              { 20, "twenty" },{ 30, "thirty" },{ 40, "forty" },{ 50, "fifty" }, { 60, "sixty" },
                                                                              { 70, "seventy" },{ 80, "eighty" },{ 90, "ninety" }};
        static Int32[] specialNumbers = new Int32[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 30, 40, 50, 60, 70, 80, 90 };
        static void Main(string[] args)
        {
            Console.WriteLine("Enter number for conversion to words. Press 000 to exit.\n");
            while (true)
            {
                string userInput = Console.ReadLine();
                userInput = userInput.Replace(",", "");
                if (userInput == "000")
                {
                    Environment.Exit(-1);
                }
                int result;
                bool validInput = int.TryParse(userInput, out result);
                if (validInput)
                {
                    Console.WriteLine(NumeralToWords(result));
                }
                else
                {
                    Console.WriteLine("Invalid Input.");
                }
            }
        }

        public static string NumeralToWords(int number)
        {
            string words = String.Empty;
            if (specialNumbers.Contains(number))
            {
                return GetWordforNumber(number);
            }
            else if (number < 100)
            {
                Int32 tensDigit = number / 10;
                Int32 unitDigit = number % 10;
                words = GetWordforNumber(tensDigit * 10) + " " + GetWordforNumber(unitDigit);
            }

            else if (number < 1000)
            {
                words = NumberLessThanThousand(number);
            }
            else if (number < 1000000)
            {
                words = NumberLessThanMillion(number);
            }
            else if (number < 1000000000)
            {
                words = NumberLessThanBillion(number);
            }
            else
                words = "Invalid Input";
            return words;
        }

        public static string NumberLessThanThousand(int number)
        {
            string words = "";
            Int32 hundredDigits = number / 100;
            if (hundredDigits > 0)
            {
                words = GetWordforNumber(hundredDigits) + " hundred ";
            }
            if (number % 100 > 0)
            {
                Int32 tens = number - hundredDigits * 100;
                if (specialNumbers.Contains(tens))
                {
                    words = (String.IsNullOrEmpty(words) ? "" : words + "and ") + GetWordforNumber(tens);
                }
                else
                {
                    Int32 tensDigit = tens / 10;
                    Int32 unitDigit = number % 10;
                    words = (String.IsNullOrEmpty(words) ? "" : words + "and ") + GetWordforNumber(tensDigit * 10) + " " + GetWordforNumber(unitDigit);
                }
            }
            return words;
        }

        public static string NumberLessThanMillion(int number)
        {
            string words = "";
            Int32 thousandDigits = number / 1000;
            if (thousandDigits > 0)
            {
                if (specialNumbers.Contains(thousandDigits))
                {
                    words = GetWordforNumber(thousandDigits) + " thousand ";
                }
                else
                {
                    words = NumberLessThanThousand(thousandDigits) + " thousand ";
                }
            }
            Int32 otherThanThousand = number % 1000;
            if (otherThanThousand > 0)
            {
                words = words + NumberLessThanThousand(otherThanThousand);
            }
            return words;
        }

        public static string NumberLessThanBillion(int number)
        {
            string words;
            Int32 millionDigits = number / 1000000;
            if (specialNumbers.Contains(millionDigits))
            {
                words = GetWordforNumber(millionDigits) + " million ";
            }
            else
            {
                words = NumberLessThanThousand(millionDigits) + " million ";
            }
            Int32 otherThanMillion = number % 1000000;
            if (otherThanMillion > 0)
            {
                words = words + NumberLessThanMillion(otherThanMillion);
            }
            return words;
        }

        public static string GetWordforNumber(int digit)
        {
            if (numerals.Keys.Contains(digit))
                return numerals[digit];
            else
                return String.Empty;
        }
    }
}
