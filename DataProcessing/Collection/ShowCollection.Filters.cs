using DataProcessing.Types;

namespace DataProcessing.Collection;

public partial class ShowCollection // filters
{
#region Filters
	private string GetRandomActor()
	{
		return GetAllDistinctActors().RandomElement(); // random actor name

		string[] GetAllDistinctActors()
		{
			 List<string> actors = new List<string>();
			 foreach (Show show in _shows)
			 {
				   if (show.Cast == null) continue;
				   foreach (string actor in show.Cast)
				   {
						if (actors.Contains(actor)) continue;
						actors.Add(actor);
				   }
			 }
			 return actors.ToArray();
		}
	}
	private string GetRandomMovieTitle()
	{
		return GetAllDistinctMovieTitles().RandomElement();

		string[] GetAllDistinctMovieTitles()
		{
			 List<string> movieTitles = new List<string>();
			 foreach (Show show in _shows)
			 {
				   if (show.Title == null) continue;
				   if (show.Type != ShowType.Movie) continue;
				   if (movieTitles.Contains(show.Title)) continue;
				   movieTitles.Add(show.Title);
			 }
			 return movieTitles.ToArray();
		}
	}
	private string GetRandomTVShowTitle()
	{
		return GetAllDistinctTVShowTitles().RandomElement();

		string[] GetAllDistinctTVShowTitles()
		{
			 // throw new NotImplementedException();

			 List<string> tvShowTitles = new List<string>();
			 foreach (Show show in _shows)
			 {
				   if (show.Title == null) continue;
				   if (show.Type != ShowType.TVShow) continue;
				   if (tvShowTitles.Contains(show.Title)) continue;
				   tvShowTitles.Add(show.Title);
			 }
			 return tvShowTitles.ToArray();
		}
	}
	private string GetRandomTitleFromYear(int releaseYear)
	{
		return GetAllDistinctShowTitlesFromYear().RandomElement();

		string[] GetAllDistinctShowTitlesFromYear()
		{
			 List<string> showTitles = new List<string>();
			 foreach (Show show in _shows)
			 {
				   if (show.Title == null) continue;
				   if (show.ReleaseYear != releaseYear) continue;
				   if (showTitles.Contains(show.Title)) continue;
				   showTitles.Add(show.Title);
			 }
			 return showTitles.ToArray();
		}
	}
	private (float movieShare, float tvShowShare) GetShowTypeShares()
	{
		int movies = GetCountOfType(ShowType.Movie);
		int tvShows = GetCountOfType(ShowType.TVShow);
		int total = movies + tvShows;
		return ((float)movies / total, (float)tvShows / total);

		int GetCountOfType(ShowType type)
		{
			 return _shows.Where(show => show.Type == type).ToArray().Length;
		}
	}
	private string[] GetShowTitlesWithPerson(string person)
	{
		List<string> titlesWithPerson = new List<string>();
		foreach (Show show in _shows)
		{
			 if (show.Title == null) continue;
			 if (show.Director != person && (show.Cast == null || !show.Cast.Contains(person))) continue;
			 titlesWithPerson.Add(show.Title);
		}
		return titlesWithPerson.ToArray();
	}
	private string[] GetShowTitlesWithSeasonCount(int count)
	{
		// assume seasons are the duration field of TV shows, where movies are in minutes.
		List<string> titlesWithSeasonCount = new List<string>();
		foreach (Show show in _shows)
		{
			 if (show.Title == null) continue;
			 if (show.Duration is not { type: DurationType.Seasons }) continue;
			 if (show.Duration.Value.amount != count) continue;
			 titlesWithSeasonCount.Add(show.Title);
		}
		return titlesWithSeasonCount.ToArray();
	}
	private Show GetShowFromTitle(string title)
	{
		return _shows.First(show => show.Title == title);
	}
	private int GetAverageLengthFromRating(string rating)
	{
		return (int)GetAllMovieLengthForRating().Average();

		IEnumerable<int> GetAllMovieLengthForRating()
		{
			 List<int> durationsForRating = new List<int>();
			 foreach (Show show in _shows)
			 {
				   if (show.Type != ShowType.Movie) continue;
				   if (show.Duration is not { type: DurationType.Minutes }) continue;
				   if (show.AgeRating == null || show.AgeRating != rating) continue;
				   durationsForRating.Add(show.Duration.Value.amount);
			 }
			 return durationsForRating.ToArray();
		}
	}

	private IEnumerable<(string word, int count)> GetTenMostUsedWordsInTitles()
	{
		// count up all words in dictionary
		Dictionary<string, int> wordCounts = new Dictionary<string, int>();
		foreach (Show show in _shows)
		{
			 if (show.Title == null) continue;
			 foreach (string untrimmedWord in show.Title.Trim().Split(' '))
			 {
				   string word = untrimmedWord.Trim(',', '-', ':', ';', '&').ToLower(); // some removed symbols
				   if (word == "") continue;
				   if (wordCounts.ContainsKey(word))
				   {
						wordCounts[word]++; // count word appearance
				   }
				   else
				   {
						wordCounts.Add(word, 1);
				   }
			 }
		}

		// make ordered list
		List<(string, int)> wordList = wordCounts
			 .Select(word => ValueTuple.Create(word.Key, word.Value))
			 .OrderByDescending(x => x.Item2)
			 .ToList();

		// return 10 highest as array
		return wordList.GetRange(0, 10).ToArray();
	}
#endregion
}