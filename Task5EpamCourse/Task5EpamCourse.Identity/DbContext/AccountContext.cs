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
        public AccountContext() : base("SaleTask5") { }

        public static AccountContext Create()
        {
            return new AccountContext();
        }
        //<connectionStrings>
        //<add name = "IdentityDb" providerName="System.Data.SqlClient" connectionString="Data Source=(local);Initial Catalog=IdentityDb;Integrated Security=True" />
        //</connectionStrings>
    }
}