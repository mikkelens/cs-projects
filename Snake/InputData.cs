namespace Snake;

public class InputData
{
	private readonly DateTime _time;
	public readonly ConsoleKey Key;

	public InputData(DateTime time, ConsoleKey key)
	{
		_time = time;
		Key = key;
	}

	public TimeSpan TimeSince()
	{
		return DateTime.Now - _time;
	}
}