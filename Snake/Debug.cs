namespace Snake;

public static class Debug
{
#region Constants etc
	private const string PauseString = "PAUSED!";
	private const string UnpausedString = "PLAYING...";
#endregion

#region Computed positions
	private static (int x, int y) LogWritePosition => (PauseIndicatorPosition.x, PauseIndicatorPosition.y + 2);
	private static (int x, int y) InputRequestWritePosition => (LogWritePosition.x, LogWritePosition.y + 1);
	private static (int x, int y) PauseIndicatorPosition => (Game.Board.DrawPositionWithinArea(Game.Board.Sizes).x + 3, 2);
#endregion

#region Extension methods
	private static string AsSpaces(this string msg)
	{
		return new string(' ', msg.Length);
	}
#endregion

#region State etc
	private static bool _paused;
	private static bool Paused
	{
		set
		{
			_paused = value;
			ClearLineAtPosition(PauseIndicatorPosition);
			WriteAtPosition(_paused ? PauseString : UnpausedString, PauseIndicatorPosition);
		}
	}

#endregion

#region Private
	private static void WriteAtPosition(string msg, (int x, int y) position)
	{
		Console.SetCursorPosition(position.x, position.y);
		Console.WriteLine(msg);
	}
	private static void Log(string msg)
	{
		ClearLineAtPosition(LogWritePosition);
		WriteAtPosition(msg, LogWritePosition);
	}
	private static void ClearMessageFromPosition(string msg, (int x, int y) position)
	{
		WriteAtPosition(msg.AsSpaces(), position);
	}
	private static void ClearLineAtPosition((int x, int y) position)
	{
		WriteAtPosition(new string(' ', Console.WindowWidth - position.x), position);
	}
	private static void LogAndPauseForSeconds(string msg, float seconds = 1)
	{
		Log(msg);
		PauseForSeconds(seconds);
		// does not clear itself afterwards on its own
	}
	private static void PauseForSeconds(float seconds)
	{
		Paused = true;
		Thread.Sleep((int)(seconds * 1000));
		Paused = false;
	}
	private static void PauseUntillEnter()
	{
		Paused = true;
		const string continueText = "Press enter to continue.";
		WriteAtPosition(continueText, InputRequestWritePosition);
		Console.ReadLine();
		ClearMessageFromPosition(continueText, InputRequestWritePosition);
		Paused = false;
	}
#endregion

#region Accessible (public) debug tools
	// ReSharper disable UnusedMember.Global
	public static void TemporaryPauseLog(string msg, float seconds = 1)
	{
		LogAndPauseForSeconds(msg, seconds);
		ClearMessageFromPosition(msg, LogWritePosition); // clears afterwards
	}
	public static void WaitingPauseLog(string msg)
	{
		Log(msg);
		PauseUntillEnter();
		ClearMessageFromPosition(msg, LogWritePosition);
	}
	// ReSharper restore UnusedMember.Global
#endregion
}