using eksamensprojekt.OpenAI.Utilities;

namespace eksamensprojekt;

public static class Program
{
	internal static void Main()
	{
		new OpenAIUtilities().GetResponse();

		Console.WriteLine("\n\n\nProgram finished. Press any key to exit.");
		Console.ReadLine();
	}
}