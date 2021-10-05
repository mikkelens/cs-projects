using System;
using System.Threading;

namespace ClassesOOP
{
    class Program
    {
        static void Main(string[] args)
        {
            Chair chair = new Chair();
            Console.WriteLine($"Chair parts: {chair.GetPartNames()}");

            EndProgram();
        }

        // program execution0
        public static void EndProgram()
        {
            Thread.Sleep(500);
            Console.WriteLine("\n\n\nPress any key to exit the program.");
            Console.ReadKey();
        }
    }
}