using CDR_BIP.Controllers;
using CDR_BIP.Repo;
using CDR_BIP.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;

namespace CDR_Tests
{
    [TestClass]
    public class CDRTests : TestBase
    {
        private ICSVService _csvService;
        private ICDRService _cdrService;
        private CDRController _controller;
        private IFormFile _file;

        [TestInitialize]
        public void Setup()
        {
            var repo = new CDRRepository(context);
            _cdrService = new CDRService(repo);
            _csvService = new Mock<ICSVService>().Object;
            _controller = new CDRController(_csvService, _cdrService);
            _file = GetFormFile();
        }

        [TestMethod]
        public void UploadCDR_Valid_Test()
        {
            //Arrange

            //Act
            var result = _controller.UploadCDR(_file);

            //Assert
            Assert.IsInstanceOfType<OkResult>(result);
        }

        [TestMethod]
        public void UploadCDR_InValid_Null_Test()
        {
            //Arrange

            //Act
            var result = _controller.UploadCDR(null);

            //Assert
            Assert.IsInstanceOfType<BadRequestResult>(result);
        }

        //Too many records, will exceed buffer
        //[TestMethod]
        //public void GetAll_Test()
        //{
        //    //Arrange

        //    //Act
        //    var results = _controller.GetCDRs().Result;

        //    //Assert
        //    Assert.IsTrue(results.Any());
        //}

        [TestMethod]
        public void GetAllWithinDateRange_Test()
        {
            //Arrange    
            var startDate = new DateTime(2016, 08, 10);
            var endDate = new DateTime(2016, 08, 16);

            //Act
            var results = _controller.GetCDRsWithinDateRange(startDate, endDate).Result;

            //Assert
            Assert.AreEqual(972, results.Count());
        }

        [TestMethod]
        public void GetLongestCallWithinDateRange_Test()
        {
            //Arrange    
            var startDate = new DateTime(2016, 08, 10);
            var endDate = new DateTime(2016, 08, 16);

            //Act
            var results = _controller.GetLongestCallWithinDateRange(startDate, endDate);

            //Assert
            Assert.AreEqual(5813, results.duration);
        }

        [TestMethod]
        public void GetByReference_Test()
        {
            //Arrange
            var reference = "C3C89CE665EAC7C0B2F7C61D3573DB85D";

            //Act
            var result = _controller.GetCDRByReference(reference).Result;

            //Assert
            Assert.AreEqual(reference, result.reference);
        }

        [TestMethod]
        public void GetAverageCallCost_Test()
        {
            //Arrange

            //Act
            var result = _controller.GetAverageCallCost();

            //Assert
            Assert.AreEqual(0.066m, result);
        }

        [TestMethod]
        public void GetByCallerId_Test()
        {
            //Arrange
            var callerId = "441216000000";

            //Act
            var result = _controller.GetCDRsByCallerId(callerId).Result;

            //Assert
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void GetByRecipient_Test()
        {
            //Arrange
            var recipient = "441734000000";

            //Act
            var result = _controller.GetCDRsByRecipient(recipient).Result;

            //Assert
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void GetTotalCostByDate_Test()
        {
            //Arrange
            var startDate = new DateTime(2016, 08, 10);

            //Act
            var result = _controller.GetTotalCostByDate(startDate);

            //Assert
            Assert.AreEqual(34.022m, result);
        }
    }
}
