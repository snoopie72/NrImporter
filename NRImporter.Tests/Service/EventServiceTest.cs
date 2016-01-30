using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Northernrunners.ImportLibrary.Service;
using NUnit.Framework;

namespace NRImporter.Tests
{
    [TestFixture]
    class EventServiceTest
    {
        private EventService _service;

        [SetUp]
        public void Setup()
        {
            _service = new EventService(new SqlDirectService(ConfigurationManager.ConnectionStrings["db"].ConnectionString));
        }

        [Test]
        public void TestGetEventById()
        {
            var id = 1;
            var result = _service.GetEvent(id);
            Assert.IsNotNull(result);
        }
    }
}
