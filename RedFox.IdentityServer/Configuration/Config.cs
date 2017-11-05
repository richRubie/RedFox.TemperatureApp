using IdentityServer4.Models;
using System.Collections.Generic;
using static IdentityServer4.IdentityServerConstants;

namespace RedFox.IdentityServer.Configuration
{
	public class Config
	{
		public static IEnumerable<Client> GetClients()
		{
			return new List<Client>
			{
				new Client
				{
					ClientId = "client",

					// no interactive user, use the clientid/secret for authentication
					AllowedGrantTypes = GrantTypes.ClientCredentials,

					// secret for authentication
					ClientSecrets =
					{
						new Secret("secret".Sha256())
					},

					// scopes that client has access to
					AllowedScopes = { "api1" }
				},
				new Client
				{
					ClientId = "raspberryPiClient",

					// no interactive user, use the clientid/secret for authentication
					AllowedGrantTypes = GrantTypes.ClientCredentials,

					// secret for authentication
					ClientSecrets =
					{
						new Secret("QChGXGOnjPBg7Xlf8S6m8tBavisAk2OG".Sha256())
					},

					// scopes that client has access to
					AllowedScopes = { "temperatureApi" }
				},
				new Client
				{
					ClientId = "sensorweb",
					ClientName = "Sensor web Client",
					AllowedGrantTypes = GrantTypes.Implicit,
					AllowAccessTokensViaBrowser = true,
					// where to redirect to after login
					RedirectUris = { "http://localhost:4200" },
					AllowedCorsOrigins = new List<string>
					{
						"http://localhost:4200"
					},

					// where to redirect to after logout
					PostLogoutRedirectUris = { "http://localhost:4200" },

					AllowedScopes = new List<string>
					{
						StandardScopes.OpenId,
						StandardScopes.Profile,
						"temperatureApi"
					}
				},
			};
		}

		public static IEnumerable<ApiResource> GetApiResources()
		{
			return new List<ApiResource>
			{
				new ApiResource("api1", "My API"),
				new ApiResource("temperatureApi", "Temperature Api")
			};
		}
	}
}
