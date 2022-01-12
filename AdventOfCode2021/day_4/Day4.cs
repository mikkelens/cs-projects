using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Day4
{
    public static string path = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "day_4\\input.txt");
    public static List<string> allLines = File.ReadAllLines(path).ToList();

    public static void ExecutePart1()
    {
        List<int> numbers = GetNumbersInOrder();
        List<List<List<int>>> allBoards = GetBingoBoards();

        List<List<int>> bingoBoard = new List<List<int>>();

        List<int> announcedNumbers = new List<int>();
        foreach (int number in numbers)
        {
            announcedNumbers.Add(number);

            foreach (List<List<int>> board in allBoards)
            {
                // vertical bingo
                for (int x = 0; x < board[0].Count; x++)
                {
                    // check this column for bingo
                    for (int y = 0; y < board.Count; y++)
                    {
                        if (!announcedNumbers.Contains(board[x][y]))
                        {
                            break;
                        }

                        bingoBoard = board;
                    }
                }
                // horizontal bingo
                for (int y = 0; y < board.Count; y++)
                {
                    // check this row for bingo
                    for (int x = 0; x < board[0].Count; x++)
                    {
                        if (!announcedNumbers.Contains(board[y][x]))
                        {
                            break;
                        }

                        bingoBoard = board;
                        // quit if number not announced
                    }
                }

                // todo: fix something above here - it always thinks the first one wins with only one number announced??

                if (bingoBoard.Count != 0)
                {
                    Console.WriteLine("Bingo board found!");
                    break;
                }
            }

            if (bingoBoard.Count != 0) break;
        }

        Console.WriteLine($"Amount of numbers announced at the end: {announcedNumbers.Count}");

        int unmarkedSum = 0;
        for (int x = 0; x < bingoBoard[0].Count; x++)
        {
            for (int y = 0; y < bingoBoard.Count; y++)
            {
                // sum up unmarked numbers
                if (!announcedNumbers.Contains(bingoBoard[x][y]))
                {
                    unmarkedSum += bingoBoard[x][y];
                }
            }
        }

        int score = unmarkedSum * announcedNumbers.Last();
        Console.WriteLine($"Score: {score}");
    }

    static List<int> GetNumbersInOrder()
    {
        List<int> numbersInOrder = new List<int>();
        string currentNumberString = "";
        string numberListString = allLines[0];
        foreach (char character in numberListString)
        {
            if (character != ',') currentNumberString += character;
            else
            {
                 numbersInOrder.Add(int.Parse(currentNumberString));
                 currentNumberString = "";
            }
        }
        return numbersInOrder;
    }

    static List<List<List<int>>> GetBingoBoards()
    {
        List<string> linesWithoutTopNumbers = allLines.ToList();
        linesWithoutTopNumbers.RemoveAt(0);

        List<List<List<int>>> allBoards = new List<List<List<int>>>();

        List<List<int>> currentBoard = new List<List<int>>();
        foreach (string line in linesWithoutTopNumbers)
        {
            if (line == "") // seperation of boards
            {
                if (currentBoard.Count != 0)
                {
                    allBoards.Add(currentBoard);
                    currentBoard = new List<List<int>>();
                }
                continue;
            }

            // board creation
            List<int> horizontalValues = new List<int>();
            string currentNumberString = "";
            foreach (char character in line)
            {
                if (character != ' ')
                {
                    // new digit to number string
                    currentNumberString += character;
                }
                else if (currentNumberString != "")
                {
                    // add number string to total (horizontal number list) as number (int)
                    horizontalValues.Add(int.Parse(currentNumberString));
                    currentNumberString = "";
                }
            }
            horizontalValues.Add(int.Parse(currentNumberString));
            currentBoard.Add(horizontalValues);
        }
        return allBoards;
    }
}