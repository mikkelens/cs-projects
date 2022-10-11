namespace Snake;

internal static class Program
{
	public static readonly (int width, int height) AreaSizes = (30, 15); // cant be const because c# lol

	public static void Main()
	{
		DrawBorder(AreaSizes);


		(int, int) startPos = (5, 5);
		int startLength = 5;
		SnakeHead snake = new SnakeHead(startPos, startLength);

		bool alive = true;

		const int startUpdateRate = 1;
		int updateRate = startUpdateRate;
		do
		{
			// todo: read input, pass to move

			snake.Move();
			if (!snake.IsWithinBounds()) alive = false;

			// frame update delay
			float deltaTime = 1f / updateRate;
			Thread.Sleep((int)(deltaTime * 1000));
		} while (alive);

		Console.SetCursorPosition(0, AreaSizes.height + 1);
		Console.WriteLine("You died.");

		Thread.Sleep(800);
		Console.WriteLine("Press any key to exit.");
		Console.ReadLine(); // enter before exit
	}

	private static void DrawBorder((int width, int height) sizes)
	{
		for (int y = 0; y < sizes.height; y++)
		{
			Console.SetCursorPosition(0, y);
			if (y == 0 || y == sizes.height - 1)
			{
				// top/bottom
				string line = new string('-', sizes.width);
				Console.Write(line);
			}
			else
			{
				// sides
				string space = new string(' ', sizes.width - 2);
				string line = $"|{space}|";
				Console.Write(line);
			}
		}
	}
}