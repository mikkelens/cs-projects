﻿using System.Collections;
using OpenAI.GPT3;
using OpenAI.GPT3.Managers;

namespace eksamensprojekt;

public static class Utilities
{
	private static string ProjectPath => Directory.GetParent(Environment.CurrentDirectory)?.Parent!.Parent!.FullName!;
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

	public static readonly DisplayList<string> GPTStr = new DisplayList<string>(lastSeperation: "or") { "chat", "gpt" };
	public static readonly DisplayList<string> DALLEStr = new DisplayList<string>(lastSeperation: "or") { "dall", "dall-e" };
	public static AITool ParseAITool(this string? request)
	{
		if (request == null) return AITool.Invalid;
		request = request.ToLower();
		if (GPTStr.Any(request.Contains)) return AITool.ChatGPT;
		if (DALLEStr.Any(request.Contains)) return AITool.DALLE;
		return AITool.Invalid;
	}
}