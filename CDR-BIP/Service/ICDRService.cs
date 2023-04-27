using CDR_BIP.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CDR_BIP.Service
{
    public interface ICDRService
    {
        //ToDo: void AddCDR(CDR cdr);

        void AddCDRRange(IEnumerable<CDR> cdrs);
        Task<IEnumerable<CDR>> GetAll();
        Task<IEnumerable<CDR>> GetAllWithinDateRange(DateTime startDate, DateTime endDate);
        Task<CDR> GetByReference(string reference);
        decimal GetAverageCallCost();
        Task<IEnumerable<CDR>> GetCDRsByCallerId(string callerId);
        Task<IEnumerable<CDR>> GetCDRsByRecipient(string recipient);
        Task<IEnumerable<CDR>> GetAllByDate(DateTime startDate);
    }
}
