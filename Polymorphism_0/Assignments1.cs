namespace Polymorphism_0;

public static class Assignments1
{
	// 1)
	private abstract class Shape
	{
		public abstract void Describe();
	}

	// 2)
	private class Rectangle : Shape
	{
		private readonly float _width;
		private readonly float _height;
		public override void Describe()
		{
			Console.WriteLine($"{nameof(Rectangle)}; dimensions {_width}x{_height}");
		}

		// 3)
		public Rectangle(float sideLength)
		{
			_width = sideLength;
			_height = sideLength;
		}

		// 4)
		public Rectangle(float width, float height)
		{
			_width = width;
			_height = height;
		}
	}

	// 5)
	private class Circle : Shape
	{
		private readonly float _radius;
		public Circle(float radius)
		{
			_radius = radius;
		}

		// 6)
		public override void Describe()
		{
			Console.WriteLine($"{nameof(Circle)}; radius of {_radius}");
		}
	}

	public static void Run()
	{
		// 7)
		Shape[] shapes =
		{
			new Circle(3),
			new Circle(5),
			new Rectangle(2),
			new Rectangle(3, 4)
		};
		foreach (Shape shape in shapes) shape.Describe();
	}
}