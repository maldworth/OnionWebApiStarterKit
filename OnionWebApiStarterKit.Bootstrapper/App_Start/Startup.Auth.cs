using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using Owin;

namespace OnionWebApiStarterKit.Bootstrapper
{
    public partial class Startup {
        public static IDataProtectionProvider DataProtectionProvider { get; private set; }
        public void ConfigureAuth(IAppBuilder app)
        {
            // Load IdentityServer Stuff
        }
    }
}