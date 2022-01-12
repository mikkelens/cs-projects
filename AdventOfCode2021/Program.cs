using System;
using System.Threading;

namespace AdventOfCode2021
{
    class Program
    {
        static void Main(string[] args)
        {
            //Day1.ExecutePart1();
            //Day1.ExecutePart2();
            //Day2.ExecutePart1();
            //Day2.ExecutePart2();
            //Day3.ExecutePart1();
            //Day3.ExecutePart2();
            Day4.ExecutePart1();

            Thread.Sleep(750);
            Console.CursorVisible = false;
            Console.Write("\n\n\nPress any key to exit.");
            Console.ReadKey();
        }
    }
}
