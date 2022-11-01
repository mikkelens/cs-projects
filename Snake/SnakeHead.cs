namespace Snake;

public class SnakeHead : SnakeBodyPart
{
	public static readonly List<SnakeBodyPart> BodyParts = new List<SnakeBodyPart>();

	private protected override char DisplayChar => 'O';

	private (int, int) _lastDirection = (1, 0); // moves left by default

	public void MovePreviousDirection()
	{
		MoveDirection(_lastDirection);
	}
	public override void MoveDirection((int, int) direction)
	{
		base.MoveDirection(direction);
		_lastDirection = direction;
	}

	public bool CollisionCheck()
	{
		if (_lastDirection == (0, 0)) return false;
		if (!CheckForBounds())
		{
			Debug.WaitingPauseLog("Ran outside borders!");
			return true;
		}
		if (CheckForBodyPart())
		{
			Debug.WaitingPauseLog("Hit body part!");
			return true;
		}
		return false;
	}

	public bool CheckForApple()
	{
		return Game.ApplePosition == Position;
	}

	private bool CheckForBounds()
	{
		(int width, int height) bounds = Game.AreaSizes;
		if (Position.x <= 0 || Position.x >= bounds.width)
		{
			return false;
		}
		if (Position.y <= 0 || Position.y >= bounds.height)
		{
			return false;
		}
		return true;
	}
	private bool CheckForBodyPart()
	{
		if (BodyParts.Any(bodyPart => bodyPart.Position == Position))
		{
			Debug.TemporaryPauseLog("Hit body part!");
			return true;
		}
		return false;
	}

	public SnakeHead((int, int) spawnPos, int extraParts = 0) : base(spawnPos, extraParts)
	{
	}
}