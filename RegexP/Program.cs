using System;
using System.IO;
using System.Text.RegularExpressions;

namespace RegexP
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
            Console.WriteLine("Insert your password and i'll tell you how strong it is!");
            string input = Console.ReadLine();
            RegexP(input);
            Console.WriteLine("Press Enter/Return to try again. Press anything else to quit.");
            } while (Console.ReadKey().Key == ConsoleKey.Enter);
        }

        private static void RegexP(string input)
        {
            int score = CheckLenght(input) + CheckPwList(input) + CheckForSpecialCharacters(input) + CheckForUpperCase(input);
            Console.WriteLine("Your password is "+score+"//10");
        }
        private static int CheckForSpecialCharacters(string input)
        {
            Regex rx = new Regex(@"!@#$%^&*(),.?':{ }|<>]", RegexOptions.Compiled);
            MatchCollection mc = rx.Matches(input);

            Regex rx2 = new Regex(@"[0-9]", RegexOptions.Compiled);
            MatchCollection mc2 = rx.Matches(input);

            if (mc.Count == 1 && mc2.Count == 0)
            {
                Console.WriteLine("Your password contains a special character. Add more special characters and numbers for complexity.");
                return 1;
            }
            if (mc2.Count == 1 && mc.Count == 0)
            {
                Console.WriteLine("Your password contains a number. Add more numbers and special characters for complexity.");
                return 1;
            }
            if (mc.Count == 1 && mc2.Count == 1)
            {
                Console.WriteLine("Your password contains a special character and a number. Add more for complexity.");
                return 2;
            }
            if (mc.Count > 1 && mc2.Count == 1)
            {
                Console.WriteLine("Your password contains special characters and a number. Add more for complexity.");
                return 3;
            }
            if (mc.Count == 1 && mc2.Count > 1)
            {
                Console.WriteLine("Your password contains a special character and numbers. Add more for complexity.");
                return 3;
            }
            if (mc.Count > 1 && mc2.Count > 1)
            {
                Console.WriteLine("Your password contains multiple special characters and numbers. Good job!");
                return 4;
            }
            Console.WriteLine("Your password contains no special characters.");
            return 0;
        }

        private static int CheckForUpperCase(string input)
        {
            Regex rx = new Regex(@"\p{Lu}", RegexOptions.Compiled);
            MatchCollection mc = rx.Matches(input);
            if (mc.Count == 1)
            {
                Console.WriteLine("Your password contains an upper case letter. Try to add more to add complexity.");
                return 1;
            }
            if (mc.Count > 1)
            {
                Console.WriteLine("Your password contains upper case letters. Good job!");
                return 2;
            }
            Console.WriteLine("Your password contains no upper case letters.");
            return 0;
        }

        private static int CheckPwList(string input)
        {
            Regex regex = new Regex(@"[0-9]|!@#$%^&*(),.?':{ }|<>");
            string noNumbersAndSpecials = regex.Replace(input, "");

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
            using (StreamReader file = new StreamReader(@"Add your wordlist like rockyou.txt here!"))
            {
                string line = file.ReadLine();
                while (line != null)
                {
                    if (line.Contains(search) || line.Contains(input))
                    {
                        Console.WriteLine("Your password is too common and easy to guess.");
                        return 0;
                    }
                    if (line.Contains(noNumbersAndSpecials))
                    {
                        Console.WriteLine("Your password contains common words and some extra characters. Better than plain text but still not unique.");
                        return 1;
                    }
                    if (line.Contains(numberCharacters))
                    {
                        Console.WriteLine("Your password contains common words and numbers replacing characters. Better than plain text but still not unique.");
                        return 1;
                    }
                    line = file.ReadLine();
                }
                Console.WriteLine("Nice. Your password is not common!");
                return 2;
            }
        }

        private static int CheckLenght(string input)
        {
            if (input.Length < 10)
            {
                Console.WriteLine("Your password is too short.");
                return 0;
            }
            else if (input.Length <= 15)
            {
                Console.WriteLine("Your password could be longer.");
                return 1;
            }
            else
            {
                Console.WriteLine("Nice. Your password is long enough.");
                return 2;
            }
        }
    }
}
