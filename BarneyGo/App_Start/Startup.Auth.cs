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
                ClientId = "replace with our Google ClientID",
                ClientSecret = "replace with our Google ClientSecret"
            });
        }

    }
}
