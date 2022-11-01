namespace Snake;

internal static class Program
{
	public static void Main()
	{
		Game.Run();
		Console.WriteLine("You died.");
		Console.WriteLine("Press enter to exit.");
		Console.ReadLine(); // enter before exit
	}
}