namespace DataProcessing.Types;

public struct Show
{
	public string? Id;
	public ShowType? Type;
	public string? Title;
	public string? Director;
	public string[]? Cast; // cast as in actors etc
	public string? Country;
	public string? DateAdded; // todo: proper format
	public int? ReleaseYear;
	public string? AgeRating; // called "rating" in data
	public (DurationType type, int amount)? Duration; // minutes OR seasons
	public string? ListedIn; // category
	public string? Description; // in quotes

	public static ShowType? ParseType(string? str)
	{
		return str switch
		{
			"Movie" => ShowType.Movie,
			"TV Show" => ShowType.TVShow,
			_ => null
		};
	}
	public static string[]? ParseCast(string? str) // cast as in actors etc
	{
		return str?.Split(',')
			.Select(castMember => castMember.Trim()).ToArray();
	}

	public static int? ParseYear(string? str)
	{
		if (str == null) return null;
		return int.Parse(str);
	}

	public static (DurationType, int)? ParseDuration(string? str) // not usable because TV shows are in seasons
	{
		if (str == null) return null;

		str = str.ToLower();

		if (str.Contains("seasons"))
		{
			return (DurationType.Seasons, int.Parse(str.Split(' ').First()));
		}

		if (str.Contains("min"))
		{
			return (DurationType.Minutes, int.Parse(str.Split(' ').First()));
		}

		return null;
	}

	// alt+insert with resharper to autogenerate ToString() struct display
	public override string ToString()
	{
		return $"{nameof(Id)}: {Id}, {nameof(Type)}: {Type}, {nameof(Title)}: {Title}, {nameof(Director)}: {Director}, {nameof(Cast)}: {(Cast == null ? "NULL" : string.Join(", ", Cast))}, {nameof(Country)}: {Country}, {nameof(DateAdded)}: {DateAdded}, {nameof(ReleaseYear)}: {ReleaseYear}, {nameof(AgeRating)}: {AgeRating}, {nameof(Duration)}: {Duration}, {nameof(ListedIn)}: {ListedIn}, {nameof(Description)}: {Description}";
	}
}