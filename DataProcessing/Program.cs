
using System.Data;

namespace DataProcessing;

public static class Program
{
	public static void Main()
	{
		const string path = @"..\..\..\Data\netflix_titles.csv"; // works when running in debug mode
		IEnumerable<Show> processedData = Loader.LoadFromFile(path);

		// write all in alphabetical order
		// foreach (MovieData movie in processedData.OrderBy(show => show.Title))
		// {
		// 	Console.WriteLine($"Movie: {movie.Title}");
		// }

		Showcase.Run(new List<Show>(processedData));
		LetUserPlayWith(new List<Show>(processedData));

		Console.Write("\nPress any key to exit...");
		Console.ReadLine();
	}

	private static readonly (string command, string description)[] Commands =
	{
		("RandomActor", "Get a random actor (from all actors)."),
		("RandomMovieTitle", "Get the title of a random movie (from all movies)."),
		("RandomTVShowTitle", "Get the title of a random TV-Show (from all TV-shows)."),
		("RandomShowFromYear", "Get the title of a random show from a specific year."),
		("ShowTypeProportions", "Get proportion of movites to tv shows as a percentage."),
		("TitlesWithPerson", "Get the titles of all shows with a specific person as actor or director (from name)."),
		("TitlesWithSeasonCount", "Get the title of all shows with a specific number of seasons."),
		("InformationOnShow", "Get all information on a specific show (from title)."),
		("AverageLengthFromRating", "Get the average length of movies with a specific age rating (from rating)."),
		("MostUsedWords", "Get the 10 most used words in all show titles."),
		("RemoveTitleFromData", "Remove a show (from title)."),
		("AddShowToData", "Add a show (from title)"),
	};

	private static void LetUserPlayWith(List<Show> shows)
	{
		// display all commands
		Console.WriteLine("All commands:\n");
		foreach ((string command, string description) in Commands)
		{
			Console.WriteLine($"{command}: {description}");
		}

		string? input;
		do
		{
			input = Console.ReadLine();
			if (input == null)
			{
				Console.WriteLine("Invalid input. See list of commands above.");
				continue;
			}
		} while (!Commands.Select(x => x.command.ToLower()).Contains(input!.ToLower()));

		// build command
		switch (input.ToUpper())
		{
			case "RandomActor":

				break;
			default:
				throw new DataException();
		}
	}

}