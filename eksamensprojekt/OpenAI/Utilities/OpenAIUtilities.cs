namespace eksamensprojekt.Utilities;

public class OpenAIUtilities
{
	private const string Publishable = "YourAPIPublishableValue";
	private const string Secret = "YourAPiSecretValue";
	private const string Path = "https://api.openai.com/v1/gpt-3.5-turbo";
	private readonly HttpClient _client = new HttpClient();
	private new readonly OpenAIModel _model = new OpenAIModel();
}