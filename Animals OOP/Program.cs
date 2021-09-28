using System;

namespace Animals_OOP
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Startup
            Console.CursorVisible = false;
            #endregion

            #region Instantiation
            Dog doggy = new Dog("Fiddo", 16);
            Bunny bny = new Bunny("Mald", 1);
            #endregion

            #region Body
            Console.WriteLine(doggy.GetDescription());

            bool running = true;
            while (running)
            {
                Console.WriteLine("Press F to feed or H to see if the dog is hungry.");

                var keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.F) doggy.Feed();
                else if (keyInfo.Key == ConsoleKey.H)
                {
                    Console.WriteLine($"Dog is hungry? {doggy.IsHungry()}");
                }
                else if (keyInfo.Key == ConsoleKey.Escape) running = false;
            }
            #endregion

            #region End
            Console.Write("\n\n\nEnd of file. Press any key to exit.");
            Console.ReadKey();
            #endregion
        }
    }
}