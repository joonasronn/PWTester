using System;
using System.IO;
using System.Text.RegularExpressions;

namespace RegexP
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Insert your password and i'll tell you how strong it is!");
            string input = Console.ReadLine();
            RegexP(input);
        }

        private static void RegexP(string input)
        {
            //
            Regex rx = new Regex(@"", RegexOptions.Compiled);
            MatchCollection mc = rx.Matches(input);
            Console.WriteLine(CheckLenght(input));
            Console.WriteLine(CheckPwList(input));

        }

        private static string CheckPwList(string input)
        {
            string search = input.ToLower();
            using (StreamReader file = new StreamReader("Path to your rockyou.txt file"))
            {
                string line = file.ReadLine();
                while (line != null)
                {
                    if (line.Contains(search))
                    {
                        return "Your password is too common and easy to guess.";
                    }
                    line = file.ReadLine();
                }
                return "Nice. Your password is not common!";
            }
        }

        private static string CheckLenght(string input)
        {
            if (input.Length < 10)
                return "Your password is too short.";
            else if (input.Length <= 15)
                return "Your password could be longer.";
            else
                return "Nice. Atleast your password is long enough.";
        }
    }
}
