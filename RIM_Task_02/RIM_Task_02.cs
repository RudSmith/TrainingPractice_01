using System;

namespace RIM_Task_02
{
    class RIM_Task_02
    {
        static void Main(string[] args)
        {
            // Setting the keyword to exit from loop
            const string keyWord = "exit";
            string inputWord = "";

            // Loop continues if keyword was entered incorrectly
            while(inputWord != keyWord)
            {
                Console.WriteLine("Enter the keyword: ");
                inputWord = Console.ReadLine();
                Console.WriteLine();
            }

            // When loop is ended, outputting the message, that keyword was correct
            Console.WriteLine("You entered correct keyword. Exiting from loop. Press any key to continue.");
            Console.ReadKey();

        }
    }
}
