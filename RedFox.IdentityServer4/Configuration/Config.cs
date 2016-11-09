using IdentityServer4.Models;
using System.Collections.Generic;

namespace RedFox.IdentityServer4.Configuration
{
	public class Config
	{
		public static IEnumerable<Scope> GetScopes()
		{
			return new List<Scope>
			{
				new Scope
				{
					Name = "temperatureApi",
					Description = "Temperature Api"
				}
			};
		}

		public static IEnumerable<Client> GetClients()
		{
			return new List<Client>
			{
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
				}
			};
		}
	}
}
