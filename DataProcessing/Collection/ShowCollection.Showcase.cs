using DataProcessing.Types;

namespace DataProcessing.Collection;

public partial class ShowCollection // showcase part
{
	public void RunShowcase()
	{
		// 1) Get a random actor (from all actors)
		DisplayRandomActor();

		// 2) Get the title of a random movie (from all movies)
		DisplayRandomMovieTitle();

		// 3) Get the title of a random TV-show (from all TV-shows)
		DisplayRandomTVShowTitle();

		// 4) Get the title of a random show from a specific year (from year)
		const int exampleYear = 1992; // example
		DisplayRandomShowFromYear(exampleYear);

		// 5) Get proportion of movies to tv shows as percentages.
		DisplayShowTypeProportions();

		// 6) Get the titles of all shows with a specific person as actor or director (from name)
		const string exampleName = "Steven Spielberg"; // example
		DisplayTitlesWithPerson(exampleName);

		// 7) Get the title of all shows with a specific number of seasons
		const int exampleSeasonCount = 4;
		DisplayTitlesWithSeasonCount(exampleSeasonCount);

		// 8) Get all information on a specific show (from title)
		const string exampleTitle = "The Matrix"; // example
		DisplayInformationOnTitle(exampleTitle);

		// 9) Get the average length of movies with a specific age rating (from rating)
		const string exampleAgeRating = "PG-13";
		DisplayAverageLengthFromRating(exampleAgeRating);

		// 10) Get the 10 most used words in all show titles
		DisplayMostUsedWords();

		// 11.1) Remove shows
		RemoveTitleFromDataAndDisplay(exampleTitle);

		// 11.2) Add shows
		Show sonic = new Show
		{
			Title = "Sonic Prime",
			Type = ShowType.TVShow,
			AgeRating = "PG-7"
		};
		AddShowToDataAndDisplay(sonic);
	}
}