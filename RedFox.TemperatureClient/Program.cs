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
			//var disco = await DiscoveryClient.GetAsync("http://localhost:5000/");
			//var disco = await DiscoveryClient.GetAsync("http://localhost.fiddler:5000");

			var tokenClient = new TokenClient(disco.TokenEndpoint, "raspberryPiClient", "QChGXGOnjPBg7Xlf8S6m8tBavisAk2OG");
			var tokenResponse = await tokenClient.RequestClientCredentialsAsync("temperatureApi");

			if (tokenResponse.IsError)
			{
				Console.WriteLine(tokenResponse.Error);
				return;
			}

			Console.WriteLine(tokenResponse.Json);

			// call api
			var client = new HttpClient();
			client.SetBearerToken(tokenResponse.AccessToken);

			var temp = new { Temperature = 6, Humidity = 25 };

			StringContent content = new StringContent(JsonConvert.SerializeObject(temp), Encoding.UTF8, "application/json");

			var response = await client.PostAsync("https://redfox-app-temperatureapp.azurewebsites.net/api/temperaturehumidity", content);
			//var response = await client.PostAsync("http://localhost:5001/api/temperaturehumidity", content);
			//var response = await client.PostAsync("http://localhost.fiddler:5001/api/temperaturehumidity", content);
			if (!response.IsSuccessStatusCode)
			{
				Console.WriteLine(response.StatusCode);
			}
		}
	}
}
