
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

		RunDataChallenges(new List<Show>(processedData));
	}

	private static void RunDataChallenges(List<Show> data)
	{
		// 1) Get a random actor (from all actors)
		Console.WriteLine($"Random actor: {Features.GetRandomActor(data.ToArray())}\n");

		// 2) Get the title of a random movie (from all movies)
		Console.WriteLine($"Random movie title: {Features.GetRandomMovieTitle(data.ToArray())}\n");

		// 3) Get the title of a random TV-show (from all TV-shows)
		Console.WriteLine($"Random TV-show title: {Features.GetRandomTVShowTitle(data.ToArray())}\n");

		// 4) Get the title of a random show from a specific year (from year)
		const int year = 1992; // example
		Console.WriteLine($"Random show from '{year}': {Features.GetRandomTitleFromYear(data.ToArray(), year)}\n");

		// 5) Get proportion of movies to tv shows as percentages.
		(float movieShare, float tvShowShare) = Features.GetShowTypeShares(data.ToArray());
		Console.WriteLine($"Movies: {movieShare * 100}%, TV-Shows: {tvShowShare * 100}%\n");

		// 6) Get the titles of all shows with a specific person as actor or director (from name)
		const string name = "Steven Spielberg"; // example
		Console.WriteLine($"Shows with '{name}' as actor or director:\n" +
		                  $"{string.Join(", ", Features.GetShowTitlesWithPerson(data.ToArray(), name))}]\n");

		// "7) Get the titles of all shows with a specific number of seasons"
		// ...is not possible with provided data, only with external requests/different data

		// 8) Get all information on a specific show (from title)
		const string title = "The Matrix"; // example
		Console.WriteLine($"Information on '{title}':\n{Features.GetShowFromTitle(data.ToArray(), title)}\n");

		// 9) Get the average length of movies with a specific age rating (from rating)
		const string rating = "PG-13";
		Console.WriteLine($"Average length for movies rated '{rating}': " +
		                  $"{Features.GetAverageLengthFromRating(data.ToArray(), rating)} minutes\n");

		// 10) Get the 10 most used words in all show titles
		string mostUsedWords = string.Join(", ", Features.GetTenMostUsedWordsInTitles(data.ToArray())
			.Select(x => $"'{x.word}': {x.count.ToString()}")); // display tuples
		Console.WriteLine($"10 most used words in titles: {mostUsedWords}\n");

		// 11.1) Remove shows
		if (data.Remove(Features.GetShowFromTitle(data.ToArray(), title)))
		{
			Console.WriteLine($"Successfully removed '{title}' from the show list.\n");
		}

		// 11.2) Add shows
		Show sonic = new Show
		{
			Title = "Sonic Prime",
			Type = ShowType.TVShow,
			AgeRating = "PG-7"
		};
		data.Add(sonic);
		Console.WriteLine($"{sonic.Title} was added to the show list.\n");
	}
}