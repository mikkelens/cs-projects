using System;
using System.Numerics;
using System.Threading;
using CustomElements;

namespace GameLoop101
{
    class Program
    {
        private static string text = "lets fuckin gooo";
        private static int xPos = 0;
        private static int yPos = 0;

        private static bool running = true;

        static void Main(string[] args)
        {
            // text
            //Console.Write("Text: ");
            //string newText = Console.ReadLine();
            //text = newText != "" ? newText : text;
            //AskForPosition();

            Label label = new Label(xPos,yPos);
            label.SetText(text);

            ConsoleKey key;

            while (running)
            {
                Vector2Int moveInput = Vector2Int.zero;

                if (Console.KeyAvailable) // something is pressed
                {
                    key = Console.ReadKey(true).Key;

                    // arrow key move input mapping
                    if (key == ConsoleKey.UpArrow) moveInput.Y += 1;
                    else if (key == ConsoleKey.DownArrow) moveInput.Y -= 1;
                    else if (key == ConsoleKey.LeftArrow) moveInput.X -= 1;
                    else if (key == ConsoleKey.RightArrow) moveInput.X += 1;

                    // if an arrow key of some type is pressed
                    if (moveInput != Vector2Int.zero)
                    {
                        label.MoveBy(moveInput.X, -moveInput.Y); // minus accounts for reverse display method
                    }

                    if (key == ConsoleKey.Escape) running = false;
                }
            }


            // exit
            Console.Clear();
            Thread.Sleep(250);
            Console.Write("\n\n\nPress any key to exit.");
            Console.ReadKey();
        }

        public static void AskForPosition()
        {
            // X
            Console.Write("X position: ");
            string xRead = Console.ReadLine();
            xPos = xRead != "" ? Convert.ToInt32(xRead) : xPos;
            // Y
            Console.Write("Y position: ");
            string yRead = Console.ReadLine();
            yPos = yRead != "" ? Convert.ToInt32(yRead) : yPos;
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

        public static Vector2Int zero => default;

        public override bool Equals(object obj)
        {
            if (obj is Vector2Int) return this.Equals((Vector2Int)obj);
            else return false;
        }

        public bool Equals(Vector2Int other)
        {
            return ((this.X == other.X) && (this.Y == other.Y));
        }

        public static bool operator ==(Vector2Int value1, Vector2Int value2)
        {
            return ((value1.X == value2.X) && (value1.Y == value2.Y));
        }

        public static bool operator !=(Vector2Int value1, Vector2Int value2)
        {
            if (value1.X == value2.X) return value1.Y != value2.Y;
            return true;
        }

    }
}
