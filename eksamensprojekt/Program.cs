using OpenAI;
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
			await MakeUserExchange();
		} while (BoolAsk("Do you want to do another conversation?"));

		Console.WriteLine("\n\n\nProgram finished. Press any key to exit.");
		Console.ReadLine();
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
		const string orgPath = "org.txt";
		const string keyPath = "key.txt";
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
			DefaultModelId = Models.TextDavinciV3,
			Organization = org
		});

		List<ChatMessage> messages = new List<ChatMessage>
		{
			ChatMessage.FromSystem("You are a helpful assistant."),
		};


		string? prompt;
		do // conversation loop
		{
			prompt = PromptUser("Write a prompt for ChatGPT. Type 'exit' to leave.\n");
			while (prompt == null)
			{
				prompt = PromptUser("\nPrompt was empty? Try again.\n");
			}
			if (prompt.ToLower().Contains("text")) return;
			messages.Add(ChatMessage.FromUser(prompt));

			ChatCompletionCreateResponse result = await openAiService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
			{
				Messages = messages
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
				Console.WriteLine($"{error.Code}: {error.Message}");
				break;
			}
		} while (!prompt.ToLower().Contains("exit")); // continue conversation

		Console.WriteLine("\nConversation ended.");
	}

	private static string? PromptUser(string prompt)
	{
		Console.WriteLine(prompt);
		Console.Write("> ");
		return Console.ReadLine();
	}
}