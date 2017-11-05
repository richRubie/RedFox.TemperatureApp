using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Reflection;

namespace RedFox.IdentityServer4
{
	public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddEnvironmentVariables();
			Configuration = builder.Build();
		}

		public IConfigurationRoot Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			var connectionString = Configuration.GetConnectionString("Connection");
			services.AddMvc();
			var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

			services.AddIdentityServer()
				.AddDeveloperSigningCredential()
				.AddInMemoryUsers(IdentityServer4.Configuration.Config.GetUsers())
			.AddInMemoryClients(IdentityServer4.Configuration.Config.GetClients());
			//services.AddIdentityServer()
			//.AddTemporarySigningCredential()
			//.AddConfigurationStore(builder =>
			//	builder.UseSqlServer(connectionString, options =>
			//		options.MigrationsAssembly(migrationsAssembly)))
			//.AddOperationalStore(builder =>
			//	builder.UseSqlServer(connectionString, options =>
			//		options.MigrationsAssembly(migrationsAssembly)));

		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole();

			//if (env.IsDevelopment())
			//{
			//}
			app.UseDeveloperExceptionPage();
			//InitializeDatabase(app);

			//// cookie middleware for temporarily storing the outcome of the external authentication
			app.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				AuthenticationScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme,
				AutomaticAuthenticate = false,
				AutomaticChallenge = false
			});

			//// middleware for google authentication
			app.UseGoogleAuthentication(new GoogleOptions
			{
				DisplayName = "Google",
				AuthenticationScheme = "Google",
				SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme,
				ClientId = "434483408261-55tc8n0cs4ff1fe21ea8df2o443v2iuc.apps.googleusercontent.com", //todo
				ClientSecret = "3gcoTrEDPPJ0ukn_aYYT6PWo" //todo
			});

			app.UseIdentityServer();

			app.UseStaticFiles();
			app.UseMvcWithDefaultRoute();
		}

		private void InitializeDatabase(IApplicationBuilder app)
		{
			using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
			{
				serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

				var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
				context.Database.Migrate();

				if (!context.Clients.Any())
				{
					foreach (var client in IdentityServer4.Configuration.Config.GetClients())
					{
						context.Clients.Add(client.ToEntity());
					}
					context.SaveChanges();
				}

				//if (!context.IdentityResources.Any())
				//{
				//	foreach (var resource in Config.GetIdentityResources())
				//	{
				//		context.IdentityResources.Add(resource.ToEntity());
				//	}
				//	context.SaveChanges();
				//}

				//if (!context.ApiResources.Any())
				//{
				//	foreach (var resource in Config.GetApiResources())
				//	{
				//		context.ApiResources.Add(resource.ToEntity());
				//	}
				//	context.SaveChanges();
				//}
			}
		}
	}
}