using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Northernrunners.ImportLibrary.Dto;
using Northernrunners.ImportLibrary.Service;
using Northernrunners.ImportLibrary.Service.Datalayer;
using NUnit.Framework;

namespace NRImporter.Tests.Service
{
    [TestFixture]
    class ResultDataServiceTest
    {
        private ResultDataService _dataService;

        [SetUp]
        public void Setup()
        {
            _dataService = new ResultDataService(new SqlDirectService(ConfigurationManager.ConnectionStrings["db"].ConnectionString));
        }

        [Test]
        public void AddEventResult()
        {
            var eventResult = GetEventResult();
            _dataService.AddEventResults(eventResult);
        }

        private static EventResultDto GetEventResult()
        {
            //var eventResult = new EventResult();

            var eventResultDto = new EventResultDto
            {
                AgeCategory = "A40",
                AgeGrade = 63.12,
                DateCreated = DateTime.Now,
                EventId = 2,
                Position = 23,
                Gender = 'F',
                Time = 2900000,
                UserId = 3
            };

            return eventResultDto;

        }
    }
}
