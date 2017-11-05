

using Microsoft.EntityFrameworkCore;

namespace RedFox.TemperatureApp.Business
{
	public class Context : DbContext
	{
		static Context()
		{
			//Database.SetInitializer(new MigrateDatabaseToLatestVersion<Context, Migrations.Configuration>());
		}

		public Context(DbContextOptions<Context> options)
			: base(options)
		{
		}

		public DbSet<TemperatureHumidityData> TemperatureHumidityData { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
		}
	}
}