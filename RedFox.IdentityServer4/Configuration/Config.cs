using IdentityServer4.Models;
using IdentityServer4.Services.InMemory;
using System.Collections.Generic;
using System.Security.Claims;

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
				},

				StandardScopes.OpenId,
				StandardScopes.Profile,
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
						StandardScopes.OpenId.Name,
						StandardScopes.Profile.Name,
						"temperatureApi"
					}
				},
			};
		}

		public static List<InMemoryUser> GetUsers()
		{
			return new List<InMemoryUser>
			{
				new InMemoryUser
				{
					Subject = "1",
					Username = "alice",
					Password = "password",

					Claims = new List<Claim>
					{
						new Claim("name", "Alice"),
						new Claim("website", "https://alice.com")
					}
				},
				new InMemoryUser
				{
					Subject = "2",
					Username = "bob",
					Password = "password",

					Claims = new List<Claim>
					{
						new Claim("name", "Bob"),
						new Claim("website", "https://bob.com")
					}
				}
			};
		}
	}
}
