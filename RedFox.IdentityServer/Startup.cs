using Microsoft.Owin;
using Owin;
using Configuration;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Host.Config;

[assembly: OwinStartup(typeof(WebHost.Startup))]

namespace WebHost
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			var factory = new IdentityServerServiceFactory()
						.UseInMemoryUsers(Users.Get())
						.UseInMemoryClients(Clients.Get())
						.UseInMemoryScopes(Scopes.Get());

			var options = new IdentityServerOptions
			{
				SigningCertificate = Certificate.Load(),
				Factory = factory,
			};

			app.Map("/core", idsrvApp =>
			{
				idsrvApp.UseIdentityServer(options);
			});
		}
	}
}