using System;
using System.Threading;

namespace Hangman
{
    class Program
    {
        private static int startXOffset = 5;
        private static int startYOffset = 2;

        private static int standardWait = 850; // time in ms

        private static string title = "HANGMAN";
        private static char seperator = '=';

        private static string word = "gaming";
        private static char censorLetter = '*';

        private static string censorWord;
        private static int remainingLetters;


        static void Main(string[] args)
        {
            ClearScreen();
            IntroText();
            Thread.Sleep(standardWait);
            PrintSentence("Press a key to continue.", startXOffset);
            WaitForInput();

            // OUTER LOOP (games)
            string inputRead;
            do
            {
                censorWord = new string(censorLetter, word.Length);
                // INNER LOOP (attempts)
                bool playing = true;
                while (playing)
                {
                    DisplayWord();

                    char guess = Console.ReadKey().KeyChar;

                    if (censorWord.Contains(guess))
                    {

                    }
                    else
                    {
                        bool guessedCorrect = word.Contains(guess);
                        if (guessedCorrect)
                        {
                            censorWord = GenerateNewCensor(censorWord, guess);
                            if (censorWord == word)
                            {
                                playing = false;
                            }
                        }
                    }
                } // exits when done/won
                
                DisplayWord();
                PrintSentence("You won! Congratulations.", startXOffset);
                Thread.Sleep(standardWait);

                PrintSentence("Do you want to play again? (Y/N)", startXOffset);

                do inputRead = Console.ReadKey().Key.ToString().ToLower();
                while (inputRead != "y" && inputRead != "n");

            } while (inputRead == "y"); // end if we pressed N

            Console.WriteLine("\n\n\nPress any key to exit.");
            WaitForInput();
        }

        #region Sections
        static void IntroText()
        {
            string display = $"{title}.";
            PrintSentence(display, startXOffset);
            PrintSentence(new string(seperator, display.Length), startXOffset);
            Console.WriteLine("\n");
        }
        static void WaitForInput()
        {
            Console.ReadKey();
        }
        #endregion

        #region Methods
        static void ClearScreen()
        {
            Console.Clear();
            Console.SetCursorPosition(startXOffset, startYOffset);
        }

        static void DisplayWord()
        {
            ClearScreen();
            PrintSentence("Guess a letter of the secret word!", startXOffset);
            PrintSentence(censorWord, startXOffset);
        }

        static void PrintSentence(string sentence, int xOffset, int yOffset = 0, bool newLine = true)
        {
            int xPosition = startXOffset + xOffset;
            int yPosition = Console.GetCursorPosition().Top + yOffset;
            for (int i = 0; i < sentence.Length; i++)
            {
                PrintCharacter( xPosition + i, yPosition, sentence[i]);
            }

            int newLineOffset = 1;
            if (!newLine) newLineOffset = 0;
            Console.SetCursorPosition(xPosition, yPosition + newLineOffset);
        }

        static void PrintCharacter(int x, int y, char print)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(print);
        }

        static string GenerateNewCensor(string oldCensor, char guess)
        {
            string censor = "";
            remainingLetters = word.Length;
            for (int i = 0; i < word.Length; i++)
            {
                if (word[i] == oldCensor[i]) // if we already guessed it
                {
                    censor += oldCensor[i];
                }
                else if (word[i] == guess) // newly guessed
                {
                    censor += word[i];
                }
                else // incorrect spot
                {
                    censor += censorLetter;
                    remainingLetters--; // updates remaining letters (number)
                }
            }
            return censor;
        }
        #endregion
    }
}
