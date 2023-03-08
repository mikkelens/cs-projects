using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace eksamensprojekt.OpenAI.Utilities;

public class OpenAIUtilities
{
	private static string ProjectPath => Directory.GetParent(Environment.CurrentDirectory)?.Parent!.Parent!.FullName!;
	private static string KeyPath => $"{ProjectPath}/OpenAI/key.txt";
	private static string Secret => File.ReadLines(KeyPath).First();

	private const string Publishable = "YourAPIPublishableValue"; // idk what this is lol

	private const string APIPath = "https://api.openai.com/v1/engines/davinci/search";
	private readonly HttpClient _client = new HttpClient();
	private readonly OpenAIModel _model = new OpenAIModel();

	public void GetResponse()
	{
		_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
			Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Publishable}:{Secret}")));
		using HttpResponseMessage response = _client.PostAsync(APIPath, FormatPostDataAsync()).Result;
		string result = response.Content.ReadAsStringAsync().Result;
		Console.WriteLine(JsonConvert.DeserializeObject(result));
	}

	private HttpContent FormatPostDataAsync()
	{
		_model.Documents = new [] { "White House", "hospital", "school" };
		_model.Query = "the president";
		string serializedModel = JsonConvert.SerializeObject(_model);
		StringContent httpContent = new StringContent(serializedModel, Encoding.UTF8, "application/json");
		Console.WriteLine(serializedModel);
		return httpContent;
	}
}