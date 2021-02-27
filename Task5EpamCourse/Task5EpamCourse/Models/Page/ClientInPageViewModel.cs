using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Task5EpamCourse.Models.Client;

namespace Task5EpamCourse.Models.Page
{
    public class ClientInPageViewModel
    {
        public IEnumerable<IndexClientViewModel> Clients { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}