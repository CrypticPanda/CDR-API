using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace CDR_BIP.Service
{
    public interface ICSVService
    {
        public IEnumerable<T> ReadCSV<T>(IFormFile file);
    }
}
