using CDR_BIP.Model;
using CDR_BIP.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CDR_BIP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CDRController : ControllerBase
    {
        private readonly ICSVService _csvService;
        private readonly ICDRService _cdrService;

        public CDRController(ICSVService csvService, ICDRService cdrService)
        {
            _csvService = csvService;
            _cdrService = cdrService;
        }

        [HttpPost("api/[controller]/upload")]
        [ProducesResponseType(200)]
        public ActionResult UploadCDR(IFormFile file)
        {
            if (file == null) return BadRequest();

            var cdrData = _csvService.ReadCSV<CDR>(file);
            _cdrService.AddCDRRange(cdrData);

            return Ok();
        }

        // Too many records to use GetAll, will exceed buffer limit
        //[HttpGet("api/[controller]/getcdrs")]
        //public async IEnumerable<CDR> GetCDRs()
        //{
        //    return await _cdrService.GetAll();
        //}

        [HttpGet("api/[controller]/getbyreference")]
        public async Task<CDR> GetCDRByReference(string reference)
        {
            return await _cdrService.GetByReference(reference);
        }

        [HttpGet("api/[controller]/getbycallerid")]
        public async Task<IEnumerable<CDR>> GetCDRsByCallerId(string callerId)
        {
            return await _cdrService.GetCDRsByCallerId(callerId);
        }

        [HttpGet("api/[controller]/getbyrecipient")]
        public async Task<IEnumerable<CDR>> GetCDRsByRecipient(string recipient)
        {
            return await _cdrService.GetCDRsByRecipient(recipient);
        }

        [HttpGet("api/[controller]/getaveragecallcost")]
        public decimal GetAverageCallCost()
        {
            var result = _cdrService.GetAverageCallCost();
            return decimal.Round(result, 3);
        }

        [HttpGet("api/[controller]/getwithindaterange")]
        public async Task<IEnumerable<CDR>> GetCDRsWithinDateRange(DateTime startDate, DateTime endDate)
        {
            return await _cdrService.GetAllWithinDateRange(startDate, endDate);
        }

        [HttpGet("api/[controller]/getlongestcallwithindaterange")]
        public CDR GetLongestCallWithinDateRange(DateTime startDate, DateTime endDate)
        {
            var result = _cdrService.GetAllWithinDateRange(startDate, endDate).Result;
            return result.OrderByDescending(x => x.duration).FirstOrDefault();
        }

        [HttpGet("api/[controller]/gettotalcostbydate")]
        public decimal GetTotalCostByDate(DateTime startDate)
        {
            var result = _cdrService.GetAllByDate(startDate).Result;
            return result.Sum(x => x.cost);
        }
    }
}
