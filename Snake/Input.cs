namespace Snake;

public static class UserInput
{

	public static bool BoolAskUser(string question)
	{
		Console.CursorVisible = true;
		Console.WriteLine(question);
		ConsoleKey input;
		do
		{
			input = Console.ReadKey(true).Key;
		} while (input != ConsoleKey.Y && input != ConsoleKey.N);
		Console.CursorVisible = false;
		return input == ConsoleKey.Y;
	}
}