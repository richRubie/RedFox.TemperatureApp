using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace RedFox.TemperatureApp.Business
{
	public class TemperatureLogMap : EntityTypeConfiguration<TemperatureLog>
	{
		public TemperatureLogMap()
		{

		}
	}
}