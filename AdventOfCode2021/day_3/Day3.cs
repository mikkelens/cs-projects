using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

class Day3
{
    public static void ExecutePart1()
    {
        string path = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "day_3\\input.txt");
        string[] lines = File.ReadAllLines(path); // string array "lines" will in this puzzle be viewed essentially like a 2d char array

        string gammaBits = "";
        string epsilonBits = "";
        for (int x = 0; x < lines[0].Length; x++) // left to right
        {
            int lowCount = 0;
            int highCount = 0;
            for (int y = 0; y < lines.Length; y++) // top down
            {
                // counting occurances
                if (lines[y][x] == '0') lowCount++;
                else highCount++;
            }

            if (lowCount > highCount) // low most common
            {
                gammaBits += 0;
                epsilonBits += 1;
            }
            else // high most common
            {
                gammaBits += 1;
                epsilonBits += 0;
            }
        }

        int gammaRate = Convert.ToInt32(gammaBits, 2);
        int epsilonRate = Convert.ToInt32(epsilonBits, 2);
        Console.WriteLine($"Gamma rate is {gammaBits}, or {gammaRate}.");
        Console.WriteLine($"Epsilon rate is {epsilonBits}, or {epsilonRate}.");
        Console.WriteLine($"Multiplied: {gammaRate * epsilonRate}");
    }

    public static void ExecutePart2()
    {
        string path = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "day_3\\input.txt");
        string[] lines = File.ReadAllLines(path); // string array "lines" will in this puzzle be viewed essentially like a 2d char array

        int lineLenght = lines[0].Length;

        string oxygenGeneratorRatingBinary = "";
        string co2ScrubberRatingBinary = "";

        for (int k = 1; k > -1; k--) // oxygen means 1, co2 means 0
        {
            char iterationChar = k.ToString()[0]; // to char
            Console.WriteLine($"\nIteration char: {iterationChar}");
            List<string> remainingNumbers = lines.ToList();

            Console.WriteLine($"Starting lenght: {remainingNumbers.Count}");
            for (int position = 0; position < lineLenght; position++) // for each number in a line (should keep going untill only 1 number remains)
            {
                // tally up counts of each binary value in this position
                int zeroesAtPosition = 0;
                int onesAtPosition = 0;
                foreach (var number in remainingNumbers) // check entire list with this position
                {
                    char c = number[position];
                    if (c == '0') zeroesAtPosition++;
                    else if (c == '1') onesAtPosition++;
                }

                // get the keeper char
                char keeperChar;
                if (zeroesAtPosition == onesAtPosition)
                {
                    // if equal
                    keeperChar = iterationChar; // set to relevant iteration int
                }
                else
                {
                    // if not equal
                    char mostCommon = onesAtPosition > zeroesAtPosition ? '1' : '0';
                    char leastCommon = onesAtPosition < zeroesAtPosition ? '1' : '0';
                    keeperChar = iterationChar == '1' ? mostCommon : leastCommon;
                }

                // remove every line/number that doesn't have correct char at position
                for (int y = 0; y < remainingNumbers.Count; y++)
                {
                    // remove if not correct
                    if (remainingNumbers[y][position] != keeperChar)
                    {
                        //Console.WriteLine($"{remainingNumbers[y]} REMOVED.");
                        remainingNumbers.RemoveAt(y);
                        y--;
                    }
                    //else
                    //{
                    //    Console.WriteLine($"{remainingNumbers[y]} KEPT.");
                    //}
                }

                Console.WriteLine($"Remaining lenght: {remainingNumbers.Count}\n");
                if (remainingNumbers.Count == 1)
                {
                    break;
                }
            }

            if (k == 1) oxygenGeneratorRatingBinary = remainingNumbers[0];
            else co2ScrubberRatingBinary = remainingNumbers[0];
        }

        int oxygenGeneratorRating = Convert.ToInt32(oxygenGeneratorRatingBinary, 2);
        int co2ScrubberRating = Convert.ToInt32(co2ScrubberRatingBinary, 2);
        Console.WriteLine($"Oxygen generator rating is {oxygenGeneratorRatingBinary}, or {oxygenGeneratorRating}.");
        Console.WriteLine($"CO2 scrubber rating is {co2ScrubberRatingBinary}, or {co2ScrubberRating}.");
        Console.WriteLine($"Life support rating (multiplied): {oxygenGeneratorRating * co2ScrubberRating}");
    }
}

