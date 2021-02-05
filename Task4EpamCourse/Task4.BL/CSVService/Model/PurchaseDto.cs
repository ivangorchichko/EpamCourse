using System;

namespace Task4.BL.CSVService.Model
{
    public class PurchaseDto
    {
        public DateTime Date { get; set; }

        public string Client { get; set; }

        public string Product { get; set; }

        public double Price { get; set; }
    }
}
