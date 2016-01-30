using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Northernrunners.ImportLibrary.Service;
using Northernrunners.ImportLibrary.Service.Helper;
using NUnit.Framework;

namespace NRImporter.Tests.Service
{
    [TestFixture]
    class SqlDirectServiceTest
    {
        private SqlDirectService _service;

        [SetUp]
        public void Setup()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["nr"].ConnectionString;
            _service = new SqlDirectService(connectionString);
        }

        [Test]
        public void TestSimpleQuery()
        {
            var query = "select * from kai_terms";
            //var result = _service.RunCommand(query);
            //var x = new testEntities();
            var q = new Query {Sql = query};
            Console.WriteLine(_service.RunCommand(q));
        }
    }
}
