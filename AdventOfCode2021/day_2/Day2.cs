using System;
using System.IO;
using System.Numerics;

public class Day2
{
    public static void ExecutePart1()
    {
        string path = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "day_2\\input.txt");
        string[] lines = File.ReadAllLines(path);

        Vector2 position = Vector2.Zero; // Y is depth, not altitude
        foreach (string command in lines)
        {
            // get the number first for convenience
            int numberStartIndex = 0;
            for (int c = 0; c < command.Length; c++)
            {
                if (command[c] == ' ')
                {
                    numberStartIndex = c + 1;
                }
            }
            int number = int.Parse(command[numberStartIndex..command.Length]);
            if (command.Contains("forward")) position.X += number;
            else if (command.Contains("down")) position.Y += number;
            else if (command.Contains("up")) position.Y -= number;
        }
        Console.WriteLine($"End horizontal ({position.X}) multiplied by end depth ({position.Y}) is {position.X * position.Y}");
    }

    public static void ExecutePart2()
    {
        string path = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "day_2\\input.txt");
        string[] lines = File.ReadAllLines(path);

        Vector2 position = Vector2.Zero; // Y is depth, not altitude
        int aim = 0;
        foreach (string command in lines)
        {
            // get the number first for convenience
            int numberStartIndex = 0;
            for (int c = 0; c < command.Length; c++)
            {
                if (command[c] == ' ')
                {
                    numberStartIndex = c + 1;
                }
            }
            int x = int.Parse(command[numberStartIndex..command.Length]);
            if (command.Contains("forward"))
            {
                position.X += x;
                position.Y += aim * x;
            }
            else if (command.Contains("down")) aim += x;
            else if (command.Contains("up")) aim -= x;
        }
        Console.WriteLine($"End horizontal ({position.X}) multiplied by end depth ({position.Y}) is {position.X * position.Y}");
    }
}