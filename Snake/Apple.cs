namespace Snake;

public class Apple : CoordinateObject
{
	private protected override char DisplayChar => '@';

	public Apple((int x, int y) position) : base(position) { }
}