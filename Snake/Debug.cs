namespace Snake;

// [SuppressMessage("ReSharper", "UnusedType.Global")]
// [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public static class Debug
{
	private static readonly (int x, int y) PauseIndicatorPosition = (Game.AreaSizes.width + 3, 2);
	private const string PauseString = "PAUSED!";
	private const string UnpausedString = "PLAYING...";

	public static (int x, int y) DebugWritePosition { private get; set; } = (0, 0);

	private static void Log(string msg)
	{
		WriteAtPosition(msg, DebugWritePosition);
	}
	private static void LogAndPauseForSeconds(string msg, float seconds = 1)
	{
		Log(msg);
		PauseForSeconds(seconds);
	}

	public static void TemporaryPauseLog(string msg, float seconds = 1)
	{
		LogAndPauseForSeconds(msg, seconds);
		ClearAmount(msg.Length);
		Thread.Sleep(100);
	}

	public static void PauseForSeconds(float seconds)
	{
		WriteAtPosition(PauseString, PauseIndicatorPosition);
		Thread.Sleep((int)(seconds * 1000));
		WriteAtPosition(UnpausedString, PauseIndicatorPosition);
	}

	public static void ClearAmount(int msgLength)
	{
		Log(new string(' ', msgLength));
	}

	private static void WriteAtPosition(string msg, (int x, int y) position)
	{
		Console.SetCursorPosition(position.x, position.y);
		Console.WriteLine(msg);
	}
}