using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Task3EPAMCourse.Billing.Model;

namespace Task3EPAMCourse.Billing.FileService
{
    public class JsonRepository
    {
        private static readonly string _saveFilePath = @"Data\SavedBilling.json";

        public bool IsSequenceSavedOnce { get; set; }

        public void SaveFile(IList<CallInfo> callsInfoCollection)
        {
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto, Formatting = Formatting.Indented };
            var resultString = JsonConvert.SerializeObject(callsInfoCollection, settings);
            using var filename = new FileStream(_saveFilePath, FileMode.Open);
            using var writer = new StreamWriter(filename, Encoding.Default);
            writer.Write(resultString);
        }

        public IEnumerable<CallInfo> GetCurrentCallInfo()
        {
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto, Formatting = Formatting.Indented };
            var jsonString = File.ReadAllText(_saveFilePath);
            return JsonConvert.DeserializeObject<IEnumerable<CallInfo>>(jsonString, settings);
        }
    }
}
