using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedFox.TemperatureApp.Business;
using System;
using System.Linq;
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

		[HttpPost]
		public async Task<IActionResult> Post([FromBody]TemperatureHumidityModel data)
		{
			context.TemperatureHumidityData.Add(
				new TemperatureHumidityData() {
					Humidity = data.Humidity,
					Temperature = data.Temperature,
					Location = data.Location,
					LogDateTime = DateTime.UtcNow,
				});

			await context.SaveChangesAsync();

			return Ok();
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var results = await context.TemperatureHumidityData.OrderByDescending(d => d.LogDateTime).Take(1000).ToListAsync();

			return Ok(results);
		}
	}
}