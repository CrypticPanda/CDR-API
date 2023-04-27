using CDR_BIP.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CDR_BIP.Repo
{
    public class CDRRepository : ICDRRepository
    {
        private readonly CDRContext _context;

        public CDRRepository(CDRContext context)
        {
            _context = context;
        }

        public void AddRange(IEnumerable<CDR> cdr)
        {
            _context.CDRs.AddRange(cdr);
            _context.SaveChanges();
        }

        public void Add(CDR cdr)
        {
            _context.CDRs.Add(cdr);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<CDR>> GetAll()
        {
            return await _context.Set<CDR>().ToArrayAsync();
        }

        public async Task<IEnumerable<CDR>> GetAllWithinDateRange(DateTime startDate, DateTime endDate)
        {
            return await _context.Set<CDR>().Where(x => x.call_date >= startDate && x.call_date <= endDate).ToArrayAsync();
        }

        public async Task<CDR> GetByReference(string reference)
        {
            return await _context.Set<CDR>().Where(x => x.reference == reference).FirstOrDefaultAsync();
        }

        public decimal GetAverageCallCost()
        {
            return _context.Set<CDR>().Average(x => x.cost);
        }

        public async Task<IEnumerable<CDR>> GetCDRsByCallerId(string callerId)
        {
            return await _context.Set<CDR>().Where(x => x.CallerId == callerId).ToArrayAsync();
        }

        public async Task<IEnumerable<CDR>> GetCDRsByRecipient(string recipient)
        {
            return await _context.Set<CDR>().Where(x => x.recipient == recipient).ToArrayAsync();
        }

        public async Task<IEnumerable<CDR>> GetAllByDate(DateTime startDate)
        {
            return await _context.Set<CDR>().Where(x => x.call_date == startDate).ToArrayAsync();
        }
    }
}
