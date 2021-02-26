using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Task5EpamCourse.Identity.DbContext;
using Task5EpamCourse.Identity.Models.Account;
using Task5EpamCourse.Identity.Models.Manager;

[assembly: OwinStartupAttribute(typeof(Task5EpamCourse.App_Start.Startup))]
namespace Task5EpamCourse.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<AccountContext>(AccountContext.Create);
            app.CreatePerOwinContext<IdentityUserManager>(IdentityUserManager.Create);

            // регистрация менеджера ролей
            //app.CreatePerOwinContext<IdentityRoleManager>(IdentityRoleManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }
    }
}