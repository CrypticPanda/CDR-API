using CDR_BIP.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CDR_BIP.Repo
{
    public interface ICDRRepository
    {
        void AddRange(IEnumerable<CDR> cdr);
        void Add(CDR cdr);
        Task<IEnumerable<CDR>> GetAll();
        Task<IEnumerable<CDR>> GetAllWithinDateRange(DateTime startDate, DateTime endDate);
        Task<CDR> GetByReference(string reference);
        decimal GetAverageCallCost();
        Task<IEnumerable<CDR>> GetCDRsByCallerId(string callerId);
        Task<IEnumerable<CDR>> GetCDRsByRecipient(string recipient);
        Task<IEnumerable<CDR>> GetAllByDate(DateTime startDate);
    }
}
