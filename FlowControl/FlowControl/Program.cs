using System;
using System.Numerics;

namespace FlowControl101
{
    class Program
    {
        public static Vector2Int printPos = new Vector2Int(5, 14);

        static void Main(string[] args)
        {
            int spawnDiameter = 10;
            int cooldown = -1;

            Vector2Int xpPos = PlaceRandomXP(spawnDiameter);

            // player
            char headChar = 'p';
            char bodyChar = 'o';
            int bodyLenght = 1; // 1 is head only
            Vector2Int headPos = new Vector2Int(0, 0);
            Vector2Int[] currentPositions = new Vector2Int[] {};
            Vector2Int[] oldPositions;
            Console.SetCursorPosition(headPos.X, headPos.Y);
            Console.Write(headChar);

            ConsoleKey inputKey;
            do // inputKey is not assigned yet
            {
                cooldown--;
                oldPositions = currentPositions;

                inputKey = Console.ReadKey(true).Key;
                if (inputKey == ConsoleKey.RightArrow) headPos.X++;
                else if (inputKey == ConsoleKey.LeftArrow && headPos.X > 0) headPos.X--;
                if (inputKey == ConsoleKey.DownArrow) headPos.Y++;
                else if (inputKey == ConsoleKey.UpArrow && headPos.Y > 0) headPos.Y--;

                Console.SetCursorPosition(headPos.X, headPos.Y);
                Console.Write(currentPositions[0]);

                currentPositions = new Vector2Int[bodyLenght];
                foreach (Vector2Int pos in currentPositions)
                {
                    //pos = oldPositions TODO: USE FOR LOOP INSTEAD FOR INDEX
                }


                // WIN STATES ETC
                if (headPos.X == xpPos.X && headPos.Y == xpPos.Y)
                {
                    cooldown = 5;
                    xpPos = PlaceRandomXP(spawnDiameter);
                    ShowWin(true);
                }
                if (cooldown == 0) ShowWin(false);

            } while (inputKey != ConsoleKey.Escape); // inputKey is assigned

            Console.SetCursorPosition(printPos.X, printPos.Y);
            Console.Write("Spillet afsluttes nu ... Tryk på en tast!");
            Console.ReadKey();
        }

        static void UpdateCharacter()
        {
            
        }

        static void ShowWin(bool display)
        {
            if (display)
            {
                Console.SetCursorPosition(printPos.X, printPos.Y);
                Console.Write("Du har samlet X op ...");
            }
            else
            {
                Console.SetCursorPosition(printPos.X, printPos.Y);
                Console.Write("                      ");
            }
        }

        static Vector2Int PlaceRandomXP(int maxRange)
        {
            Console.CursorVisible = false;
            Random r = new Random();
            Vector2Int xpPos = new Vector2Int(r.Next(0, maxRange + 1), r.Next(0, maxRange + 1));
            Console.SetCursorPosition(xpPos.X,xpPos.Y);
            Console.Write("X");
            return xpPos;
        }
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
