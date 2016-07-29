using RedFox.TemperatureApp.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RedFox.TemperatureApp.Controllers
{
	public class UnSafeTemperatureController : ApiController
	{
		public IHttpActionResult Post(decimal temperature)
		{
			using (var context = new Context())
			{
				context.TemperatureLogs.Add(new TemperatureLog() { Temperature = temperature, LogDateTime = DateTime.UtcNow, });
			}

			return Ok();
		}
	}
}
