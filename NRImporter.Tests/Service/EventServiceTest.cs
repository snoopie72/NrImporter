using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using Northernrunners.ImportLibrary.Excel;
using Northernrunners.ImportLibrary.Poco;
using Northernrunners.ImportLibrary.Service;
using Northernrunners.ImportLibrary.Service.Datalayer;
using NUnit.Framework;

namespace NRImporter.Tests.Service
{
    [TestFixture]
    internal class EventServiceTest
    {
        private EventService _service;
        private UserService _userService;
        private Assembly _assembly;
        [SetUp]
        public void Setup()
        {
            _service = new EventService(new ResultDataService(new SqlDirectService(ConfigurationManager.ConnectionStrings["db"].ConnectionString)));
            _assembly = Assembly.GetExecutingAssembly();
            _userService =
                new UserService(
                    new ResultDataService(
                        new SqlDirectService(ConfigurationManager.ConnectionStrings["db"].ConnectionString)));
        }

        [Test]
        public void TestGetEventById()
        {
            var id = 1;
            var result = _service.GetEvent(id);
            Assert.IsNotNull(result);
        }

        [Test]
        public void AddResults()
        {
            const string resource = "NRImporter.Tests.Resources.Folkeparken1404.csv";
            const string filter = "Northern Runners";

            var handler = new EventResultHandler(_userService, _service);
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("no-NO");
            using (var stream = _assembly.GetManifestResourceStream(resource))
            {
                var deltakere = ExcelLoader.LoadRaceResult(stream, filter);
                handler.InsertResultInEvent(deltakere, new Event { Id = 4 });
                
            }
            

        }

        [Test]
        public void TestLoadIssue()
        {
            var resource = "NRImporter.Tests.Resources.Folkeparken1404.csv";
            var filter = "Northern Runners";

            using (var stream = _assembly.GetManifestResourceStream(resource))
            {
                var deltakere = ExcelLoader.LoadRaceResult(stream, filter);
                foreach (var deltaker in deltakere)
                {
                    Console.WriteLine(deltaker.Name);
                }
            }


        }

        [Test]
        public void TestTimespan()
        {
            string[] values = { "6", "6:12", "6:12:14", "6:12:14:45",
                          "6.12:14:45", "6:12:14:45.3448",
                          "6:12:14:45,3448", "6:34:14:45" };
            string[] cultureNames = { "no-NO" };

            // Change the current culture.
            foreach (string cultureName in cultureNames)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureName);
                Console.WriteLine("Current Culture: {0}",
                                  Thread.CurrentThread.CurrentCulture.Name);
                foreach (string value in values)
                {
                    try
                    {
                        TimeSpan ts = TimeSpan.Parse(value);
                        Console.WriteLine("{0} --> {1}", value, ts.ToString("c"));
                        Console.WriteLine("Minutes: " + ts.Minutes);
                        Console.WriteLine("Seconds: " + ts.Seconds);
                        Console.WriteLine("Hours: " + ts.Hours);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("{0}: Bad Format", value);
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine("{0}: Overflow", value);
                    }
                }
                Console.WriteLine();
            }
        }

       
    }
}
