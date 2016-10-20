using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedFox.TemperatureApp.Business;
using System;

namespace RedFox.TemperatureApp.Controllers
{
	[Authorize]
	public class TemperatureController : ControllerBase
	{
		public IActionResult Post(decimal temperature)
		{
			using (var context = new Context())
			{
				context.TemperatureLogs.Add(new TemperatureLog() { Temperature = temperature, LogDateTime = DateTime.UtcNow, });
			}

			return Ok();
		}
	}
}
