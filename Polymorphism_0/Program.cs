namespace Polymorphism_0;

internal static class Program
{
	public static void Main()
	{
		Examples.Run();
		Console.Write(NewLine(3));
		Assignments.Run();

		string NewLine(int amount = 1) => new string('\n', amount);
	}
}