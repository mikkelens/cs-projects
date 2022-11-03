namespace Snake;

public class SnakeHead : SnakeBodyPart
{
	private bool _alive;
	public bool Alive
	{
		get => _alive;
		set
		{
			_alive = value;
			Redraw();
		}
	}

	private protected override char DisplayChar => Alive ? 'O' : 'X';

	// identical to body constructor, but with "alive" state
	public SnakeHead((int, int) spawnPos, int extraParts = 0) : base(spawnPos, extraParts)
	{
		Alive = true;
	}

	private (int x, int y) _lastDirection = (1, 0); // moves left by default

	public bool CanMoveDirection((int x, int y) direction) // Can head turn this direction?
	{
		// Reject directions opposite previous direction (can't turn directly into itself)
		if (_lastDirection.x * direction.x < 0) return false;
		if (_lastDirection.y * direction.y < 0) return false;
		return true;
	}
	public override void MoveDirection((int, int) direction)
	{
		base.MoveDirection(direction);
		_lastDirection = direction;
	}
	public void MovePreviousDirection()
	{
		MoveDirection(_lastDirection);
	}

#region Checks
	public bool CollisionCheck()
	{
		if (_lastDirection == (0, 0)) return false;
		if (Game.Board.CheckOutsideBounds(Position))
		{
			// Debug.WaitingPauseLog("Ran outside borders!");
			return true;
		}
		if (CheckForBodyPart())
		{
			// Debug.WaitingPauseLog("Hit body part!");
			return true;
		}
		return false;
	}

	private bool CheckForBodyPart()
	{
		return AllBodyParts.Where(bodyPart => bodyPart != this).Any(bodyPart => Position == bodyPart.Position && !bodyPart.NewlySpawned);
	}
#endregion
}