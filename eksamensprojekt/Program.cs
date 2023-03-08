using System.Diagnostics;
using System.Net;
using OpenAI.GPT3.Interfaces;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels;
using OpenAI.GPT3.ObjectModels.RequestModels;
using OpenAI.GPT3.ObjectModels.ResponseModels;
using OpenAI.GPT3.ObjectModels.ResponseModels.ImageResponseModel;

namespace eksamensprojekt;

public static class Program
{
	private static IOpenAIService _openAiService = null!;

	private static async Task Main()
	{
		if (Utilities.InitializeAIService() is not { } openAiService) throw new Exception("AIService could not be initialized!");
		_openAiService = openAiService;

		do
		{
			Console.Clear();
			await (ToolAsk($"\nChoose an AI tool to use: ChatGPT or DALL-E\n") switch
			{
				AITool.ChatGPT => ChatGPTConversation(),
				AITool.DALLE => DALLEImageGeneration(),
				AITool.Invalid or _ => throw new ArgumentOutOfRangeException()
			});
		} while (BoolAsk("Do you want to use another tool?"));

		Console.WriteLine("\n\n\nProgram finished. Press any key to exit.");
		Console.ReadLine();
	}

	private static string? PromptUser(string prompt)
	{
		Console.Write($"{prompt}\n> ");
		return Console.ReadLine();
	}

	private static bool BoolAsk(string yesNoQuestion)
	{
		BoolAnswer answer = PromptUser(yesNoQuestion).ParseBoolAnswer();
		while (answer == BoolAnswer.Invalid)
		{
			answer = PromptUser($"Invalid answer? Try again (Must contain '{Utilities.YesChar}' or '{Utilities.NoChar}').").ParseBoolAnswer();
		}
		return answer == BoolAnswer.Yes;
	}
	private static AITool ToolAsk(string toolQuestion)
	{
		AITool tool = PromptUser(toolQuestion).ParseAITool();
		while (tool == AITool.Invalid)
		{
			tool = PromptUser($"Invalid answer? Try again (Must contain '{Utilities.GPTStr} or '{Utilities.DALLEStr}''").ParseAITool();
		}
		return tool;
	}

	private static async Task DALLEImageGeneration()
	{
		string? prompt;
		do
		{
			prompt = PromptUser("\nWrite a prompt for DALL-E. Type 'exit' to leave.");
			while (prompt == null)
			{
				prompt = PromptUser("\nPrompt was empty? Try again.");
			}

			ImageCreateResponse imageResult = await _openAiService.Image.CreateImage(new ImageCreateRequest
			{
				Prompt = prompt,
				N = 1,
				Size = StaticValues.ImageStatics.Size.Size256,
				ResponseFormat = StaticValues.ImageStatics.ResponseFormat.Url,
			});

			if (imageResult.Successful)
			{
				string url = imageResult.Results.First().Url;
				Console.WriteLine($"\nNEW IMAGE URL:\n{url}");
				if (BoolAsk("\nDo you want to download and open the image?"))
				{
#pragma warning disable SYSLIB0014
					using WebClient client = new WebClient();
#pragma warning restore SYSLIB0014
					const string imageName = "image.png";
					string imageDirectory = Utilities.ImageDirectoryPath;
					string imagePath = $"{imageDirectory}/{imageName}";
					await client.DownloadFileTaskAsync(new Uri(url), imagePath);
					Process.Start(new ProcessStartInfo(imageName)
					{
						Arguments = imageName,
						UseShellExecute = true,
						WorkingDirectory = imageDirectory,
						FileName = imagePath
					});
				}
			}
			else
			{
				if (imageResult.Error is not { } error) throw new Exception("Unknown Error?");
				Console.WriteLine($"ERROR [{error.Code ??= "(unnamed)"}]: {error.Message}");
				break;
			}

		} while (!prompt.ToLower().Contains("exit"));
	}

	private static async Task ChatGPTConversation()
	{
		List<ChatMessage> messages = new List<ChatMessage>
		{
			ChatMessage.FromSystem("You are a helpful assistant.")
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

			ChatCompletionCreateResponse chatResult = await _openAiService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
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