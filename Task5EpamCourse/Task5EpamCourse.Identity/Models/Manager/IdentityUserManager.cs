using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Task5EpamCourse.Identity.DbContext;
using Task5EpamCourse.Identity.Models.Account;

namespace Task5EpamCourse.Identity.Models.Manager
{
    public class IdentityUserManager : UserManager<AccountUser>
    {
        public IdentityUserManager(IUserStore<AccountUser> store)
            : base(store)
        {
        }
        public static IdentityUserManager Create(IdentityFactoryOptions<IdentityUserManager> options,
            IOwinContext context)
        {
            AccountContext db = context.Get<AccountContext>();
            IdentityUserManager manager = new IdentityUserManager(new UserStore<AccountUser>(db));
            return manager;
        }
    }
}