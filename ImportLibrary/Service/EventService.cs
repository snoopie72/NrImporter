using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Northernrunners.ImportLibrary.Dto;
using Northernrunners.ImportLibrary.Poco;
using Northernrunners.ImportLibrary.Service.Datalayer;
using Northernrunners.ImportLibrary.Service.Helper;
using Northernrunners.ImportLibrary.Utils;

namespace Northernrunners.ImportLibrary.Service
{
    public class EventService:IEventService
    {
        private readonly ISqlDirectService _sqlDirectService;
        private readonly Assembly _assembly;
        private readonly IResultDataService _resultDataService;
        public EventService(ISqlDirectService sqlDirectService, IResultDataService resultDataService)
        {
            _sqlDirectService = sqlDirectService;
            _resultDataService = resultDataService;
            _assembly = Assembly.GetAssembly(typeof(EventService));
        }
    

        public ICollection<Event> GetEvents(DateTime @from, DateTime to)
        {
            return GetAllEvents().Where(t => t.Date > from && t.Date < to).ToList();
        }

        public ICollection<Event> GetEvents(string name, int year)
        {
            return GetAllEvents().Where(t => t.Name.Contains(name) && t.Date.Year.Equals(year)).ToList();            
        }

        public ICollection<Event> GetEvents(string name)
        {
            return GetAllEvents().Where(t => t.Name.Contains(name)).ToList();            
        }

        public Event GetEvent(int eventId)
        {
            var events = GetAllEvents();
            return events.FirstOrDefault(t => t.Id.Equals(eventId));
        }

        public void AddEventResults(EventResult eventResult)
        {
            foreach (var result in eventResult.Results)
            {
                //todo: Noe feil her.. bruker skal ikke kunne være tom.
                if (result.User != null && result.User.Id > 0)
                {
                    var gender = 'M';
                    var time = CalculateTime(result.Time);
                    var eventResultDto = new EventResultDto
                    {
                        EventId = eventResult.Event.Id,
                        AgeCategory = GetAgeCategory(result.User),
                        AgeGrade = GetAgeGrade(result.User),
                        DateCreated = DateTime.Now,
                        Gender = gender,
                        UserId = result.User.Id,
                        //TODO: Fikse time.. ligger i resultat
                        Time = time,
                        Position = result.Position
                    };
                    _resultDataService.AddEventResults(eventResultDto);
                }
                //TODO: Må laste inn kjønn korrekt
                //var gender = result.User.Male ? 'M' : 'F';
                
            }
        }

        private static int CalculateTime(TimeSpan time)
        {
            var minutes = 60 * time.Hours + time.Minutes;
            var seconds = ((float)time.Seconds) / 60;
            var sum = (minutes + seconds) * 60000 + new Random().Next(100, 10000);
            return Convert.ToInt32(sum);
        }

        private static double GetAgeGrade(User user)
        {
            //TODO: Finne agegrade basert på userid
            return 23.17;
        }

        private static string GetAgeCategory(User user)
        {
            var dob = user.DateOfBirth;
            var age = DateTime.Now.Year - new Random().Next(1945, 1995);
            Console.WriteLine("Age: " + age);
            if (age < 20)
            {
                return "J";
            } else if (age < 35)
            {
                return "S";
            } else if (age < 40)
            {
                return "A35";
            }
            else if (age < 45)
            {
                return "A40";
            }
            else if (age < 50)
            {
                return "A45";
            }
            else if (age < 55)
            {
                return "A50";
            }
            else if (age < 60)
            {
                return "A55";
            }
            else if (age < 65)
            {
                return "A60";
            }
            else if (age < 70)
            {
                return "A65";
            }
            else if (age < 75)
            {
                return "A70";
            }
            else if (age < 80)
            {
                return "A75";
            }
            else 
            {
                return "A80";
            }

        }


        private ICollection<Event> GetAllEvents()
        {
            var sql = "select date, id, name from kai_wpa_event";
            var query = new Query {Sql = sql};
            var result = _sqlDirectService.RunCommand(query);
            return result.Select(item => new Event
            {
                Id = (int) item["id"], Date = (DateTime) item["date"], Name = (string) item["name"]
            }).ToList();

        } 
    }
}
