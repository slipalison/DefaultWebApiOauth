using DefaultWebApiOauth.Provider;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Web.Http;

[assembly: OwinStartup(typeof(DefaultWebApiOauth.Startup))]

namespace DefaultWebApiOauth
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            ConfigurationWebApi(config);
            ConfigureOAuth(app);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        public static void ConfigurationWebApi(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.Filters.Add(new AuthorizeAttribute());
              config.Routes.MapHttpRoute(
                  name: "DefaultApi",
                  routeTemplate: "api/{version}/{controller}/{id}",
                  defaults: new { id = RouteParameter.Optional , version  = RouteParameter.Optional}
                  );
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            var OAutServerOption = new OAuthAuthorizationServerOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                AllowInsecureHttp = true, 
                TokenEndpointPath = new PathString("/api/security/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(1), 
                Provider = new AuthAuthorizationServerProvider()
            };
            app.UseOAuthAuthorizationServer(OAutServerOption);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions {
                AuthenticationType = "Bearer", 
                AuthenticationMode = AuthenticationMode.Active
            });

        }

    }
}