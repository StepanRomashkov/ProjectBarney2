using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Google;


namespace BarneyGo
{
    public partial class Startup
    {
        private void ConfigureAuth(IAppBuilder app)
        {
            var cookieOptions = new CookieAuthenticationOptions
            {
                LoginPath = new PathString("/Account/Login")
            };

            app.UseCookieAuthentication(cookieOptions);

            app.SetDefaultSignInAsAuthenticationType(cookieOptions.AuthenticationType);

            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions
            {
                ClientId = "1093499569682-f5s3s4anvom2qmetf87hvbkit8ot86a8.apps.googleusercontent.com",
                ClientSecret = "Kg_Xjyy-WlJEt36Jo-3ZB1K4"
            });
        }

    }
}
