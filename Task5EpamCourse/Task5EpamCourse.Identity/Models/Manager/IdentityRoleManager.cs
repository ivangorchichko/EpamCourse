using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Task5EpamCourse.Identity.DbContext;
using Task5EpamCourse.Identity.Models.Roles;

namespace Task5EpamCourse.Identity.Models.Manager
{
    public class IdentityRoleManager : RoleManager<AccountRole>
    {
        public IdentityRoleManager(RoleStore<AccountRole> store)
            : base(store)
        { }
        public static IdentityRoleManager Create(IdentityFactoryOptions<IdentityRoleManager> options,
            IOwinContext context)
        {
            return new IdentityRoleManager(new
                RoleStore<AccountRole>(context.Get<AccountContext>()));
        }
    }
}