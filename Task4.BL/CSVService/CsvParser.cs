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
        public IEnumerable<PurchaseDto> ParseCsvFile(string filePath)
        {
            ICollection<PurchaseDto> data = new List<PurchaseDto>();
            using (var reader = new StreamReader(filePath))
            {
                using (var csv = new CsvReader(reader, CultureInfo.CurrentCulture))
                {
                    foreach (var item in csv.GetRecords<PurchaseDto>())
                    {
                        data.Add(item); 
                    }
                }
            }
            return data;
        }
    }
}
