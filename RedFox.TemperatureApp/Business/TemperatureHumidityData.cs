using System;

namespace RedFox.TemperatureApp.Business
{
	public class TemperatureHumidityData
	{
		public int TemperatureHumidityDataId { get; set; }
		public decimal Temperature { get; set; }
		public DateTime LogDateTime { get; set; }
		public decimal Humidity { get; set; }
	}
}