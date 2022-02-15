using System.Numerics;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine($"PRINT: {new Vector2(1, 4).ToIndex()}");
        End();
    }
    static void End()
    {
        Console.CursorVisible = false;
        Console.Write("End of program.");
        Console.ReadKey();
    }
}
