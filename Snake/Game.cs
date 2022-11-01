namespace Snake;

public static class Game
{
	public static readonly (int width, int height) AreaSizes = (30, 15); // cant be const because c# lol

	public static void Run()
	{
		Console.CursorVisible = false;

		DrawBorder(AreaSizes);
		Debug.DebugWritePosition = (0, AreaSizes.height + 6);

		const int startX = 5;
		const int startY = 5;
		const int startLength = 3;
		SnakeHead snake = new SnakeHead((startX, startY), startLength);

		bool alive = true;

		const int startUpdateRate = 25;
		int updateRate = startUpdateRate; // todo: should get faster with more fruits eaten
		while (alive)
		{
			// frame update delay
			float deltaTime = 1f / updateRate;
			Thread.Sleep((int)(deltaTime * 1000));

			(int, int)? newInput = ReadNewInput();
			if (newInput != null)
			{
				snake.MoveDirection(newInput.Value);
				// Debug.TemporaryPauseLog($"INPUT RECEIVED. MOVEINPUT: {newInput.Value}", 0.75f);
			}
			else
			{
				snake.MovePreviousDirection();
				// Debug.TemporaryPauseLog($"NO INPUT. PREVIOUS: {_lastMove}", 0.75f);
			}

			if (snake.CollisionCheck()) alive = false;

			(int, int)? ReadNewInput()
			{
				ConsoleKeyInfo? lastKey = null;
				while (Console.KeyAvailable)
				{
					lastKey = Console.ReadKey(true);
				}
				if (lastKey == null) return null;
				(int x, int y)? input = lastKey.Value.Key switch
				{
					ConsoleKey.UpArrow => (0, -1),
					ConsoleKey.W => (0, -1),
					ConsoleKey.DownArrow => (0, 1),
					ConsoleKey.S => (0, 1),
					ConsoleKey.LeftArrow => (-1, 0),
					ConsoleKey.A => (-1, 0),
					ConsoleKey.RightArrow => (1, 0),
					ConsoleKey.D => (1, 0),
					_ => null
				};
				// get rid of previous key info (only allow new keypresses to be used)
				// if (key == null || key.Value == _lastMove) return null;
				// if (key.Value.x == _lastMove.x) key = (0, key.Value.y);
				// else if (key.Value.y == _lastMove.y) key = (key.Value.x, 0);
				return input;
			}
		}

		Console.SetCursorPosition(0, AreaSizes.height + 1);
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