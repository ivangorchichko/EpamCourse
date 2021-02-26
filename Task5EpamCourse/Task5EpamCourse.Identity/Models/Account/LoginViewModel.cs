using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Task5EpamCourse.Identity.Models.Account
{
    public class LoginViewModel
    {

        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //[Required]
        //[Display(Name = "Адрес электронной почты")]
        //[EmailAddress]
        //public string Email { get; set; }

        //[Required]
        //[DataType(DataType.Password)]
        //[Display(Name = "Пароль")]
        //public string Password { get; set; }
    }
}