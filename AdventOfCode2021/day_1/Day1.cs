using System;
using System.IO;

public class Day1
{
    public static void ExecutePart1()
    {
        string path = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "day_1\\input.txt");
        string[] lines = File.ReadAllLines(path);
        int[] numbers = Array.ConvertAll(lines, int.Parse);
        
        int increases = 0;
        int lastNumber = 0;
        foreach (int number in numbers)
        {
            if (lastNumber != 0 && lastNumber < number)
            {
                increases++;
            }
            lastNumber = number;
        }
        Console.WriteLine($"Individual increases: {increases}");
    }

    public static void ExecutePart2()
    {
        string path = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "day_1\\day_1_input.txt");
        string[] lines = File.ReadAllLines(path);
        int[] numbers = Array.ConvertAll(lines, int.Parse);

        int increases = 0;
        int lastSetSum = 0;
        for (int i = 0; i < numbers.Length - 2; i++) // iterates through all the first parts of the 3 sets
        {
            int setSum = numbers[i] + numbers[i + 1] + numbers[i + 2];
            if (setSum > lastSetSum && i != 0) increases++;
            lastSetSum = setSum;
        }
        Console.WriteLine($"Set sum increases: {increases}");
    }
}