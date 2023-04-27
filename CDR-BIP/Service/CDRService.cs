using CDR_BIP.Model;
using CDR_BIP.Repo;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CDR_BIP.Service
{
    public class CDRService : ICDRService
    {
        private readonly ICDRRepository _cdrRepository;

        public CDRService(ICDRRepository cdrRepository)
        {
            _cdrRepository = cdrRepository;
        }

        public void AddCDRRange(IEnumerable<CDR> cdrs)
        {
            _cdrRepository.AddRange(cdrs);
        }

        public async Task<IEnumerable<CDR>> GetAll()
        {
            return await _cdrRepository.GetAll();
        }

        public async Task<IEnumerable<CDR>> GetAllWithinDateRange(DateTime startDate, DateTime endDate)
        {
            return await _cdrRepository.GetAllWithinDateRange(startDate, endDate);
        }

        public async Task<CDR> GetByReference(string reference)
        {
            return await _cdrRepository.GetByReference(reference);
        }
        public decimal GetAverageCallCost()
        {
            return _cdrRepository.GetAverageCallCost();
        }

        public async Task<IEnumerable<CDR>> GetCDRsByCallerId(string callerId)
        {
            return await _cdrRepository.GetCDRsByCallerId(callerId);
        }

        public async Task<IEnumerable<CDR>> GetCDRsByRecipient(string recipient)
        {
            return await _cdrRepository.GetCDRsByRecipient(recipient);
        }

        public async Task<IEnumerable<CDR>> GetAllByDate(DateTime startDate)
        {
            return await _cdrRepository.GetAllByDate(startDate);
        }
    }
}
