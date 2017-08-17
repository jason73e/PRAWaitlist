using System;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace PRAWaitList
{
    public static class MyAuthentication
    {
        public const String ApplicationCookie = "PRAWaitListAuthenticationType";
    }

    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            // need to add UserManager into owin, because this is used in cookie invalidation
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = MyAuthentication.ApplicationCookie,
                LoginPath = new PathString("/Login"),
                Provider = new CookieAuthenticationProvider(),
                CookieName = "MyPRAWaitListAuth",
                CookieHttpOnly = true,
                ExpireTimeSpan = TimeSpan.FromHours(12), // adjust to your needs
            });
        }
    }
}