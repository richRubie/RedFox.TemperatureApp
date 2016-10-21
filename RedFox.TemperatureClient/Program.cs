using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

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

			// call api
			var client = new HttpClient();
			client.SetBearerToken(tokenResponse.AccessToken);

			var temp = new { temperature= 6};

			StringContent content = new StringContent(JsonConvert.SerializeObject(temp));
			
			var response = await client.PostAsync("http://localhost.fiddler:5001/api/temperature", content);
			if (!response.IsSuccessStatusCode)
			{
				Console.WriteLine(response.StatusCode);
			}
		}
	}
}
