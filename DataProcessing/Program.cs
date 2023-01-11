using DataProcessing.Collection;

namespace DataProcessing;

public static class Program
{
	public static void Main()
	{
		const string path = @"..\..\..\Data\netflix_titles.csv"; // works when running in debug mode
		ShowCollection collection = Loader.LoadFromFile(path);

		// write all in alphabetical order
		// foreach (MovieData movie in processedData.OrderBy(show => show.Title))
		// {
		// 	Console.WriteLine($"Movie: {movie.Title}");
		// }

		// Showcase.Run(new List<Show>(processedData));
		collection.LetUserPlayWith();

		Console.Write("\nPress any key to exit...");
		Console.ReadLine();
	}

}