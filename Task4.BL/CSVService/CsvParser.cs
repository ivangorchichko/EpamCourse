using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using Task4.BL.Contracts;
using Task4.BL.CSVService.Model;

namespace Task4.BL.CSVService
{
    public class CsvParser : ICsvParser
    {
        public IEnumerable<PurchaseDTO> ParseCsvFile(string filePath)
        {
            ICollection<PurchaseDTO> data = new List<PurchaseDTO>();
            using (var reader = new StreamReader(filePath))
            {
                using (var csv = new CsvReader(reader, CultureInfo.CurrentCulture))
                {
                    foreach (var item in csv.GetRecords<PurchaseDTO>())
                    {
                        data.Add(item); 
                    }
                }
            }
            return data;
        }
    }
}
