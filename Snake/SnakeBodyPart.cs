namespace Snake;

// body of the snake. also inhereted for head
public class SnakeBodyPart
{
	protected virtual char DisplayChar => 'o';

	private SnakeBodyPart? _nextBodyPart;

	protected (int x, int y) Position { get; private set; }

	protected SnakeBodyPart((int, int) pos, int extraParts = 0)
	{
		Position = pos;
		if (extraParts > 0)
		{
			_nextBodyPart = new SnakeBodyPart(pos, extraParts - 1);
		}
	}

	// move with body to position
	protected virtual void Move((int, int) newPos)
	{
		ClearAtConsolePosition(Position);
		if (_nextBodyPart != null)
		{
			_nextBodyPart.Move(Position); // remaining body follows this body part
		}
		Position = newPos;
		DisplayAtConsolePosition(Position);
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