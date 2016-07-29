using IdentityServer3.AccessTokenValidation;
using Microsoft.Owin;
using Owin;
using System.IdentityModel.Tokens;
using Swashbuckle.Application;
using System.Web.Http;

[assembly: OwinStartup(typeof(RedFox.TemperatureApp.Startup))]

namespace RedFox.TemperatureApp
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			HttpConfiguration config = new HttpConfiguration();
			JwtSecurityTokenHandler.InboundClaimTypeMap.Clear();

			app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
			{
				Authority = "https://localhost:44333/core",
				RequiredScopes = new[] { "write" },

				// client credentials for the introspection endpoint
				ClientId = "temperatureApp",
				ClientSecret = "secret"
			});

			config.MapHttpAttributeRoutes();

			config
				.EnableSwagger(c => c.SingleApiVersion("v1", "A title for your API"))
				.EnableSwaggerUi();

			app.UseWebApi(WebApiConfig.Register(config));
		}
	}
}
