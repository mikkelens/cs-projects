using System;
using System.Threading;

namespace Methods
{
    class Program
    {
        enum Anchor { Left, Center, Right };

        static void Main(string[] args)
        {
            //Assignment_1();

            //Assignment_2();

            Assignment_3();

            End();
        }

        #region program execution
        static void End()
        {
            Console.CursorVisible = false;
            Thread.Sleep(500);
            Console.Write("\n\n\nPress any key to exit.");
            Console.ReadKey();
        }
        #endregion

        #region Assignment 1 (Tax / "Moms")
        static void Assignment_1()
        {
            float taxPercent = 25;

            Print("\n--- TAX ONTO PRICE ---");
            float price1 = 80;
            float price2 = 150;
            Print($"Price 1: {price1}, after tax: {TaxedPrice(price1, taxPercent)}");
            Print($"Price 2: {price2}, after tax: {TaxedPrice(price2, taxPercent)}");
            
            Print("\n--- TAX ONTO PRICE ---");
            float taxedPrice1 = 90;
            float taxedPrice2 = 140;
            Print($"Taxed Price 1: {taxedPrice1}, before tax: {PriceWithoutTax(taxedPrice1, taxPercent)}");
            Print($"Taxed Price 2: {taxedPrice2}, before tax: {PriceWithoutTax(taxedPrice2, taxPercent)}");
        }

        static float TaxedPrice(float priceWithoutTax, float taxPercentage)
        {
            return priceWithoutTax * (1 + Percent(taxPercentage));
        }

        static float PriceWithoutTax(float priceWithTax, float taxPercentage)
        {
            return priceWithTax / (1 + Percent(taxPercentage));
        }

        #endregion

        #region Assignment 2 (Equal / "lige eller ulige")

        static void Assignment_2()
        {
            Print($"\nInput a number to see if it is even.");
            string evenReply = InputLine();
            if (evenReply != "")
            {
                int evenNumber = int.Parse(evenReply);

                string message = IsEven(evenNumber)
                    ? $"Congratulations, {evenNumber} is even."
                    : $"Unfortunate, {evenNumber} is not even.";
                Print(message);
            }

            Print("\nInput a number, and then a number to divide by, to see if the first number is divisible by the second.");
            string stringToDivide = InputLine();
            if (stringToDivide != "")
            {
                int numberToDivide = int.Parse(stringToDivide);

                string divideString = InputLine();
                if (divideString != null)
                {
                    int divideNumber = int.Parse(divideString);

                    string message = DivisibleByN(numberToDivide, divideNumber)
                        ? $"Congratulations, {numberToDivide} is divisible by {divideNumber}."
                        : $"Unfortunate, {numberToDivide} is not divisible by {divideNumber}.";
                    Print(message);
                }
            }
        }

        static bool IsEven(int number)
        {
            return number % 2 == 0;
        }

        static bool DivisibleByN(int number, int N)
        {
            return number % N == 0;
        }
        #endregion

        #region Assignment 3 (Print adjustments / "justering af tekst")
        static void Assignment_3()
        {
            int xOutset = 20;
            Print($"Outset: {xOutset}");

            //Thread.Sleep(250);

            //Vector2Int point1 = new Vector2Int(xOutset, 3);
            //PrintXY("Why are we still here?", point1); // Anchor.left
            //Vector2Int point2 = new Vector2Int(xOutset, 5);
            //PrintXY("Just to suffer?", point2, Anchor.center);
            //Vector2Int point3 = new Vector2Int(xOutset, 7);
            //PrintXY("Every night...", point3, Anchor.right);

            Thread.Sleep(250);

            Vector2Int point4 = new Vector2Int(xOutset, 2);
            Box(point4, "", new Vector2Int(0, 0));

            Vector2Int point5 = new Vector2Int(xOutset, 8);
            Box(point5, "yeah...", new Vector2Int(2, 2), Anchor.Center);

            Vector2Int point6 = new Vector2Int(xOutset, 20);
            Box(point6, "nahhhhhhh", new Vector2Int(1, 1), Anchor.Right);
        }

