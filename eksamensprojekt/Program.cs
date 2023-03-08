using OpenAI.GPT3;
using OpenAI.GPT3.Interfaces;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels;
using OpenAI.GPT3.ObjectModels.RequestModels;
using OpenAI.GPT3.ObjectModels.ResponseModels;
using OpenAI.GPT3.ObjectModels.ResponseModels.ImageResponseModel;

namespace eksamensprojekt;

public static class Program
{
	private static async Task Main()
	{
		do
		{
			Console.Clear();
			await MakeUserExchange();
		} while (BoolAsk("Do you want to use another tool?"));

		Console.WriteLine("\n\n\nProgram finished. Press any key to exit.");
		Console.ReadLine();
	}

	private static string? PromptUser(string prompt)
	{
		Console.Write($"{prompt}\n> ");
		return Console.ReadLine();
	}

	private enum BoolAnswer
	{
		Invalid,
		Yes,
		No
	}
	private const char YesChar = 'y';
	private const char NoChar = 'n';
	private static BoolAnswer ParseBoolAnswer(this string? request)
	{
		if (request == null) return BoolAnswer.Invalid;
		if (request.Contains(YesChar)) return BoolAnswer.Yes;
		if (request.Contains(NoChar)) return BoolAnswer.No;
		return BoolAnswer.Invalid;
	}
	private static bool BoolAsk(string yesNoQuestion)
	{
		BoolAnswer answer = PromptUser(yesNoQuestion).ParseBoolAnswer();
		while (answer == BoolAnswer.Invalid)
		{
			answer = PromptUser($"Invalid answer? Try again (Must contain '{YesChar}' or '{NoChar}').").ParseBoolAnswer();
		}
		return answer == BoolAnswer.Yes;
	}

	private static async Task MakeUserExchange()
	{
		if (InitializeAIService() is not { } openAiService)
		{
			throw new Exception("AIService could not be initialized!");
			return;
		}

		await (ToolAsk($"\nChoose an AI tool to use: ChatGPT or DALL-E\n") switch
		{
			AITool.ChatGPT => ChatGPTConversation(openAiService),
			AITool.DALLE => DALLEImageGeneration(openAiService),
			AITool.Invalid or _ => throw new ArgumentOutOfRangeException()
		});
	}

	private enum AITool
	{
		Invalid,
		ChatGPT,
		DALLE
	}
	private const string GPTStr = "gpt";
	private const string DALLEStr = "dall";
	private static AITool ParseAITool(this string? request)
	{
		if (request == null) return AITool.Invalid;
		request = request.ToLower();
		if (request.Contains(GPTStr)) return AITool.ChatGPT;
		if (request.Contains(DALLEStr)) return AITool.DALLE;
		return AITool.Invalid;
	}
	private static AITool ToolAsk(string toolQuestion)
	{
		AITool tool = PromptUser(toolQuestion).ParseAITool();
		while (tool == AITool.Invalid)
		{
			tool = PromptUser($"Invalid answer? Try again (Must contain '{GPTStr} or '{DALLEStr}''").ParseAITool();
		}
		return tool;
	}

	private static OpenAIService? InitializeAIService()
	{
		const string orgFile = "org.txt";
		const string keyFile = "key.txt";

		string projectPath = Directory.GetParent(Environment.CurrentDirectory)?.Parent!.Parent!.FullName!;
		string orgPath = $"{projectPath}/{orgFile}";
		string keyPath = $"{projectPath}/{keyFile}";
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


	private static async Task DALLEImageGeneration(OpenAIService openAiService)
	{
		string? prompt;
		do
		{
			prompt = PromptUser("\nWrite a prompt for DALL-E. Type 'exit' to leave.");
			while (prompt == null)
			{
				prompt = PromptUser("\nPrompt was empty? Try again.");
			}

			ImageCreateResponse imageResult = await openAiService.Image.CreateImage(new ImageCreateRequest
			{
				Prompt = prompt,
				N = 1,
				Size = StaticValues.ImageStatics.Size.Size256,
				ResponseFormat = StaticValues.ImageStatics.ResponseFormat.Url,
			});

			if (imageResult.Successful)
			{
				Console.WriteLine($"NEW IMAGE URL: {imageResult.Results.First().Url}");
			}
			else
			{
				if (imageResult.Error is not { } error) throw new Exception("Unknown Error?");
				Console.WriteLine($"ERROR [{error.Code ??= "(unnamed)"}]: {error.Message}");
				break;
			}

		} while (!prompt.ToLower().Contains("exit"));
	}

	private static async Task ChatGPTConversation(IOpenAIService openAiService)
	{
		List<ChatMessage> messages = new List<ChatMessage>
		{
			ChatMessage.FromSystem("You are a helpful assistant."),
		};

		string? prompt;
		do // conversation loop
		{
			prompt = PromptUser("\nWrite a prompt for ChatGPT. Type 'exit' to leave.\n");
			while (prompt == null)
			{
				prompt = PromptUser("\nPrompt was empty? Try again.\n");
			}
			if (prompt.ToLower().Contains("exit")) break;
			messages.Add(ChatMessage.FromUser(prompt));

			ChatCompletionCreateResponse chatResult = await openAiService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
			{
				Messages = messages,
				Model = Models.ChatGpt3_5Turbo
			});
			if (chatResult.Successful)
			{
				ChatMessage reply = chatResult.Choices.First().Message;
				messages.Add(reply);
				Console.WriteLine($"\nChatGPT reply: {reply.Content}");
			}
			else
			{
				if (chatResult.Error is not { } error) throw new Exception("Unknown Error?");
				Console.WriteLine($"ERROR [{error.Code ??= "(unnamed)"}]: {error.Message}");
				break;
			}
		} while (!prompt.ToLower().Contains("exit")); // continue conversation

		Console.WriteLine("\nConversation ended.\n");
	}
}