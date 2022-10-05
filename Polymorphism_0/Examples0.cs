using System.Diagnostics.CodeAnalysis;

namespace Polymorphism_0;

[SuppressMessage("ReSharper", "UnusedMember.Local")]
public static class Examples0
{
	public static void Run()
	{
		// Console.WriteLine("START");
		// ExampleMethod();
		// ExampleMethod("yes");
		// ExampleMethod(ReturnIntMethod(ReturnIntMethod()));
		Animal[] animalArray = {
			new Animal(),
			new Cow()
		};
		foreach (Animal animal in animalArray)
		{
			animal.WriteDescription();
		}
	}
#region Overloading
	private static void ExampleMethod() => Console.WriteLine("Default method!");
	private static void ExampleMethod(string info) => Console.WriteLine($"Method with info: {info}");
	private static void ExampleMethod(int number) => Console.WriteLine($"Method with value: {number}");
	private static int ReturnIntMethod(int given = 5) => given;
#endregion

#region Inheritance
	private class Animal
	{
		protected string Desc = "Generic Animal";
		protected internal void WriteDescription() => Console.WriteLine(Desc);
	}
	private class Cow : Animal
	{
		public Cow()
		{
			Desc = "Cow";
			Moo();
		}
		private void Moo() => WriteDescription();
	}
#endregion
}