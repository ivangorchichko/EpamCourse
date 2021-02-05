using System.Collections.Generic;
using Task4.BL.CSVService.Model;

namespace Task4.BL.Contracts
{
    public interface ICsvParser
    {
        IEnumerable<PurchaseDto> ParseCsvFile(string filePath);
    }
}
