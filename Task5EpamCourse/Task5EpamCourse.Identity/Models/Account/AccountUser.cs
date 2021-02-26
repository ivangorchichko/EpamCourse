using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Task5EpamCourse.Identity.Models.Account
{
    public class AccountUser : IdentityUser
    {
        public string NickName { get; set; }
        public AccountUser()
        {
        }
    }
}