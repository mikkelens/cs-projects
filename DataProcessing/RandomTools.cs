namespace DataProcessing;

public static class RandomTools
{
	public static T RandomElement<T>(this T[] elements)
	{
		return elements[Random.Shared.Next(elements.Length)];
	}
}