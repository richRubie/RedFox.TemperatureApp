﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace RedFox.IdentityServer4
{
	public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

			services.AddMvc();

			services.AddIdentityServer()
			.AddTemporarySigningCredential()
			.AddInMemoryScopes(Configuration.Config.GetScopes())
			.AddInMemoryClients(Configuration.Config.GetClients())
			.AddInMemoryUsers(Configuration.Config.GetUsers());
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
            }
                app.UseDeveloperExceptionPage();

			app.UseIdentityServer();

			//// cookie middleware for temporarily storing the outcome of the external authentication
			//app.UseCookieAuthentication(new CookieAuthenticationOptions
			//{
			//	AuthenticationScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme,
			//	AutomaticAuthenticate = false,
			//	AutomaticChallenge = false
			//});

			//// middleware for google authentication
			//app.UseGoogleAuthentication(new GoogleOptions
			//{
			//	AuthenticationScheme = "Google",
			//	SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme,
			//	ClientId = "434483408261-55tc8n0cs4ff1fe21ea8df2o443v2iuc.apps.googleusercontent.com",
			//	ClientSecret = "3gcoTrEDPPJ0ukn_aYYT6PWo"
			//});

			app.UseStaticFiles();
			app.UseMvcWithDefaultRoute();

			//app.Run(async (context) =>
   //         {
   //             await context.Response.WriteAsync("Hello World!");
   //         });
        }
    }
}