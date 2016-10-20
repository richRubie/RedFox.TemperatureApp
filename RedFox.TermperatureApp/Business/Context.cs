using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace RedFox.TemperatureApp.Business
{
	public class Context :  DbContext
	{
		static Context()
		{
			Database.SetInitializer(new CreateDatabaseIfNotExists<Context>());
		}

		public Context()
			: base("Connection")
		{
		}

		public DbSet<TemperatureLog> TemperatureLogs { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
			modelBuilder.Configurations.Add(new TemperatureLogMap());
		}
	}
}