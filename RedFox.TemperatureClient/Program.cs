using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace RedFox.TemperatureClient
{
	public class Program
    {
        public static void Main(string[] args)
        {
			Task.Run(() => RunAction()).Wait();
			Console.ReadLine();
		}

		private static async Task RunAction()
		{
			var disco = await DiscoveryClient.GetAsync("http://localhost.fiddler:5000");

			var tokenClient = new TokenClient(disco.TokenEndpoint, "client", "secret");
			var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api1");

			if (tokenResponse.IsError)
			{
				Console.WriteLine(tokenResponse.Error);
				return;
			}

			Console.WriteLine(tokenResponse.Json);
			Console.ReadLine();

			// call api
			var client = new HttpClient();
			client.SetBearerToken(tokenResponse.AccessToken);

			var response = await client.GetAsync("http://localhost:5001/api/values");
			if (!response.IsSuccessStatusCode)
			{
				Console.WriteLine(response.StatusCode);
			}

			var content = response.Content.ReadAsStringAsync().Result;
			Console.WriteLine(JArray.Parse(content));
		}
	}
}
