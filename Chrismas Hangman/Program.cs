using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;

namespace Christmas_Hangman
{
    class Program
    {
        public static char Censor = '*';
        public static int MaxWrongGuesses = 3;

        public static int WinCounter = 0;
        private static string _currentWord; // chosen from christmasWords
        private static string _allGuesses = "";
        private static string _correctGuesses = "";
        private static int _wrongGuessCount = 0;

        public static List<string> christmasWords = new List<string>
        {
            "santa",
            "jesus",
            "your mom",
            "gifts"
        };

        static void Main(string[] args)
        {
            Random random = new Random();

            // BEGIN GAME
            Console.Clear();
            Thread.Sleep(400);
            Console.WriteLine("Press enter to begin. [Write 'ESC' to exit program]");
            bool playing = Console.ReadLine()?.ToUpper() != "ESC";
            while (playing) // only exited by "break;"
            {
                _currentWord = christmasWords[random.Next(0, christmasWords.Count)];
                _allGuesses = "";
                _correctGuesses = "";
                _wrongGuessCount = 0;
                
                while (true) // only exited by "break;"
                {
                    string displayWord = CensoredWord(_currentWord, _correctGuesses);
                    if (displayWord.ToLower() == _currentWord.ToLower())
                    {
                        WinCounter++;
                        PrintWin();
                        break;
                    }
                    if (_wrongGuessCount >= MaxWrongGuesses)
                    {
                        PrintLoss();
                        break;
                    }
                    // NEW ATTEMPT/GUESS
                    Console.Clear();
                    Console.WriteLine($"WORD: {displayWord}\n");
                    Console.WriteLine($"All the letters you have guessed: {_allGuesses}");
                    Console.WriteLine("Take a guess at a letter in the word. [Write 'ESC' to exit]\n");
                    string guess = Console.ReadLine();
                    Console.Clear();
                    if (guess.Length == 1)
                    {
                        CheckGuess(guess[0], _currentWord);
                    }
                    else if (guess.ToUpper() == "ESC")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"You can only guess one letter at a time. Your guess: {guess}");
                    }
                    Thread.Sleep(1250);
                }

                // end of a game
                Console.ForegroundColor = ConsoleColor.White;
                Thread.Sleep(250);
                Console.WriteLine($"The word was: {_currentWord}");
                Thread.Sleep(250);
                Console.WriteLine($"Total wins since program start: {WinCounter}");

                // promt to play again
                Thread.Sleep(1250);
                Console.WriteLine("\n\n\nPress enter to play again. [Write 'ESC' to exit program]");
                if (Console.ReadLine().ToUpper().Contains('N'))
                {
                    break;
                }
            }

            // end of program
            Console.Clear();
            Thread.Sleep(500);
            Console.WriteLine("End of program. Press any key to exit.");
            Console.ReadKey();
        }
        
        private static void PrintWin()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("You won!");
        }
        private static void PrintLoss()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You lost D:");
        }

        static string CensoredWord(string word, string correctGuesses)
        {
            string censoredWord = "";
            foreach (char character in word)
            {
                if (correctGuesses.Contains(character) || character == ' ')
                {
                    censoredWord += character;
                }
                else
                {
                    censoredWord += Censor;
                }
            }
            return censoredWord;
        }

        static void CheckGuess(char guess, string word)
        {
            string printString = $"{guess} ";
            if (guess == ' ')
            {
                printString += "is a space and that is included in the string";
            }
            else if (_allGuesses.Contains(guess))
            {
                printString += "is a letter you have already guessed.";
            }
            else if (word.Contains(guess))
            {
                printString += "is a letter in the string, yes!";
                _correctGuesses += guess;
                _allGuesses += guess;
            }
            else
            {
                printString += "is not a letter in the string.";
                _wrongGuessCount++;
                _allGuesses += guess;
            }
            Console.WriteLine(printString);
        }
    }
}
