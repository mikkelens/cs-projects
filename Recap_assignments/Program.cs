// See https://aka.ms/new-console-template for more information

class Program
{
	public static void Main(string[] args)
	{
		// 1)
		string s = "(enter text here)";
		
		// 2)
		Console.WriteLine($"{s.GetType()} {nameof(s)} = {s}");

		// 3)
		Console.Write("Write a char: ");
		Char inputChar = Console.ReadKey().KeyChar;
		
		// 4)
		
		
		
		// pause on end
		End();
	}

	private static void End()
	{
		Console.Write("\n\n\nPress enter to close window.");
		Console.ReadLine();
	}
}