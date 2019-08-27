using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using TitchyCottage.Security.Model;
using TitchyCottage.Security.Repository;

namespace TitchyCottage.Api.Providers
{
    public class AuthorizationServerProvider: OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            //context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            ApplicationUser user = new ApplicationUser();
            if (string.IsNullOrWhiteSpace(context.UserName) || string.IsNullOrWhiteSpace(context.Password))
            {
                context.SetError("invalid_grant", "The user name or password is Empty.");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);

            using (AuthRepository _repo = new AuthRepository())
            {
                user = await _repo.FindUser(context.UserName.Substring(0, context.UserName.IndexOf("@")), context.Password);

                if (user == null)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }
                else
                {
                    identity.AddClaim(new Claim("UserId", user.Id));
                    identity.AddClaim(new Claim("Name", user.UserName));
                    identity.AddClaim(new Claim("EmailAddress", user.Email));
                    identity.AddClaim(new Claim("FullName", string.Concat(user.FirstName, " ", user.LastName)));
                    identity.AddClaim(new Claim("Roles", Json.Encode(user.Roles)));


                }
            }

            var addRole = new List<string>();
            foreach(var item in user.Roles)
            {
                addRole.Add(item.RoleId);
            }

            var props = new AuthenticationProperties(new Dictionary<string, string>
                        {
                            {
                                "UserName", user.UserName
                            },
                            {
                                "EmailAddress", user.Email
                            },
                            {
                                "FullName", string.Concat(user.FirstName, " ", user.LastName)
                            },
                            {
                                "Roles",  Json.Encode(addRole)
                            }
                        });

            var ticket = new AuthenticationTicket(identity, props);
            context.Validated(ticket);

            //context.Validated(identity);

        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                //removed .issued and .expires parameter
                if (!property.Key.StartsWith("."))
                    context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }
    }
}