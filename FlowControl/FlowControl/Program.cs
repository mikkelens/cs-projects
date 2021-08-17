using System;
using System.Linq;
using System.Collections.Generic;

namespace FlowControl101
{
    class Program
    {

        // GAMEPLAY SETTINGS
        public static Vector2Int areaSizes = new Vector2Int(16, 9);
        public static int growthValue = 2;

        // GRAPHICS
        public static int winShowTimer = 8;
        public static char headChar = 'S';
        public static char bodyChar = 'o';
        public static char clearChar = ' ';
        public static char DEBUGCHAR = '?';

        public static char horizontalBoundChar = '-';
        public static char verticalBoundChar = '|';

        public static char valuableChar = 'X';
        public static Vector2Int printPos = new Vector2Int(5, 14);

        // game data
        public static int winHideTime = -1;
        public static int bodyLenght = 1; // 1 is head only (KEEP AT 1 - ELSE IT CRASHES)
        public static Vector2Int headPos = new Vector2Int(0, 0);
        public static List<Vector2Int> currentPositions = new List<Vector2Int>() { new Vector2Int(0, 0) };
        public static List<Vector2Int> oldPositions = new List<Vector2Int>() { new Vector2Int(0, 0) };

        public static Vector2Int xpPos = PlaceRandomXP();

        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            DrawBounds(true);

            bool dead = false;

            Console.SetCursorPosition(headPos.X, headPos.Y);
            Console.Write(headChar);

            ConsoleKey inputKey;
            do // inputKey is not assigned yet
            {
                winHideTime--;

                Vector2Int newPos = headPos;
                inputKey = Console.ReadKey(true).Key;
                if (inputKey == ConsoleKey.RightArrow) newPos.X++;
                else if (inputKey == ConsoleKey.LeftArrow) newPos.X--;
                if (inputKey == ConsoleKey.DownArrow) newPos.Y++;
                else if (inputKey == ConsoleKey.UpArrow) newPos.Y--;

                // lock us to the play area
                newPos = new Vector2Int(
                    Math.Clamp(newPos.X, 0, areaSizes.X),
                    Math.Clamp(newPos.Y, 0, areaSizes.Y));

                // ensure we actually pressed a movement key                
                if ((newPos.X != headPos.X || newPos.Y != headPos.Y )) // ensure we can only move if going into empty, free space
                {
                    if (currentPositions.Contains(newPos)) // if we move into ourself, we die
                    {
                        dead = true;
                        continue;
                    }

                    // we successfully moved
                    headPos = newPos;

                    currentPositions[0] = headPos; // ensures compatibility with the clear-last-tile part below
                    // update body parts
                    for (int i = 0; i < bodyLenght; i++)
                    {
                        char bodyPart;
                        // a currentPositions vector2int is one index higher than its clone in oldPositions
                        if (i == 0)
                        {
                            bodyPart = headChar;
                        }
                        else
                        {
                            bodyPart = bodyChar;
                            currentPositions[i] = new List<Vector2Int>(oldPositions)[i - 1];
                        }
                        // clear previous last
                        Console.SetCursorPosition(oldPositions[i].X, oldPositions[i].Y);
                        Console.Write(clearChar);

                        // write new
                        Console.SetCursorPosition(currentPositions[i].X, currentPositions[i].Y);
                        Console.Write(bodyPart);
                    }
                    

                    // WIN STATES ETC
                    if (headPos.X == xpPos.X && headPos.Y == xpPos.Y)
                    {
                        // longer snek
                        bodyLenght += growthValue;
                        for (int v = 0; v < growthValue; v++)
                        {
                            currentPositions.Add(currentPositions.Last());
                        }

                        // spawn new
                        xpPos = PlaceRandomXP();

                        // congratulate
                        ShowWin(true);
                        winHideTime = winShowTimer; // shows win text for 5 input "frames"
                    }

                    oldPositions = new List<Vector2Int>(currentPositions);
                }
                if (winHideTime == 0) ShowWin(false);

            } while (inputKey != ConsoleKey.Escape && !dead); // inputKey is assigned

            Console.SetCursorPosition(printPos.X, printPos.Y);
            Console.Write("GAME OVER. Press any button to exit.                ");
            Console.ReadKey();
        } // main

        static Vector2Int PlaceRandomXP()
        {
            // get random seed
            Random r = new Random();

            // make random vector2int
            Vector2Int xpPos;
            do
            {
                xpPos = new Vector2Int(r.Next(0, areaSizes.X + 1), r.Next(0, areaSizes.Y + 1));
            }
            while (currentPositions.Contains(xpPos)); // ensuring it doesn't spawn in us

            // display vector2int
            Console.SetCursorPosition(xpPos.X, xpPos.Y);
            Console.Write(valuableChar);
            // return to keep position data
            return xpPos;
        } // placeXP

        static void DrawBounds(bool draw)
        {
            Vector2Int areaBounds = new Vector2Int(areaSizes.X + 1, areaSizes.Y + 1);
            // decide to draw or clear
            char horizontalDraw = draw ? horizontalBoundChar : clearChar;
            char verticalDraw = draw ? verticalBoundChar : clearChar;

            // bottom
            for (int x = 0; x < areaBounds.X; x++)
            {
                Console.SetCursorPosition(x, areaBounds.Y);
                Console.Write(horizontalDraw);
            }
            // side
            for (int y = 0; y <= areaBounds.Y; y++)
            {
                Console.SetCursorPosition(areaBounds.X, y);
                Console.Write(verticalDraw);
            }
        }

        static void ShowWin(bool display)
        {
            if (display)
            {
                Console.SetCursorPosition(printPos.X, printPos.Y);
                Console.Write("You picked up a '" + valuableChar + "'! (fruit)");
            }
            else
            {
                Console.SetCursorPosition(printPos.X, printPos.Y);
                Console.Write("                                               ");
            }
        } //showWin
    }

    [Serializable]
    public struct Vector2Int
    {
        public int X;
        public int Y;

        public Vector2Int(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
