using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
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
        private EventService _eventService;
        private UserService _userService;
        private Assembly _assembly;
        private FilterService _filterService;
        private EventResultHandler _handler;
        [SetUp]
        public void Setup()
        {
            _eventService = new EventService(new DatalayerService(new SqlDirectService(ConfigurationManager.ConnectionStrings["db"].ConnectionString)));
            _assembly = Assembly.GetExecutingAssembly();
            _userService =
                new UserService(
                    new DatalayerService(
                        new SqlDirectService(ConfigurationManager.ConnectionStrings["db"].ConnectionString)));
            _filterService = new FilterService(new DatalayerService(
                        new SqlDirectService(ConfigurationManager.ConnectionStrings["db"].ConnectionString)));
            _handler = new EventResultHandler(_userService, _eventService, _filterService);

        }

        [Test]
        public void TestGetEventById()
        {
            var id = 1;
            var result = _eventService.GetEvent(id);
            Assert.IsNotNull(result);
        }

        //
        [Test]
        public void AddResults()
        {
            const string resource = "NRImporter.Tests.Resources.Folkeparken1404.csv";
            const string filter = "Northern Runners";

            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("no-NO");
            using (var stream = _assembly.GetManifestResourceStream(resource))
            {
                var deltakere = new ExcelLoader().LoadRaceResult(stream);                
                _handler.InsertResultInEvent(deltakere, new Event { Id = 4 });
                
            }
            

        }

        

        [Test]
        public void TestUpdateInvalidResults()
        {
            _handler.UpdateTempResults();
        }

        [TestCase()]
        [Test]
        public void TestLoadMembers()
        {
            
            const string resource = "NRImporter.Tests.Resources.medlemsregister.csv";

            using (var stream = _assembly.GetManifestResourceStream(resource))
            {
                var users = new ExcelLoader().LoadMemberFile(stream);
                foreach (var user in users)
                {
                    Console.WriteLine(user.Name + " " + user.Gender + " " + user.DateOfBirth);
                }
                _handler.CreateOrIgnoreUsers(users);                
            }
        }

        [Test]
        public void TestGetInvalidUsers()
        {
            var users = _userService.GetAllUsersWithInvalidDate();
            foreach (var x in users)
            {
                Console.WriteLine(x.Name);
            }
        }

        [Test]
        public void TestSaveFilters()
        {
            var filters = new List<Filter>
            {
                new Filter
                {
                    Type = FilterType.Contains,
                    Value = "Northern Runners"
                },
                new Filter
                {
                    Type = FilterType.Equals,
                    Value = "NR"
                }
            };
            _filterService.SaveFilters(filters);
        }

        [Test]
        public void TestLoadFilters()
        {
            var filters = _filterService.GetFilters();
            Assert.IsNotNull(filters);
        }

        //[Test]
        public void TestTimespan()
        {
            string[] values = { "6", "6:12", "6:12:14", "6:12:14:45",
                          "6.12:14:45", "6:12:14:45.3448",
                          "6:12:14:45,3448", "6:34:14:45" };
            string[] cultureNames = { "no-NO" };

            // Change the current culture.
            foreach (var cultureName in cultureNames)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureName);
                Console.WriteLine("Current Culture: {0}",
                                  Thread.CurrentThread.CurrentCulture.Name);
                foreach (var value in values)
                {
                    try
                    {
                        var ts = TimeSpan.Parse(value);
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
