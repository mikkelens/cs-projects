
namespace DataProcessing;

public static class Program
{
	public static void Main()
	{
		const string path = @"C:\Users\mikke\Desktop\repos\programmering-b\cs-projects\DataProcessing\DataProcessing\Data\netflix_titles.csv";
		MovieData[] netflixData = Loader.LoadFromFile(path)!;

		foreach (MovieData movie in netflixData)
		{
			Console.WriteLine($"Movie: {movie.Title}");
		}
	}
}