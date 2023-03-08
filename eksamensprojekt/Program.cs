using OpenAI.GPT3;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels;
using OpenAI.GPT3.ObjectModels.RequestModels;
using OpenAI.GPT3.ObjectModels.ResponseModels;

namespace eksamensprojekt;

public static class Program
{
	private static async Task Main()
	{
		do
		{
			Console.Clear();
			await MakeUserExchange();
		} while (BoolAsk("Do you want to do another conversation?"));

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
		const string orgFile = "org.txt";
		const string keyFile = "key.txt";

		string projectPath = Directory.GetParent(Environment.CurrentDirectory)?.Parent!.Parent!.FullName!;
		string orgPath = $"{projectPath}/{orgFile}";
		string keyPath = $"{projectPath}/{keyFile}";
		if (!File.Exists(orgPath) || File.ReadLines(orgPath).FirstOrDefault() is not { } org)
		{
			Console.WriteLine($"'{orgPath}' file missing.");
			return;
		}
		if (!File.Exists(keyPath) || File.ReadLines(keyPath).FirstOrDefault() is not { } key)
		{
			Console.WriteLine($"'{keyPath}' file missing.");
			return;
		}

		OpenAIService openAiService = new OpenAIService(new OpenAiOptions
		{
			ApiKey = key,
			Organization = org
		});

		await ChatGPTConversation(openAiService);

		Console.WriteLine("\nConversation ended.\n");
	}

	private static async Task ChatGPTConversation(OpenAIService openAiService)
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

			ChatCompletionCreateResponse result = await openAiService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
			{
				Messages = messages,
				Model = Models.ChatGpt3_5Turbo
			});
			if (result.Successful)
			{
				ChatMessage reply = result.Choices.First().Message;
				messages.Add(reply);
				Console.WriteLine($"\nChatGPT reply: {reply.Content}");
			}
			else
			{
				if (result.Error is not { } error) throw new Exception("Unknown Error?");
				Console.WriteLine($"ERROR [{error.Code ??= "(unnamed)"}]: {error.Message}");
				break;
			}
		} while (!prompt.ToLower().Contains("exit")); // continue conversation
	}
}