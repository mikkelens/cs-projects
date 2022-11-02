namespace Snake;

public static class Game
{
	// Todo: improve display formatting so that game can have proper square pixels
	private const int AreaWidth = 20;
	private const int AreaHeight = 10;
	public static readonly Area Board = new Area((AreaWidth, AreaHeight), (0, 0)); // cant be const because c# tuple lol

	private const float StartMoveSpeed = 4f;

	private static float _updateRate;
	private static double DesiredFrameTime => 1000.0 / _updateRate; // In milliseconds
	private static SnakeHead _snakeHead = null!;
	public static Apple CurrentApple { get; private set; } = null!;

	private static readonly Random Random = new Random();

	public static void Run()
	{
		Console.CursorVisible = false;
		Console.Clear();

		Board.DrawBorder();

		const int startX = 1;
		const int startY = 1;
		const int startLength = 3;
		CurrentApple = SpawnNewApple();
		_snakeHead = new SnakeHead((startX, startY), startLength);

		_updateRate = StartMoveSpeed;
		DateTime lastMeasuredTime = DateTime.Now;
		List<InputData> inputs = new List<InputData>();
		while (_snakeHead.Alive)
		{
			// process inputs //

			// clear old inputs (older than 2 frames, allowing for a buffer of 2 inputs)
			inputs.RemoveAll(input => input.TimeSince().TotalMilliseconds > DesiredFrameTime * 2);
			while (Console.KeyAvailable) // get new inputs
			{
				inputs.Add(new InputData(DateTime.Now, Console.ReadKey(true).Key));
			}

			// update game if frametime has passed, else read new inputs untill true
			if ((DateTime.Now - lastMeasuredTime).TotalMilliseconds < DesiredFrameTime) continue;

			// Game tick

			if (inputs.Count != 0)
			{
				InputData oldestInput = inputs[0];
				inputs.RemoveAt(0); // remove used input
				(int, int) direction = DirectionFromKey(oldestInput.Key);
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
			if (_snakeHead.CollisionCheck()) _snakeHead.Alive = false;

			lastMeasuredTime = DateTime.Now;
		}

		Console.SetCursorPosition(0, Board.DrawSizes.height);
		Console.WriteLine("You died.");
	}

	private static (int, int) DirectionFromKey(ConsoleKey input)
	{
		return input switch
		{
			ConsoleKey.UpArrow => (0, -1),
			ConsoleKey.W => (0, -1),
			ConsoleKey.DownArrow => (0, 1),
			ConsoleKey.S => (0, 1),
			ConsoleKey.LeftArrow => (-1, 0),
			ConsoleKey.A => (-1, 0),
			ConsoleKey.RightArrow => (1, 0),
			ConsoleKey.D => (1, 0),
			_ => default
		};
	}

	private static void ConsumeApple()
	{
		_snakeHead.GrowAtEndOfSnake();
		_updateRate += 0.75f;
		CurrentApple = SpawnNewApple();

	}
	private static Apple SpawnNewApple()
	{
		(int, int) newRandomPosition = (Random.Next(Board.Sizes.width), Random.Next(Board.Sizes.height));
		return new Apple(newRandomPosition);
	}
}