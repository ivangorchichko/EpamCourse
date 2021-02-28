using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Task5.BL.DIConfig;
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
           RegistrContainer();
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

        private void RegistrContainer()
        {
            var bulder = AutofacConfiguration.ConfigureContainer();
            bulder.RegisterControllers(Assembly.GetExecutingAssembly());
            DependencyResolver.SetResolver(new AutofacDependencyResolver(bulder.Build()));
        }
    }
}