using System.Net.Http.Headers;
using System.Text;

namespace eksamensprojekt.OpenAI.Utilities;

public class OpenAIUtilities
{
	private static string ProjectPath => Directory.GetParent(Environment.CurrentDirectory)?.Parent!.Parent!.FullName!;
	private static string KeyPath => $"{ProjectPath}/OpenAI/key.txt";
	private static string Secret => File.ReadLines(KeyPath).First();

	private const string Publishable = "YourAPIPublishableValue"; // idk what this is lol

	private const string APIPath = "https://api.openai.com/v1/gpt-3.5-turbo";
	private readonly HttpClient _client = new HttpClient();
	private readonly OpenAIModel _model = new OpenAIModel();

	public void GetResponse()
	{
		_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
			Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Publishable}:{Secret}")));
		// using var
	}
}