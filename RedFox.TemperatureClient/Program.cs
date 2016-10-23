using IdentityModel.Client;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RedFox.TemperatureClient
{
	public class Program
    {
        public static void Main(string[] args)
        {
			Thread.Sleep(5000);
			Task.Run(() => RunAction()).Wait();
			Console.ReadLine();
		}

		private static async Task RunAction()
		{
			var disco = await DiscoveryClient.GetAsync("https://redfox-app-identityserver.azurewebsites.net");
			//var disco = await DiscoveryClient.GetAsync("http://localhost/redfox.identityserver/");
			//var disco = await DiscoveryClient.GetAsync("http://localhost.fiddler:5000");

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

			var temp = new { Temperature = 6 };

			StringContent content = new StringContent(JsonConvert.SerializeObject(temp), Encoding.UTF8, "application/json");

			var response = await client.PostAsync("https://redfox-app-temperatureapp.azurewebsites.net/api/temperature", content);
			//var response = await client.PostAsync("http://localhost/redfox.temperatureapp/api/temperature", content);
			//var response = await client.PostAsync("http://localhost.fiddler:5001/api/temperature", content);
			if (!response.IsSuccessStatusCode)
			{
				Console.WriteLine(response.StatusCode);
			}
		}
	}
}
