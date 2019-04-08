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
            Regex regex = new Regex(@"[0-9|!@#$%^&*(),.?':{ }|<>]");
            string noNumbersAndSpecials = regex.Replace("input", "");

            string numberCharacters = input;

            numberCharacters.Replace("0", "o");
            numberCharacters.Replace("1", "i");
            numberCharacters.Replace("2", "z");
            numberCharacters.Replace("3", "e");
            numberCharacters.Replace("4", "a");
            numberCharacters.Replace("5", "s");
            numberCharacters.Replace("6", "b");
            numberCharacters.Replace("7", "t");
            numberCharacters.Replace("8", "b");
            numberCharacters.Replace("9", "q");

            string search = input.ToLower();
            using (StreamReader file = new StreamReader(@"Your rockyou.txt path here"))
            {
                string line = file.ReadLine();
                while (line != null)
                {
                    if (line.Contains(search))
                    {
                        return "Your password is too common and easy to guess.";
                    }
                    if (line.Contains(noNumbersAndSpecials))
                    {
                        return "Your password contains common words and some extra characters. Better than plain text but still not unique.";
                    }
                    if (line.Contains(numberCharacters))
                    {
                        return "Your password contains common words and numbers replacing characters. Better than plain text but still not unique.";
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
