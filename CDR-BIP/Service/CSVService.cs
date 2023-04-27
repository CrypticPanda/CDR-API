using CsvHelper;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace CDR_BIP.Service
{
    public class CSVService : ICSVService
    {
        public IEnumerable<T> ReadCSV<T>(IFormFile file)
        {
            var SourceStream = file.OpenReadStream();

            var reader = new StreamReader(SourceStream);

            var csv = new CsvReader(reader, CultureInfo.GetCultureInfo("en-GB"));

            var records = csv.GetRecords<T>();
            return records;
        }
    }
}