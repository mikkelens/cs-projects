namespace DataProcessing;

public enum ShowType
{
	Movie,
	TvShow
}

public struct MovieData
{
	public string? ShowId;
	public ShowType? Type;
	public string? Title;
	public string? Director;
	public string[]? Cast; // cast as in actors etc
	public string? Country;
	public string? DateAdded; // todo: proper format
	public int? ReleaseYear;
	public string? AgeRating; // "rating" in data
	public string? Duration; // minutes, todo: proper format
	public string? ListedIn; // category
	public string? Description; // in quotes

	public static ShowType? ParseType(string? str)
	{
		return str switch
		{
			"Movie" => ShowType.Movie,
			"TV Show" => ShowType.TvShow,
			_ => null
		};
	}
	public static string[]? ParseCast(string? str) // cast as in actors etc
	{
		if (str == null) return null;
		return str.Split(',').Select(castMember => castMember.Trim()).ToArray();
	}

	public static int? ParseYear(string? str)
	{
		if (str == null) return null;
		return int.Parse(str);
	}
}