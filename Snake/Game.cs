namespace Snake;

public static class Game
{
	private const int AreaWidth = 20;
	private const int AreaHeight = 10;
	public static readonly Area Board = new Area((AreaWidth, AreaHeight), (0, 0)); // cant be const because c# tuple lol

	private const int StartUpdateRate = 5;

	private static int _updateRate;
	private static SnakeHead _snakeHead = null!;
	public static Apple CurrentApple { get; private set; } = null!;

	private static readonly Random Random = new Random();

	public static void Run()
	{
		Console.CursorVisible = false;
		Console.Clear();

		Board.DrawBorder(); // todo fix

		const int startX = 1;
		const int startY = 1;
		const int startLength = 3;
		_snakeHead = new SnakeHead((startX, startY), startLength);
		CurrentApple = SpawnNewApple();

		bool alive = true;


		_updateRate = StartUpdateRate;
		while (alive)
		{
			// frame update delay
			double deltaTime = 1.0 / _updateRate;
			Thread.Sleep((int)(deltaTime * 1000));

			(int, int)? newInput = ReadNewInput();
			if (newInput != null)
			{
				(int, int) direction = newInput.Value;
				if (_snakeHead.CanMoveDirection(direction))
				{
					_snakeHead.MoveDirection(direction);
				}
			}
			else
			{
				_snakeHead.MovePreviousDirection();
			}

			if (_snakeHead.CheckForApple()) ConsumeApple();
			if (_snakeHead.CollisionCheck()) alive = false;
		}

		Console.SetCursorPosition(0, Board.DrawSizes.height);
		Console.WriteLine("You died.");
	}

	private static (int, int)? ReadNewInput()
	{
		ConsoleKeyInfo? latestKey = null;
		while (Console.KeyAvailable)
		{
			// We only remember the latest keypress, so that no queue of inputs is formed.
			// The issue with this is that queues are needed at low framerates/updates.
			latestKey = Console.ReadKey(true);
		}
		if (latestKey == null) return null;
		(int x, int y)? input = latestKey.Value.Key switch
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
		return input;
	}

	private static void ConsumeApple()
	{
		_snakeHead.GrowAtEndOfSnake();
		_updateRate++;
		CurrentApple = SpawnNewApple();

	}
	private static Apple SpawnNewApple()
	{
		(int, int) newRandomPosition = (Random.Next(Board.Sizes.width), Random.Next(Board.Sizes.height));
		return new Apple(newRandomPosition);
	}
}