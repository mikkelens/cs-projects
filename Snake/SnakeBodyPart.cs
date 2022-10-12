namespace Snake;

// body of the snake. also inhereted for head
public class SnakeBodyPart
{
	public (int x, int y) Position { get; private set; }

	private SnakeBodyPart? _nextBodyPart;

	private protected virtual char DisplayChar => 'o';

	protected SnakeBodyPart((int, int) spawnPos, int extraParts = 0)
	{
		// Debug.TemporaryPauseLog($"Spawned Snake body part at Position: {spawnPos.ToString()}", 0.75f);
		SnakeHead.BodyParts.Add(this);
		Position = spawnPos;
		if (extraParts > 0)
		{
			_nextBodyPart = new SnakeBodyPart(spawnPos, extraParts - 1);
		}
		// else
		// {
		// 	Debug.TemporaryPauseLog("No more body parts to spawn.", 0.5f);
		// }
	}


	// move with body to position
	public virtual void MoveDirection((int x, int y) direction)
	{
		(int x, int y) prevPos = Position;
		Position = (Position.x + direction.x, Position.y + direction.y);
		ClearAtConsolePosition(prevPos); // clear this position
		if (_nextBodyPart != null && _nextBodyPart.Position != prevPos)
		{
			(int x, int y) bodyPartPos = _nextBodyPart.Position;
			(int, int) directionToPrev = (prevPos.x - bodyPartPos.x, prevPos.y - bodyPartPos.y);
			_nextBodyPart.MoveDirection(directionToPrev); // remaining body follows this body part
		}
		DisplayAtConsolePosition(Position); // display top last (head on top)
		// Debug.TemporaryPauseLog($"Moving snake body part from {Position} to {newPos}");
	}

	public void Grow()
	{
		if (_nextBodyPart == null)
		{
			_nextBodyPart = new SnakeBodyPart(Position);
		}
		else
		{
			_nextBodyPart.Grow();
		}
	}

	private void ClearAtConsolePosition((int x, int y) pos)
	{
		Console.SetCursorPosition(pos.x, pos.y);
		Console.Write(' ');
	}
	private void DisplayAtConsolePosition((int x, int y) pos)
	{
		Console.SetCursorPosition(pos.x, pos.y);
		Console.Write(DisplayChar);
	}
}