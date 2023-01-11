using DataProcessing.Collection;
using DataProcessing.Types;

namespace DataProcessing;

public static class Loader
{
	public static ShowCollection LoadFromFile(string path)
	{
		StreamReader reader = new StreamReader(File.OpenRead(path));
		if (reader.EndOfStream) throw new ArgumentException(); // incorrect input, since it could not be read

		// collect all lines
		List<string> allLines = new List<string>();
		while (!reader.EndOfStream)
		{
			string? line = reader.ReadLine();
			if (line == null) continue;
			allLines.Add(line.Trim().Trim('"'));
		}
		allLines.RemoveAt(0); // remove first entry because it is example data

		// parse for each line and return them all
		return new ShowCollection(allLines.Select(DataStringsFromLine).Select(ParseParts).ToList());
	}

	private static Show ParseParts(string?[] parts)
	{
		return new Show
		{
			Id = parts[0],
			Type = Show.ParseType(parts[1]),
			Title = parts[2],
			Director = parts[3],
			Cast = Show.ParseCast(parts[4]),
			Country = parts[5],
			DateAdded = parts[6],
			ReleaseYear = Show.ParseYear(parts[7]),
			AgeRating = parts[8],
			Duration = Show.ParseDuration(parts[9]),
			ListedIn = parts[10],
			Description = parts[11]?.Trim(';')
		};
	}

	private static string?[] DataStringsFromLine(string line)
	{
		// naive string builder approach
		// split by "," unless in quotes (""). Otherwise, add to builder.

		List<string?> elements = new List<string?>();
		string elementTemp = "";
		int quoteCount = 0;
		bool inDoubleQuotes = false;
		foreach (char c in line)
		{
			// double quote handling
			if (c == '\"')
			{
				if (!inDoubleQuotes)
				{
					quoteCount++;
					if (quoteCount == 2)
					{
						inDoubleQuotes = true;
					}
				}
				else
				{
					quoteCount--;
					if (quoteCount == 0)
					{
						inDoubleQuotes = false;
					}
				}
				continue; // ignore all quote chars
			}

			if (c == ',' && !inDoubleQuotes)
			{
				// split
				elements.Add(elementTemp != "" ? elementTemp.Trim() : null);
				elementTemp = "";
				continue; // ignore splitting char
			}

			// if nothing else, just add character
			elementTemp += c;
		}
		// add last part too
		elements.Add(elementTemp == "" ? null : elementTemp.Trim());

		return elements.ToArray();
	}
}