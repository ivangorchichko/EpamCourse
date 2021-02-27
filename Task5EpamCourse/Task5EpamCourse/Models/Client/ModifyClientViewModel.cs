using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task5EpamCourse.Models.Client
{
    public class ModifyClientViewModel
    {
        public int Id { get; set; }

        public string ClientName { get; set; }

        public string ClientTelephone { get; set; }
    }
}