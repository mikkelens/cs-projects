using System;
using System.Collections.Generic;
using System.Threading;

namespace ListsAndInterfaces
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> Weekdays = new List<string>();
            Weekdays.Add("Monday");
            Weekdays.Add("Tuesday");
            Weekdays.Add("Wendnesday");
            Weekdays.Add("Thursday");
            Weekdays.Add("Friday");
            Weekdays.Add("Saturday");
            Weekdays.Add("Sunday");

            Console.WriteLine($"My list: {ListAsString(Weekdays)}");

            for (int i = 0; i < 8; i++)
            {
                Console.WriteLine(Weekdays[i % Weekdays.Count]);
            }

            End();
        }

        #region Execution
        static void End()
        {
            Thread.Sleep(500);
            Console.Write("\n\n\nEnd of program. Press any key to exit.");
            Console.ReadKey();
        }
        #endregion

        #region Printing methods
        static string ArrayAsString<T>(T[] array)
        {
            return string.Join(", ", array);
        }

        static string ListAsString<T>(List<T> list)
        {
            return string.Join(", ", list);
        }
        #endregion
    }
}
