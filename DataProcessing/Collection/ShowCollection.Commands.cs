namespace DataProcessing.Collection;

public partial class ShowCollection
{
	private delegate void CommandMethod();

	public void LetUserPlayWith()
	{
		Dictionary<string, (string description, CommandMethod methodToCall)> commands
			= new Dictionary<string, (string description, CommandMethod methodToCall)>(StringComparer.OrdinalIgnoreCase)
		{
			{ "RandomActor", ("Get a random actor (from all actors).", DisplayRandomActor) },
			{ "RandomMovieTitle", ("Get the title of a random movie (from all movies).", DisplayRandomMovieTitle) },
			{ "RandomTVShowTitle", ("Get the title of a random TV-Show (from all TV-shows).", DisplayRandomTVShowTitle) },
			{ "RandomShowFromYear", ("Get the title of a random show from a specific year.", RandomShowFromSpecifiedYear) },
			{ "ShowTypeProportions", ("Get proportion of movites to tv shows as a percentage.", DisplayShowTypeProportions) },
			{ "TitlesWithPerson", ("Get the titles of all shows with a specific person as actor or director (from name).", TitlesWithSpecifiedPerson) },
			{ "TitlesWithSeasonCount", ("Get the title of all shows with a specific number of seasons.", TitlesWithSpecificSeasonCount) },
			{ "InformationOnShow", ("Get all information on a specific show (from title).", InformationOnSpecificTitle) },
			{ "AverageLengthFromRating", ("Get the average length of movies with a specific age rating (from rating).", AverageLengthFromSpecificRating) },
			{ "MostUsedWords", ("Get the 10 most used words in all show titles.", DisplayMostUsedWords) },
			{ "RemoveTitleFromData", ("Remove a show (from title).", RemoveSpecificTitleFromData) },
			{ "AddShowToData", ("Add a show (from title)", AddSpecificShowToData) },
		};

		bool continuePlaying;
		do
		{
			// display all commands
			Console.WriteLine("All commands:\n");
			foreach ((string command, (string description, _)) in commands)
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
				}
			} while (input == null || !commands.ContainsKey(input));

			commands[input].methodToCall();

			Console.WriteLine("\n");
			Thread.Sleep(1000);
			// todo: ask
			continuePlaying = true;
		} while (continuePlaying);
	}
}