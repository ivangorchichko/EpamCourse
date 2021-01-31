using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;
using Task4.DomainModel.DataModel;

namespace Task4.BL.CSVService.Model
{
    public class PurchaseDTO
    {
        public DateTime Date { get; set; }

        public string Client { get; set; }

        public string Product { get; set; }

        public double Price { get; set; }
    }
}
