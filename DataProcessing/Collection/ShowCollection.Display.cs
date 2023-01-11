using DataProcessing.Types;

namespace DataProcessing.Collection;

public partial class ShowCollection // display
{
#region Display
	private void DisplayRandomActor()
	{
		Console.WriteLine($"Random actor: {GetRandomActor()}\n");
	}
	private void DisplayRandomMovieTitle()
	{
		Console.WriteLine($"Random movie title: {GetRandomMovieTitle()}\n");
	}
	private void DisplayRandomTVShowTitle()
	{
		Console.WriteLine($"Random TV-show title: {GetRandomTVShowTitle()}\n");
	}
	private void DisplayRandomShowFromYear(int year)
	{
		Console.WriteLine($"Random show from '{year}': {GetRandomTitleFromYear(year)}\n");
	}
	private void DisplayShowTypeProportions()
	{
		(float movieShare, float tvShowShare) = GetShowTypeShares();
		Console.WriteLine($"Movies: {movieShare * 100}%, TV-Shows: {tvShowShare * 100}%\n");
	}
	private void DisplayTitlesWithPerson(string name)
	{
		Console.WriteLine($"Shows with '{name}' as actor or director:\n" +
		                  $"{string.Join(", ", GetShowTitlesWithPerson(name))}]\n");
	}
	private void DisplayTitlesWithSeasonCount(int seasonCount)
	{
		Console.WriteLine($"Shows with {seasonCount} seasons:\n" +
		                  $"{string.Join(", ", GetShowTitlesWithSeasonCount(seasonCount))}\n");
	}
	private void DisplayInformationOnTitle(string title)
	{
		Console.WriteLine($"Information on '{title}':\n{GetShowFromTitle(title)}\n");
	}
	private void DisplayAverageLengthFromRating(string rating)
	{
		Console.WriteLine($"Average length for movies rated '{rating}': " +
		                  $"{GetAverageLengthFromRating(rating)} minutes.\n");
	}
	private void DisplayMostUsedWords()
	{
		string mostUsedWords = string.Join(", ", GetTenMostUsedWordsInTitles()
		.Select(x => $"'{x.word}': {x.count.ToString()}")); // display tuples
		Console.WriteLine($"10 most used words in titles: {mostUsedWords}\n");
	}
	private void RemoveTitleFromDataAndDisplay(string title)
	{
		if (_shows.Remove(GetShowFromTitle(title)))
		{
			Console.WriteLine($"Successfully removed '{title}' from the show list.\n");
		}
	}
	private void AddShowToDataAndDisplay(Show showToAdd)
	{
		_shows.Add(showToAdd);
		Console.WriteLine($"{showToAdd.Title} was added to the show list.\n");
	}
#endregion
}