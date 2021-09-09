using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Hangman
{
    class Program
    {
        private const int startXOffset = 3;
        private const int startYOffset = 1;

        private const int standardWait = 500; // time in ms
        private const int longWait = 2250;

        private const string title = "HANGMAN";
        private const char seperator = '=';

        private const char censorLetter = '_';
        private const char spaceLetter = ' ';

        private static int difficulty = 1;

        private static List<string[]> drawings = new()
        {
            new[] {
                "        ",
                "        ",
                "        ",
                "        ",
                "        ",
                "        ",
                "        "
            },
            new[]
            {
                "        ",
                "        ",
                "        ",
                "        ",
                "        ",
                "        ",
                "--------"
            },
            new[]
            {
                "        ",
                "        ",
                "        ",
                "        ",
                "        ",
                "|       ",
                "--------"
            },
            new[]
            {
                "        ",
                "        ",
                "        ",
                "|       ",
                "|       ",
                "|       ",
                "--------"
            },
            new[]
            {
                "        ",
                "|       ",
                "|       ",
                "|       ",
                "|       ",
                "|       ",
                "--------"
            },
            new[]
            {
                "____    ",
                "|/      ",
                "|       ",
                "|       ",
                "|       ",
                "|       ",
                "--------"
            },
            new[]
            {
                "______  ",
                "|/   |  ",
                "|       ",
                "|       ",
                "|       ",
                "|       ",
                "--------"
            },
            new[]
            {
                "______  ",
                "|/   |  ",
                "|       ",
                "|       ",
                "|       ",
                "|       ",
                "--------"
            },
            new[]
            {
                "______  ",
                "|/   |  ",
                "|    O  ",
                "|       ",
                "|       ",
                "|       ",
                "--------"
            },
            new[]
            {
                "______  ",
                "|/   |  ",
                "|    O  ",
                "|    |  ",
                "|       ",
                "|       ",
                "--------"
            },
            new[]
            {
                "______  ",
                "|/   |  ",
                "|    O  ",
                "|   /|  ",
                "|       ",
                "|       ",
                "--------"
            },
            new[]
            {
                "______  ",
                "|/   |  ",
                "|    O  ",
                "|   /|\\",
                "|       ",
                "|       ",
                "--------"
            },
            new[]
            {
                "______  ",
                "|/   |  ",
                "|    O  ",
                "|   /|\\",
                "|   /   ",
                "|       ",
                "--------"
            },
            new[]
            {
                "______  ",
                "|/   |  ",
                "|    O !",
                "|   /|\\",
                "|   / \\",
                "|       ",
                "--------"
            },
            new[]
            {
                "______  ",
                "|/   |  ",
                "|   x-x ",
                "|   /|\\",
                "|   / \\",
                "|       ",
                "--------"
            }
        };

        private static List<string> possibleWords = new()
        {
            "gaming",
            "fortnite",
            "the death of the author",
            "society",
            "fucking horseshit",
            "sussy baka",
            "bread",
            "pensive",
            "communism",
            "AAAAAAAAAA"
        };

        private static string word;
        private static string censorWord;
        private static string usedLetters;
        private static int remainingLetters;
        private static int mistakes;
        private static bool lastGuessWasInvalid;


        static void Main(string[] args)
        {
            ClearScreen();
            IntroText();
            Thread.Sleep(standardWait);
            PrintSentence("Press any key to continue.");
            WaitForInput();

            // OUTER LOOP (games)
            char inputRead;
            do
            {
                bool playing = true;
                bool winning = false;
                int randomWordIndex = new Random().Next(0, possibleWords.Count);
                word = possibleWords[randomWordIndex];
                censorWord = GenerateCensor();
                usedLetters = "";
                remainingLetters = word.Length;
                mistakes = 0;
                lastGuessWasInvalid = false;

                ClearScreen();
                PrintSentence("Declare what difficulty you want, or continue with a difficulty of 1 (press enter)");

                string difficultyInput;
                int difficultyInt;
                bool isInt;
                do
                {
                    difficultyInput = Console.ReadLine();
                    isInt = int.TryParse(difficultyInput, out int result);
                    difficultyInt = result;
                } while (!isInt && !string.IsNullOrWhiteSpace(difficultyInput));
                difficulty = isInt ? difficultyInt : difficulty;

                // INNER LOOP (attempts)
                while (playing)
                {
                    RefreshGameScreen();

                    char guess = Console.ReadKey().KeyChar;
                    if (!char.IsLetter(guess)) continue; // only continues if guess is letter
                    
                    if (usedLetters.ToLower().Contains(char.ToLower(guess)))
                    {
                        // previous guess
                        lastGuessWasInvalid = true;
                    }
                    else
                    {
                        lastGuessWasInvalid = false;
                        usedLetters = SortedString(usedLetters + guess);
                        if (word.ToLower().Contains(char.ToLower(guess)))
                        {
                            // good guess
                            AddLetterToCensor(guess);
                            if (censorWord == word.ToLower())
                            {
                                winning = true;
                                playing = false;
                            }
                        }
                        else
                        {
                            // bad guess
                            mistakes++;
                            PrintSentence($"{guess.ToString().ToUpper()} is not a part of the word.");
                            if (mistakes * difficulty >= drawings.Count)
                            {
                                playing = false;
                            }
                        }
                    }
                } // exits when done/won

                censorWord = word;
                RefreshGameScreen();

                if (winning)
                {
                    WinText();
                }
                else
                {
                    LoseText();
                }
                Thread.Sleep(longWait);


                PrintSentence("Do you want to play again? (Y/N)");

                do inputRead = Console.ReadKey().KeyChar;
                while (inputRead != 'y' && inputRead != 'n');

            } while (inputRead == 'y'); // end if we pressed N

            PrintSentence("Press any key to exit.", 0, 3);
            WaitForInput();
        }

        #region Print sections
        static void IntroText()
        {
            string display = $"{title}.";
            PrintSentence(new string(seperator, display.Length));
            PrintSentence(display);
            PrintSentence(new string(seperator, display.Length));
        }

        static void WinText()
        {
            PrintSentence("You won! Congratulations.", 0, 1);
        }

        static void LoseText()
        {
            PrintSentence("You lost. :^(", 0, 1);
        }
        #endregion

        #region Methods
        static void WaitForInput()
        {
            Console.ReadKey();
        }

        static void ClearScreen()
        {
            Console.Clear();
            Console.SetCursorPosition(startXOffset, startYOffset);
        }

        static void RefreshGameScreen()
        {
            ClearScreen();
            // - printing time! -
            PrintSentence("Guess a letter of the secret word!");
            PrintDrawing(drawings[Math.Min(mistakes * difficulty, drawings.Count - 1)]);
            PrintSentence(""); // (space)
            PrintSentence($"{remainingLetters} remaining letters.");
            PrintSentence(usedLetters.Length > 0 ? $"Letters guessed: {usedLetters}" : "No guesses yet...");
            PrintSentence(lastGuessWasInvalid ? $"You already guessed that letter." : "");
            PrintSentence(censorWord);
        }

        static void PrintDrawing(string[] drawing, int xOffset = 0, int yOffset = 0, bool newLine = true)
        {
            int xPosition = Math.Max(0, startXOffset + xOffset); // max(0) for floor
            int yPosition = Math.Max(0, Console.GetCursorPosition().Top + yOffset);

            for (int i = 0; i < drawing.Length; i++)
            {
                PrintSentence(drawing[i]);
            }
        }

        static void PrintSentence(string sentence, int xOffset = 0, int yOffset = 0, bool newLine = true)
        {
            int xPosition = Math.Max(0, startXOffset + xOffset); // max(0) for floor
            int yPosition = Math.Max(0, Console.GetCursorPosition().Top + yOffset);
            for (int i = 0; i < sentence.Length; i++)
            {
                PrintCharacter( xPosition + i, yPosition, sentence[i]);
            }

            int newLineOffset = newLine ? 1 : 0;
            Console.SetCursorPosition(xPosition, yPosition + newLineOffset);
        }

        static void PrintCharacter(int x, int y, char print)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(print);
        }

        static string SortedString(string thingy)
        {
            return string.Concat(thingy.OrderBy(c => c));
        }

        static void AddLetterToCensor(char letter)
        {
            string newBuild = "";
            for (int i = 0; i < word.Length; i++)
            {
                if (char.ToLower(word[i]) == char.ToLower(letter))
                {
                    newBuild += letter;
                    remainingLetters--;
                }
                else
                {
                    newBuild += censorWord[i];
                }
            }
            censorWord = newBuild;
        }

        static string GenerateCensor()
        {
            string censor = "";
            for (int i = 0; i < word.Length; i++)
            {
                censor += GetCensorChar(word[i]);
            }
            return censor;
        }

        static char GetCensorChar(char censor) => censor == spaceLetter ? spaceLetter : censorLetter;
        #endregion
    }
}
