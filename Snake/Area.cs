namespace Snake;

public class Area
{
	private (int width, int height) _sizes;
	public (int width, int height) Sizes
	{
		get => _sizes;
		private set
		{
			_sizes = value;
			Console.Clear();
			DrawBorder();
		}
	}


	public Area((int, int) sizes, (int, int) drawPivot)
	{
		Sizes = sizes;
		_drawPivot = drawPivot;
	}

	public bool CheckOutsideBounds((int x, int y) position)
	{
		if (position.x < 0 || position.x >= Sizes.width)
		{
			return true;
		}
		if (position.y < 0 || position.y >= Sizes.height)
		{
			return true;
		}
		return false;
	}

	// drawing
	private readonly (int x, int y) _drawPivot;
	private const int BoundsDrawThickness = 1;
	public (int width, int height) DrawSizes => (Sizes.width + BoundsDrawThickness * 2, Sizes.height + BoundsDrawThickness * 2);

	public void SetCursorToDrawPosition((int, int) logicalPosition)
	{
		(int x, int y) drawPosition = DrawPositionWithinArea(logicalPosition);
		Console.SetCursorPosition(drawPosition.x, drawPosition.y);
	}
	public (int x, int y) DrawPositionWithinArea((int x, int y) logicalPosition)
	{
		return (logicalPosition.x + _drawPivot.x + BoundsDrawThickness, logicalPosition.y + _drawPivot.y + BoundsDrawThickness);
	}

	// Draw area with border
	public void DrawBorder()
	{
		const char horizontalChar = '-';
		const char verticalChar = '|';
		const char cornerChar = '#';
		// Draw from top-left, right then down
		(int width, int height) drawArea = (Sizes.width + 2 * BoundsDrawThickness, Sizes.height + 2 * BoundsDrawThickness);
		for (int y = 0; y < drawArea.height; y++)
		{
			for (int x = 0; x < drawArea.width; x++)
			{
				char charToWrite;
				bool drawX = x - BoundsDrawThickness < 0 || x + BoundsDrawThickness >= drawArea.width;
				bool drawY = y - BoundsDrawThickness < 0 || y + BoundsDrawThickness >= drawArea.height;
				if (drawX && drawY)
				{
					charToWrite = cornerChar;
				}
				else if (drawY)
				{
					charToWrite = horizontalChar; // horizontal on top/bottom edges
				}
				else if (drawX)
				{
					charToWrite = verticalChar; // vertical on left/right edges
				}
				else
				{
					continue;
				}
				Console.SetCursorPosition(_drawPivot.x + x, _drawPivot.y + y);
				Console.Write(charToWrite);
			}
		}
	}
}