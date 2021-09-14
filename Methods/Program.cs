using System;
using System.Threading;

namespace Methods
{
    class Program
    {
        static void Main(string[] args)
        {
            //Assignment_1();

            //Assigment_2();

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

        static void Assigment_2()
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

        #region Assignment 3 ("justering af tekst")
        static void Assignment_3()
        {
            Vector2Int point1 = new Vector2Int(15, 0);
            PrintCenterXY("Why are we still here?", point1);
            Vector2Int point2 = new Vector2Int(6, 2);
            PrintXY("Just to suffer?", point2);
        }

        static void Box(Vector2Int position, Vector2Int dimensions, Vector2Int spacing)
        {

        }

        static void PrintCenterXY(string message, Vector2Int centerPosition)
        {
            int offsetX = message.Length / 2;
            Vector2Int offsetPosition = new Vector2Int(centerPosition.X - offsetX, centerPosition.Y);
            PrintXY(message, offsetPosition);
        }

        static void PrintXY(string message, Vector2Int leftPosition)
        {
            Vector2Int actualPosition = RightJustifyXY(leftPosition);
            Console.SetCursorPosition(actualPosition.X, actualPosition.Y);
            Print(message, false);
        }

        static Vector2Int RightJustifyXY(Vector2Int position)
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
        public struct Vector2Int
        {
            public int X;
            public int Y;

            public Vector2Int(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

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
