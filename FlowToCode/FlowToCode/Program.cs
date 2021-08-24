using System;

namespace FlowToCode
{
    class Program
    {
        // PROGRAM //
        static void Main(string[] args)
        {
            PrintNumbers();

            Console.WriteLine("\nPress any key to exit.");
            Console.ReadKey();
        }

        // ASSIGNMENT //
        static void PrintNumbers()
        {
            Console.WriteLine("--- ALL NUMBERS ---");
            for (int number = 1; number < 10; number++)
            {
                if (number != 3 && number != 5) // number *isnt* 3 AND *isnt* 5
                    Console.WriteLine($"Number: {number}.");
                else // number *is* 3 OR is 5
                    Console.WriteLine($"BANNED NUMBER. ({number})");
            }
            Console.WriteLine("--- END ---");
        }
    }
}
