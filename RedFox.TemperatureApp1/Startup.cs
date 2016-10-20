using Microsoft.Owin;
using Owin;
using System.IdentityModel.Tokens;
using Swashbuckle.Application;
using System.Web.Http;
using Microsoft.AspNetCore.Builder;

[assembly: OwinStartup(typeof(RedFox.TemperatureApp.Startup))]

namespace RedFox.TemperatureApp
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			HttpConfiguration config = new HttpConfiguration();

			app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
			{
				Authority = "http://localhost:5000",
				ScopeName = "api1",

				RequireHttpsMetadata = false
			});

			config.MapHttpAttributeRoutes();

			config
				.EnableSwagger(c => c.SingleApiVersion("v1", "A title for your API"))
				.EnableSwaggerUi();

			app.UseWebApi(WebApiConfig.Register(config));
		}
	}
}
