namespace DataProcessing;

public static class Showcase
{
	public static void Run(List<Show> data)
	{
		// 1) Get a random actor (from all actors)
		FeaturesDisplay.RandomActor(data);

		// 2) Get the title of a random movie (from all movies)
		FeaturesDisplay.RandomMovieTitle(data);

		// 3) Get the title of a random TV-show (from all TV-shows)
		FeaturesDisplay.RandomTVShowTitle(data);

		// 4) Get the title of a random show from a specific year (from year)
		const int exampleYear = 1992; // example
		FeaturesDisplay.RandomShowFromYear(data, exampleYear);

		// 5) Get proportion of movies to tv shows as percentages.
		FeaturesDisplay.ShowTypeProportions(data);

		// 6) Get the titles of all shows with a specific person as actor or director (from name)
		const string exampleName = "Steven Spielberg"; // example
		FeaturesDisplay.TitlesWithPerson(data, exampleName);

		// 7) Get the title of all shows with a specific number of seasons
		const int exampleSeasonCount = 4;
		FeaturesDisplay.TitlesWithSeasonCount(data, exampleSeasonCount);

		// 8) Get all information on a specific show (from title)
		const string exampleTitle = "The Matrix"; // example
		FeaturesDisplay.InformationOnShow(data, exampleTitle);

		// 9) Get the average length of movies with a specific age rating (from rating)
		const string exampleAgeRating = "PG-13";
		FeaturesDisplay.AverageLengthFromRating(data, exampleAgeRating);

		// 10) Get the 10 most used words in all show titles
		FeaturesDisplay.MostUsedWords(data);

		// 11.1) Remove shows
		FeaturesDisplay.RemoveTitleFromData(data, exampleTitle);

		// 11.2) Add shows
		Show sonic = new Show
		{
			Title = "Sonic Prime",
			Type = ShowType.TVShow,
			AgeRating = "PG-7"
		};
		FeaturesDisplay.AddShowToData(data, sonic);
	}
}