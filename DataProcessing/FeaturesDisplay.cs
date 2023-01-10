namespace DataProcessing;

public static class FeaturesDisplay
{
	public static void RandomActor(List<Show> data)
	{
		Console.WriteLine($"Random actor: {Features.GetRandomActor(data.ToArray())}\n");
	}

	public static void RandomMovieTitle(List<Show> data)
	{
		Console.WriteLine($"Random movie title: {Features.GetRandomMovieTitle(data.ToArray())}\n");
	}

	public static void RandomTVShowTitle(List<Show> data)
	{
		Console.WriteLine($"Random TV-show title: {Features.GetRandomTVShowTitle(data.ToArray())}\n");
	}

	public static void RandomShowFromYear(List<Show> data, int year)
	{
		Console.WriteLine($"Random show from '{year}': {Features.GetRandomTitleFromYear(data.ToArray(), year)}\n");
	}

	public static void ShowTypeProportions(List<Show> data)
	{
		(float movieShare, float tvShowShare) = Features.GetShowTypeShares(data.ToArray());
		Console.WriteLine($"Movies: {movieShare * 100}%, TV-Shows: {tvShowShare * 100}%\n");
	}

	public static void TitlesWithPerson(List<Show> data, string name)
	{
		Console.WriteLine($"Shows with '{name}' as actor or director:\n" +
		                  $"{string.Join(", ", Features.GetShowTitlesWithPerson(data.ToArray(), name))}]\n");
	}

	public static void TitlesWithSeasonCount(List<Show> data, int seasonCount)
	{
		Console.WriteLine($"Shows with {seasonCount} seasons:\n" +
		                  $"{string.Join(", ", Features.GetShowTitlesWithSeasonCount(data.ToArray(), seasonCount))}\n");
	}

	public static void InformationOnShow(List<Show> data, string title)
	{
		Console.WriteLine($"Information on '{title}':\n{Features.GetShowFromTitle(data.ToArray(), title)}\n");
	}

	public static void AverageLengthFromRating(List<Show> data, string rating)
	{
		Console.WriteLine($"Average length for movies rated '{rating}': " +
		                  $"{Features.GetAverageLengthFromRating(data.ToArray(), rating)} minutes.\n");
	}

	public static void MostUsedWords(List<Show> data)
	{
		string mostUsedWords = string.Join(", ", Features.GetTenMostUsedWordsInTitles(data.ToArray())
		.Select(x => $"'{x.word}': {x.count.ToString()}")); // display tuples
		Console.WriteLine($"10 most used words in titles: {mostUsedWords}\n");
	}

	public static void RemoveTitleFromData(List<Show> data, string title)
	{
		if (data.Remove(Features.GetShowFromTitle(data.ToArray(), title)))
		{
			Console.WriteLine($"Successfully removed '{title}' from the show list.\n");
		}
	}

	public static void AddShowToData(ICollection<Show> data, Show showToAdd)
	{
		data.Add(showToAdd);
		Console.WriteLine($"{showToAdd.Title} was added to the show list.\n");
	}
}