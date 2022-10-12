using System.Diagnostics.CodeAnalysis;

namespace Snake;

[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public static class Debug
{
	public static (int x, int y) WritePosition { private get; set; } = (0, 0);

	public static void Log(string msg)
	{
		Console.SetCursorPosition(WritePosition.x, WritePosition.y);
		Console.WriteLine(msg);
	}
	public static void LogAndPauseForSeconds(string msg, float seconds = 1)
	{
		Log(msg);
		Thread.Sleep((int)(seconds * 1000));
	}

	public static void TemporaryPauseLog(string msg, float seconds = 1)
	{
		LogAndPauseForSeconds(msg, seconds);
		ClearAmount(msg.Length);
		Thread.Sleep(100);
	}

	public static void ClearAmount(int msgLength)
	{
		Log(new string(' ', msgLength));
	}
}