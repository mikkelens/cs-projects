namespace Polymorphism_0;

public static class Examples1
{
	public static void Run()
	{
		Animal[] animals = { new Animal.Bear() };
		foreach (Animal animal in animals)
		{
			animal.Eat();
		}
	}

	public abstract class Animal
	{
		public abstract void Eat();

		public class Goat : Animal
		{
			public override void Eat()
			{
				Console.WriteLine("Crunch");
			}
		}
		public class Bear : Animal
		{
			public override void Eat()
			{
				Console.WriteLine("Monch");
			}
		}
	}
}