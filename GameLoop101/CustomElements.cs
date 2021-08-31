using System;

namespace CustomElements
{
    class Label
    {
        int x, y;
        string text = "";

        public Label(int left, int top)
        {
            x = left;
            y = top;
        }

        public void SetText(string newText)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(newText);

            if (newText.Length < text.Length)
            {
                Console.Write(new string(' ', text.Length - newText.Length));
            }

            text = newText;
        }

        public void MoveTo(int x, int y)
        {
            if (x != this.x || y != this.y)
            {
                Console.SetCursorPosition(this.x, this.y);
                Console.Write(new string(' ', text.Length));

                this.x = x;
                this.y = y;

                if (x <= 0) x += Console.WindowWidth;
                if (x >= Console.WindowWidth) x -= Console.WindowWidth;
                if (y <= 0) y += Console.WindowHeight;
                if (y >= Console.WindowHeight) y -= Console.WindowHeight;

                Console.SetCursorPosition(x, y);
                Console.Write(text);
            }
        }
        public void MoveBy(int dx, int dy)
        {
            if (dx != 0 || dy != 0)
            {
                Console.SetCursorPosition(x, y);
                Console.Write(new string(' ', text.Length));

                x += dx;
                y += dy;

                if (x <= 0) x += Console.WindowWidth;
                if (x >= Console.WindowWidth) x -= Console.WindowWidth;
                if (y <= 0) y += Console.WindowHeight;
                if (y >= Console.WindowHeight) y -= Console.WindowHeight;

                Console.SetCursorPosition(x, y);
                Console.Write(text);
            }
        }
    }
}
