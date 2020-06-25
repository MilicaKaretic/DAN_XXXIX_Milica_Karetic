using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAN_XXXIX_Milica_Karetic
{
    class Validation
    {
        /// <summary>
        /// Yes/no validation
        /// </summary>
        /// <returns></returns>
        public string YesNo()
        {
            string choose = Console.ReadLine().ToLower();

            //BackToMainMenu(choose);
            while (choose != "yes" && choose != "no")
            {
                Console.WriteLine("Invalid input. Try again: ");
                //BackToMainMenu(choose);
                choose = Console.ReadLine().ToLower();
            }

            return choose;
        }

        /// <summary>
        /// The word can not be empty
        /// </summary>
        /// <param name="word"> word that is being inspected </param>
        public void CheckIfNullOrEmpty(string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                Console.WriteLine("The input cannot be empty.\n");
            }
        }

        /// <summary>
        /// Valid positive int input
        /// </summary>
        /// <returns></returns>
        public int ValidPositiveNumber()
        {
            string s = Console.ReadLine();
            BackToMainMenu(s);
            int Num;
            bool b = Int32.TryParse(s, out Num);
            while (!b || Num < 0)
            {
                Console.WriteLine("Invalid input. Try again: ");
                s = Console.ReadLine();
                BackToMainMenu(s);
                b = Int32.TryParse(s, out Num);
            }
            return Num;
        }

        /// <summary>
        /// Returns the user back to the main menu
        /// </summary>
        /// <param name="input">input that returns the user</param>
        /// 
        public void BackToMainMenu(string input)
        {
            if (input.ToLower() == "return")
            {
                Program.Main(null);
            }
        }


        /// <summary>
        /// Validation for time format
        /// </summary>
        /// <returns>Song duration</returns>
        public string IsValidTimeFormat()
        {
            string time = Console.ReadLine();
            string format = "hh\\:mm\\:ss";
            bool validDuration = TimeSpan.TryParseExact(time, format, CultureInfo.CurrentCulture, out TimeSpan duration);
            
            while (!validDuration)
            {
                Console.WriteLine("Invalid input. Try again: ");
                time = Console.ReadLine();
                validDuration = TimeSpan.TryParseExact(time, format, CultureInfo.CurrentCulture, out duration);
            }
            return duration.ToString();
        }

        /// <summary>
        /// Method for validation empty input
        /// </summary>
        /// <param name="Prompt">Text for displey</param>
        /// <returns></returns>
        public string GetInput(string Prompt)
        {
            string result = "";
            do
            {
                Console.WriteLine(Prompt + ": ");
                result = Console.ReadLine();
                if (string.IsNullOrEmpty(result))
                {
                    Console.WriteLine("Empty input, please try again");
                }
            } while (string.IsNullOrEmpty(result));
            return result;
        }

    }
}
