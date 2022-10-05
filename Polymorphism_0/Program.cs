namespace Polymorphism_0;

internal static class Program
{
	public static void Main()
	{
		Examples0.Run();
		Console.Write(NewLine(3));
		Assignments0.Run();

		Examples1.Run();
		Console.Write(NewLine(3));
		Assignments1.Run();

		string NewLine(int amount = 1) => new string('\n', amount);
	}
}