namespace eksamensprojekt;

public class DisplayList<T> : List<T>
{
	private readonly char _polySeperator;
	private readonly string _lastSeperation;

	public DisplayList(string lastSeperation = "&", char polySeperator = ',')
	{
		_polySeperator = polySeperator;
		_lastSeperation = lastSeperation;
	}

	public override string ToString()
	{
		return $"[{Count switch
		{
			0 => "",
			1 => this.First()!.ToString()!,
			_ => $"{string.Join(_polySeperator, this, 0, Count - 1)} {_lastSeperation} {this.Last()!}"
		}}]";
	}
}