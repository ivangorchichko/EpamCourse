using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Task5EpamCourse.Identity.Models.Account;

namespace Task5EpamCourse.Identity.DbContext
{
    public class AccountContext : IdentityDbContext<AccountUser>
    {
        public AccountContext() : base("IdentityDb") { }

        public static AccountContext Create()
        {
            return new AccountContext();
        }
    }
}