using System.Numerics;

namespace Recursive_WinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            BackColor = Color.Black;
            
            Opgave1();
        }

        private void Opgave1()
        {
            List<Color> orderedColors = new List<Color> // has 6+ colors, can draw 6+ levels
            {
                Color.Red,
                Color.Blue,
                Color.Green,
                Color.Yellow,
                Color.Orange,
                Color.Brown,
                Color.Purple,
                Color.Magenta,
                Color.Pink
            };
            
            Graphics graphics = CreateGraphics();
            Pen pen = new Pen(Color.Black); // color and width changed dynamically depending on level

            Rectangle outerBounds = new Rectangle(150, 150, 250, 250);
            int totalLevelCount = 8;
            DrawLevelAndBelow(totalLevelCount, outerBounds);

            void DrawLevelAndBelow(int level, Rectangle frame)
            {
                level--;
                if (level < 0) return; // go back up

                // draw itself
                pen.Color = orderedColors[totalLevelCount - (1 + level)];
                pen.Width = level * 2;
                graphics.DrawRectangle(pen, frame);
                
                // draw below frames
                for (int i = 0; i < 4; i++)
                {
                    int x = i % 2 == 0 ? 0 : 1;
                    int y = i >= 2 ? 1 : 0;

                    Point location = new Point(frame.X + x * frame.Width, frame.Y + y * frame.Height);
                    
                    Size size = new Size(frame.Width / 2, frame.Height / 2);
                    location.X -= size.Width / 2;
                    location.Y -= size.Height / 2;
                    
                    Rectangle innerFrame = new Rectangle(location, size);

                    DrawLevelAndBelow(level, innerFrame); // draw below frames first
                }
            }
        }


        private static void DrawRectangleLocationAsCenter(Graphics graphics, Pen pen, Rectangle rect)
        {
            CenterRectangle();
            graphics.DrawRectangle(pen, rect);
            
            void CenterRectangle()
            {
                rect.X -= rect.Width / 2;
                rect.Y -= rect.Height / 2;
            }
        }

        // protected void Sierpinski()
        // {
        //     Graphics graphics = CreateGraphics();
        //     Pen pen = new Pen(Color.Green, 10);
        //     //Rectangle rect = new Rectangle(100, 10, 100, 100)
        //     DrawTriangle(graphics, pen, 0, Vector2.Zero);
        // }
        
        // private void DrawTriangle(Graphics graphics, Pen pen, int level, Vector2 offset)
        // {
        //     if (level > 50) return;
        //
        //     // times to make triangles this level
        //     float triangleCount = level > 0 ? MathF.Pow(3, level) : 1;
        //
        //     for (int i = 0; i < triangleCount; i++)
        //     {
        //
        //     }
        //     // calculate position and size
        //     float size = 1f;
        //
        //     PointF up = new PointF(size / 2f + offset.X, size + offset.Y);
        //     PointF left = new PointF(offset.X, offset.Y);
        //     PointF right = new PointF(offset.X + size, offset.Y);
        //
        //     PointF[] points = new PointF[]
        //     {
        //         up,
        //         left,
        //         right
        //     };
        //
        //     // draw
        //     graphics.DrawPolygon();
        //
        //     // go step down
        //     level--;
        //     DrawTriangle(graphics, pen, level);
        // }
    }
}