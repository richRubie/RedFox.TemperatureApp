using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Swashbuckle.Application;

[assembly: OwinStartup(typeof(WebApplication1.Startup))]

namespace WebApplication1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
			HttpConfiguration config = new HttpConfiguration();

			ConfigureAuth(app);

			config
				.EnableSwagger(c => c.SingleApiVersion("v1", "A title for your API"))
				.EnableSwaggerUi();

		}
	}
}
