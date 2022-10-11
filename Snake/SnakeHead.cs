namespace Snake;

public class SnakeHead : SnakeBodyPart
{
	protected override char DisplayChar => 'O';

	private (int, int) _lastDirection = (0, 0); // moves left by default

	public void Move()
	{
		Move(_lastDirection);
	}

	protected override void Move((int, int) direction)
	{
		base.Move(direction);
		_lastDirection = direction;
	}
	public bool IsWithinBounds()
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

	public SnakeHead((int, int) pos, int extraParts = 0) : base(pos, extraParts)
	{
	}
}