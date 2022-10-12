namespace Snake;

public class SnakeHead : SnakeBodyPart
{
	public static readonly List<SnakeBodyPart> BodyParts = new List<SnakeBodyPart>();

	private protected override char DisplayChar => 'O';

	private (int, int) _lastDirection = (0, 0); // moves left by default

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
		if (!IsWithinBounds()) return true;
		if (HasHitBodyPart()) return true;
		return false;
	}
	private bool IsWithinBounds()
	{
		(int width, int height) bounds = Program.AreaSizes;
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
	private bool HasHitBodyPart()
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