namespace Polymorphism_0;

public static class Assignments0
{
	// 1)
	private static int Smallest(this int[] numbers)
	{
		return numbers.Min();
	}

	// 2)
	private static float Smallest(this float[] numbers)
	{
		return numbers.Min();
	}

	// 3)
	private class Item
	{
		protected string ItemType = "Unnamed";
		public void Describe()
		{
			Console.WriteLine($"Type: {ItemType}");
		}
	}

	// 4)
	private class Spoon : Item
	{
		public Spoon() => ItemType = "Spoon";
	}

	private class Shovel : Item
	{
		public Shovel() => ItemType = "Shovel";
	}

	private class Phone : Item
	{
		public Phone() => ItemType = "Phone";
	}

	private static readonly Random Random = new Random();
	public static void Run()
	{
		int[] ints = { 1, 2, 3 };
		Console.WriteLine($"1) smallest of {nameof(ints)}: {ints.Smallest()}");
		float[] floats = { 1.2f, 2.6f, 3.1f };
		Console.WriteLine($"2) Smallest of {nameof(floats)}: {floats.Smallest()}");

		// 5)
		Item[] myItems = RandomItems(10);

		Item[] RandomItems(int amount)
		{
			Item[] items = new Item[amount];
			for (int i = 0; i < amount; i++)
			{
				items[i] = RandomItem();
			}
			return items;

			Item RandomItem()
			{
				Item[] possibleItems = { new Spoon(), new Shovel(), new Phone() };
				return possibleItems[Random.Next(3)];
			}
		}

		// 6)
		foreach (Item myItem in myItems)
		{
			myItem.Describe();
		}
	}
}