// c# main function
namespace SortingAlgorithms;

internal static class Program
{
	private static Random _random = null!;
	
	private static void Main()
	{
	#region Assignment 1
		// get random generator
		const int seed = 1234;
		_random = new Random(seed);
		// fill collections with random numbers
		int[] ten = new int[10];
		int[] fifty = new int[50];
		int[] fivehundred = new int[500];
		int[] thousand = new int[1000];
		int[] hundredthousand = new int[50000];
		List<int[]> allNumbers = new List<int[]> { ten, fifty, fivehundred, thousand, hundredthousand };
		
		allNumbers.ForEach(collection => FillWithRandomNumbers(ref collection));
		// Console.WriteLine($"RANDOM NUMBERS:\n{FormatCollections(allNumbers)}\n");
	#endregion

	#region Assignment 2
		// try sort
		allNumbers.ForEach(collection => SortCustom(ref collection));
		Console.WriteLine($"TRY SORT:\n{FormatCollections(allNumbers)}\n");
		
		// verify
		// allNumbers.ForEach(Array.Sort);
		// Console.WriteLine($"TRY SORT:\n{FormatCollections(allNumbers)}\n");
	#endregion

		// program end
		Console.Write("Press any key to exit.");
		Console.ReadLine();
	}
	
	private static void SortCustom(ref int[] numbers)
	{
		// left to right
		for (int i = 0; i < numbers.Length; i++)
		{
			// start point (right-ish (from zero)) towards left
			for (int j = i; j > 0; j--) // does not include zero index
			{
				int value = numbers[j];
				int valueOnLeft = numbers[j - 1];

				if (value >= valueOnLeft) continue;
				
				// swap values
				numbers[j - 1] = value;
				numbers[j] = valueOnLeft;
			}
		}
	}

	private static void FillWithRandomNumbers(ref int[] collection)
	{
		// fill collection
		for (int i = 0; i < collection.Length; i++)
		{
			collection[i] = _random.Next(0, 100);
		}
	}

	private static string FormatCollections(List<int[]> collections)
	{
		string result = "";
		foreach (int[] collection in collections)
		{
			if (collection.Length < 1000000)
			{
				// display raw
				result += $"Collection of length {collection.Length}:\n{string.Join(", ", collection)}\n";
			}
			else
			{
				// display with "..." between first 5000 and last 5000
			}
		}
		return result;
	}
}