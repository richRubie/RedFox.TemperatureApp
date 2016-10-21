﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedFox.TemperatureApp.Business;
using System;
using System.Threading.Tasks;

namespace RedFox.TemperatureApp.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	public class TemperatureController : ControllerBase
	{
		private readonly Context context;

		public TemperatureController(Context context)
		{
			this.context = context;
		}

		public async Task<IActionResult> Post([FromBody]decimal temperature)
		{
			context.TemperatureLogs.Add(new TemperatureLog() { Temperature = temperature, LogDateTime = DateTime.UtcNow, });

			await context.SaveChangesAsync();

			return Ok();
		}
	}
}
