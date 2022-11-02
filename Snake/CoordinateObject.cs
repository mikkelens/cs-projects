namespace Snake;

// object on coordinate that is displayable
public abstract class CoordinateObject
{
	private (int, int) _position;
	public (int x, int y) Position
	{
		get => _position;
		protected set
		{
			ClearAtPosition();
			_position = value;
			DisplayAtPosition();
		}
	}

	protected CoordinateObject((int, int) position)
	{
		Position = position;
	}

	private protected virtual char DisplayChar => 'X'; // default "missing texture" heh

	private protected void Redraw()
	{
		DisplayAtPosition();
	}

	private void ClearAtPosition()
	{
		Game.Board.SetCursorToDrawPosition(Position);
		Console.Write(' ');
	}
	private void DisplayAtPosition()
	{
		Game.Board.SetCursorToDrawPosition(Position);
		Console.Write(DisplayChar);
	}
}