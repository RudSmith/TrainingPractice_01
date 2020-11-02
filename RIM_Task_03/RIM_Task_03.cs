using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIM_Task_03
{
    class RIM_Task_03
    {
        static void Main(string[] args)
        {
            // Setting the password
            const string password = "pantsOnHead";
            const string secretWord = "Mug";
            // Creating variable, that will store the amount of available password input attempts
            uint attemptsCount = 0;
            // Setting max amount of tries
            const uint maxAttempts = 3;

            // Repeating the loop while user still has attempts available
            while (attemptsCount < maxAttempts)
            {
                Console.WriteLine("Enter the password: ");
                string userInput = Console.ReadLine();

                // Checking password correctness
                if (userInput != password)
                {
                    ++attemptsCount;
                    Console.WriteLine($"Wrong password. Try again. You have { maxAttempts - attemptsCount } attempts left. \n");
                }
                else
                {
                    Console.WriteLine($"Correct password. Secret word is { secretWord } \n");
                    break;
                }
            }

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();

        }
    }
}
