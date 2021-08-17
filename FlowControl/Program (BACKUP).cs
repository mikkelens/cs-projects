using System;

namespace FlowControl101
{
    class Program
    {
        static void Main(string[] args)
        {
            int x = 0;
            int y = 0;
            int cooldown = -1;

            Console.CursorVisible = false;

            Console.SetCursorPosition(15, 7);
            Console.Write("X");

            ConsoleKey brian;

            do
            {
                cooldown--;

                Console.SetCursorPosition(x, y);
                Console.Write("P");

                brian = Console.ReadKey(true).Key;

                Console.SetCursorPosition(x, y);
                Console.Write(" ");

                if (brian == ConsoleKey.RightArrow)
                {
                    x++;
                }
                if (brian == ConsoleKey.DownArrow)
                {
                    y++;
                }

                if (x == 15 && y == 7)
                {
                    Console.SetCursorPosition(5, 14);
                    Console.Write("Du har samlet X op ...");
                    cooldown = 5;
                }

                if (cooldown == 0)
                {
                    Console.SetCursorPosition(5, 14);
                    Console.Write("                      ");
                }

            } while (brian != ConsoleKey.Escape);

            Console.SetCursorPosition(5, 14);
            Console.Write("Spillet afsluttes nu ... Tryk på en tast!");
            Console.ReadKey();
        }
    }
}
