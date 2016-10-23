using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace RedFox.TemperatureApp
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
        public void ConfigureServices(IServiceCollection services)
        {
			services.AddOptions();
			services.Configure<AppOptions>(Configuration.GetSection("AppOptions"));
            // Add framework services.
            services.AddMvc();
			services.AddScoped(_ => new Business.Context(Configuration.GetConnectionString("Connection")));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddNLog();
            loggerFactory.AddDebug();

			env.ConfigureNLog("nlog.config");

			string authorityUrl = Configuration.GetSection("AppOptions")["AuthorityUrl"];

			app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
			{
				Authority = authorityUrl,
				ScopeName = "api1",
				
				RequireHttpsMetadata = false
			});

			app.UseMvc();
		}
	}
}