        static void Box(Vector2Int position, string message = "", Vector2Int extraSpacing = default, Anchor justification = Anchor.Left)
        {
            float aspectRatio = 2; // width divided by height aka. width is (aspect-ratio) times larger than height
            Vector2Int actualSpacing = new Vector2Int((int)(extraSpacing.X * aspectRatio) + 1, extraSpacing.Y + 1);

            Vector2Int wordOccupation = new Vector2Int(message.Length > 1 ? message.Length : 1, 1);
            Vector2Int boxDimensions = new Vector2Int(wordOccupation.X + actualSpacing.X * 2, wordOccupation.Y + actualSpacing.Y * 2);

            Vector2Int offset = Vector2Int.Zero;
            if(justification == Anchor.Center)
                offset = new Vector2Int(-boxDimensions.X / 2, 0);
            else if (justification == Anchor.Right)
                offset = new Vector2Int(-boxDimensions.X, 0);

            Vector2Int cornerPosition = new Vector2Int(position.X + offset.X, position.Y + offset.Y);

            PrintXY($"CORNER POSITION: {cornerPosition.X}, {cornerPosition.Y}. DIMENSIONS: {boxDimensions.X}, {boxDimensions.Y}. EXTRA SPACING: {extraSpacing.X}, {extraSpacing.Y}", new Vector2Int(cornerPosition.X, cornerPosition.Y - 1));

            // boundary drawing
            char xBoundary = '|';
            char yBoundary = '-';
            for (int y = 0; y < boxDimensions.Y; y++)
            {
                for (int x = 0; x < boxDimensions.X; x++)
                {
                    Vector2Int charPosition = new Vector2Int(cornerPosition.X + x, cornerPosition.Y + y);
                    if (y == 0 || y == boxDimensions.Y - 1)
                    {
                        PrintXY(yBoundary.ToString(), charPosition);
                    }
                    else if (x == 0 || x == boxDimensions.X - 1)
                    {
                        PrintXY(xBoundary.ToString(), charPosition);
                    }
                }
            }

            Vector2Int messagePosition = new Vector2Int(cornerPosition.X + actualSpacing.X, cornerPosition.Y + actualSpacing.Y);
            PrintXY(message, messagePosition);
        }

        static void PrintXY(string message, Vector2Int desiredPosition, Anchor justification = Anchor.Left)
        {
            int offsetX = 0; // if justification == Anchor.left
            if (justification == Anchor.Center)
            {
                offsetX = message.Length / 2;
            }
            else if (justification == Anchor.Right)
            {
                offsetX = message.Length;
            }
            Vector2Int offsetPosition = new Vector2Int(desiredPosition.X - offsetX, desiredPosition.Y);
            Vector2Int actualPosition = ClampedPosition(offsetPosition);
            Console.SetCursorPosition(actualPosition.X, actualPosition.Y);
            Print(message, false);
        }

        static Vector2Int ClampedPosition(Vector2Int position)
        {
            int width = Console.WindowWidth;
            int height = Console.WindowHeight;
            return new Vector2Int(Math.Clamp(position.X, 0, width), Math.Clamp(position.Y, 0, height));
        }
        #endregion

        #region number manipulation
        static float Percent(float toPercent) => toPercent / 100;
        #endregion

        #region printing
        static void Print(string message, bool newLine = true)
        {
            if (newLine)
            {
                Console.WriteLine(message);
            }
            else
            {
                Console.Write(message);
            }
        }

        static string InputLine()
        {
            Print("> ", false);
            return Console.ReadLine();
        }
        #endregion

        #region structs (yes really)
        public partial struct Vector2Int
        {
            public int X;
            public int Y;

            public Vector2Int(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public static Vector2Int Zero => default;

            public static Vector2Int One => new Vector2Int(1, 1);
            public static Vector2Int Right => new Vector2Int(1, 0);
            public static Vector2Int Left => new Vector2Int(-1, 0);
            public static Vector2Int Up => new Vector2Int(0, 1);
            public static Vector2Int Down => new Vector2Int(0, -1);

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

            public override int GetHashCode()
            {
                return (this.X.GetHashCode() + this.Y.GetHashCode());
            }

            public override string ToString()
            {
                return string.Format("{{X:{0} Y:{1}}}", this.X, this.Y);
            }
        }
        #endregion
    }
}
