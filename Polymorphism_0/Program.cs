namespace Polymorphism_0;

internal static class Program
{
	private static readonly string Space = new string('\n', 3);

	public static void Main()
	{
		Examples.Run();
		Console.Write(new string('\n', 3));
		Assignments.Run();
	}
}