using OpenAI.GPT3;
using OpenAI.GPT3.Managers;

namespace eksamensprojekt;

public static class Utilities
{
	public static string ProjectPath => Directory.GetParent(Environment.CurrentDirectory)?.Parent!.Parent!.FullName!;
	public static string ImageDirectoryPath => $"{ProjectPath}/ImageOutput";

	public static OpenAIService? InitializeAIService()
	{
		const string orgFile = "org.txt";
		const string keyFile = "key.txt";

		string orgPath = $"{ProjectPath}/{orgFile}";
		string keyPath = $"{ProjectPath}/{keyFile}";
		if (!File.Exists(orgPath) || File.ReadLines(orgPath).FirstOrDefault() is not { } org)
		{
			Console.WriteLine($"'{orgPath}' file missing.");
			return null;
		}

		if (!File.Exists(keyPath) || File.ReadLines(keyPath).FirstOrDefault() is not { } key)
		{
			Console.WriteLine($"'{keyPath}' file missing.");
			return null;
		}

		return new OpenAIService(new OpenAiOptions
		{
			ApiKey = key,
			Organization = org
		});
	}

	public const char YesChar = 'y';
	public const char NoChar = 'n';
	public static BoolAnswer ParseBoolAnswer(this string? request)
	{
		if (request == null) return BoolAnswer.Invalid;
		if (request.Contains(YesChar)) return BoolAnswer.Yes;
		if (request.Contains(NoChar)) return BoolAnswer.No;
		return BoolAnswer.Invalid;
	}

	public const string GPTStr = "gpt";
	public const string DALLEStr = "dall";
	public static AITool ParseAITool(this string? request)
	{
		if (request == null) return AITool.Invalid;
		request = request.ToLower();
		if (request.Contains(GPTStr)) return AITool.ChatGPT;
		if (request.Contains(DALLEStr)) return AITool.DALLE;
		return AITool.Invalid;
	}
}