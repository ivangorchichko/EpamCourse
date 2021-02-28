using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task5EpamCourse.Models.Manager
{
    public class ManagerViewModel
    {
        public int Id { get; set; }

        public string ManagerName { get; set; }

        public string ManagerTelephone { get; set; }

        public DateTime Date { get; set; }

        public string ManagerRank { get; set; }
    }
}