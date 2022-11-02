namespace Snake;

internal static class Program
{
	public static void Main()
	{
		do
		{
			Game.Run();
		} while (UserInput.BoolAskUser("Do you want to play again? (Y/N)"));

		Console.Clear();
		Console.WriteLine("Closing...");
	}

}