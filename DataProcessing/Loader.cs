namespace DataProcessing;

public static class Loader
{
	public static MovieData[] LoadFromFile(string path)
	{
		StreamReader reader = new StreamReader(File.OpenRead(path));
		if (reader.EndOfStream) throw new ArgumentException(); // incorrect input, since it could not be read

		// collect all lines of data as strings (1 string per line)
		List<string> allLines = new List<string>();
		while (!reader.EndOfStream)
		{
			string? line = reader.ReadLine();
			if (line == null) continue;
			allLines.Add(line.Substring(1, line.Length)); // todo: fix?
		}
		allLines.RemoveAt(0);

		// collect all data
		List<MovieData> allData = new List<MovieData>();
		foreach (string line in allLines)
		{
			string?[] parts = DataStringsFromLine(line);
			Console.WriteLine($"Parts count: [{parts.Length}]");
			allData.Add(ParseData(parts));
		}

		// return data
		return allData.ToArray();
	}

	private static MovieData ParseData(string?[] parts)
	{
		return new MovieData
		{
			ShowId = parts[0],
			Type = MovieData.ParseType(parts[1]),
			Title = parts[2],
			Director = parts[3],
			Cast = MovieData.ParseCast(parts[4]),
			Country = parts[5],
			DateAdded = parts[6],
			ReleaseYear = MovieData.ParseYear(parts[7]),
			AgeRating = parts[8],
			Duration = parts[9],
			ListedIn = parts[10],
			Description = parts[11]
		};
	}

	private static string?[] DataStringsFromLine(string line)
	{
		// naive string builder approach
		// split by "," unless in quotes (""). Otherwise, add to builder.

		Console.WriteLine($"DEBUG; line: {line}"); // WHY IS THERE A QUOTE AT THE START

		List<string?> elements = new List<string?>();
		string builder = "";
		bool inQuotes = false;
		foreach (char c in line)
		{
			if (!inQuotes)
			{
				if (c == '\"')
				{
					inQuotes = true;
					continue;
				}
				if (c == ',')
				{
					// split
					if (builder != "")
					{
						elements.Add(builder);
						builder = "";
					}
					else
					{
						elements.Add(null);
					}
					continue;
				}
			}
			else
			{
				if (c == '\"')
				{
					inQuotes = false;
					continue;
				}
			}

			// if nothing else, just add character
			builder += c;
		}
		// add last part too
		elements.Add(builder == "" ? null : builder);

		Console.WriteLine("Split parts:");
		foreach (string? element in elements)
		{
			Console.WriteLine(element ?? "NULL");
		}

		return elements.ToArray();
	}
}