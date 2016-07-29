using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedFox.TemperatureApp.Business
{
	public class TemperatureLog
	{
		public int TemperatureLogId { get; set; }
		public decimal Temperature { get; set; }
		public DateTime LogDateTime { get; set; }
	}
}