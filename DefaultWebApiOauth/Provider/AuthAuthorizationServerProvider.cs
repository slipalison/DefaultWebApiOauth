using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading;

namespace DefaultWebApiOauth.Provider
{
    public class AuthAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            //return base.ValidateClientAuthentication(context);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            try
            {
                var user = context.UserName;
                var pass = context.Password;

                if (user != "alison" || pass != "alison")
                {
                    context.SetError("invalid_grant", "Usuário ou senha invalida");
                    return;
                }

                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, user));

                var roles = new List<string> { "User" };

                foreach (var role in roles)
                    identity.AddClaim(new Claim(ClaimTypes.Role, role));

                var principal = new GenericPrincipal(identity, roles.ToArray());
                Thread.CurrentPrincipal = principal;

                context.Validated(identity);
                
            }
            catch 
            {

                context.SetError("invalid_grant", "Falha ao autenticar");
            }
            // return base.GrantResourceOwnerCredentials(context);
        }
    }
}