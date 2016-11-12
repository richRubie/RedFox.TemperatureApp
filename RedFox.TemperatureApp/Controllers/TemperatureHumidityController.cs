using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedFox.TemperatureApp.Business;
using System;
using System.Threading.Tasks;

namespace RedFox.TemperatureApp.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	public class TemperatureHumidityController : ControllerBase
	{
		private readonly Context context;

		public TemperatureHumidityController(Context context)
		{
			this.context = context;
		}

		public async Task<IActionResult> Post([FromBody]TemperatureHumidityModel data)
		{
			context.TemperatureHumidityData.Add(
				new TemperatureHumidityData() {
					Humidity = data.Humidity,
					Temperature = data.Temperature,
					LogDateTime = DateTime.UtcNow,
				});

			await context.SaveChangesAsync();

			return Ok();
		}
	}
}