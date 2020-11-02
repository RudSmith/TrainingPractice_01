using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIM_Task_01
{
    class RIM_Task_01
    {
        static void Main(string[] args)
        {
            // Reading initial gold amount from keyboard
            Console.WriteLine("Please, enter your amount of gold: ");
            // TryParse() will validate the input
            ulong.TryParse(Console.ReadLine(), out ulong userGoldAmount);

            // Gold to crystalls exchange rate
            const ulong exchangeRate = 50;

            Console.WriteLine($"Today`s gold to crystalls exchange rate: 1 crystall costs { exchangeRate } gold. \n");

            // Reading the amount of crystalls, that user wants to buy
            Console.WriteLine("Please, enter amount of crystalls you want to buy: ");
            ulong.TryParse(Console.ReadLine(), out ulong crystallsBuyingAmount);

            // Crystalls amount that user has
            ulong userCrystallsAmount = 0;

            // Outputting the result using ternary operator
            Console.WriteLine( exchangeRate * crystallsBuyingAmount > userGoldAmount 
                ? $"Not enougn money. Your balance: { userGoldAmount } gold and { userCrystallsAmount } crystalls. \n"
                : $"Success. Your balance: { userGoldAmount - exchangeRate * crystallsBuyingAmount } gold and { crystallsBuyingAmount } crystalls. \n");


            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
