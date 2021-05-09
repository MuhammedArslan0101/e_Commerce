using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(e_Commerce.App_Start.Startup1))]

namespace e_Commerce.App_Start
{
    public class Startup1
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions {
            AuthenticationType ="ApplicationUser" , LoginPath = new PathString ("/Account/Login")
            
            });
        }
    }
}
