namespace Snake;

// body of the snake. also inhereted for head
public class SnakeBodyPart : CoordinateObject
{
	public bool NewlySpawned { get; private set; }

	private SnakeBodyPart? _nextBodyPart;

	private protected override char DisplayChar => 'o';
	private protected override void DisplayAtPosition()
	{
		if (!CheckForApple()) // dont draw if on top of apple. todo: make apple not spawn on top of us
		{
			base.DisplayAtPosition();
		}
	}

	protected SnakeBodyPart((int, int) spawnPos, int extraParts = 0) : base(spawnPos)
	{
		NewlySpawned = true;
		SnakeHead.BodyParts.Add(this);
		if (extraParts > 0)
		{
			_nextBodyPart = new SnakeBodyPart(spawnPos, extraParts - 1);
		}
	}

	// move with body to position
	public virtual void MoveDirection((int x, int y) direction)
	{
		if (NewlySpawned) NewlySpawned = false; // todo: make this check work

		(int x, int y) previousPos = Position;
		Position = (Position.x + direction.x, Position.y + direction.y); // move to next position

		if (_nextBodyPart != null && _nextBodyPart.Position != previousPos) // next body part in chain was not on inside of us
		{
			(int x, int y) bodyPartPos = _nextBodyPart.Position;
			(int, int) directionToPreviousPosition = (previousPos.x - bodyPartPos.x, previousPos.y - bodyPartPos.y);
			_nextBodyPart.MoveDirection(directionToPreviousPosition); // remaining body parts follow this body part (in a chain)
		}
		// Debug.TemporaryPauseLog($"Moving snake body part from {Position} to {newPos}");
	}

	public void GrowAtEndOfSnake()
	{
		if (_nextBodyPart == null) // reach end of chain?
		{
			// grow
			_nextBodyPart = new SnakeBodyPart(Position);
		}
		else
		{
			// send down chain
			_nextBodyPart.GrowAtEndOfSnake();
		}
	}

	public bool CheckForApple()
	{
		return Game.CurrentApple.Position == Position;
	}
}