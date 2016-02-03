using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IronJS;
using IronJS.Hosting;
using Northernrunners.ImportLibrary.Dto;
using Northernrunners.ImportLibrary.Poco;
using Northernrunners.ImportLibrary.Service.Datalayer;
using Northernrunners.ImportLibrary.Utils;

namespace Northernrunners.ImportLibrary.Service
{
    public class EventService:IEventService
    {
        private Random _rnd;
        private readonly IResultDataService _resultDataService;
        private CSharp.Context _context;
        private FunctionObject _calculate;
        public EventService(IResultDataService resultDataService)
        {
            _rnd = new Random();
            _resultDataService = resultDataService;
            _context = new IronJS.Hosting.CSharp.Context();

            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources/calculate.js");
            _context.ExecuteFile(file);
            _calculate = _context.Globals.GetT<FunctionObject>("calculate");
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
            var query = new List<EventResultDto>();
            foreach (var result in eventResult.Results)
            {
                    var time = CalculateTime(result.Time);
                    var eventResultDto = new EventResultDto
                    {
                        EventId = eventResult.Event.Id,
                        AgeCategory = GetAgeCategory(result.User),
                        AgeGrade = GetAgeGrade(result.User, result.Time, eventResult.Event),
                        DateCreated = DateTime.Now,
                        Gender = result.User.Gender,
                        UserId = result.User.Id,
                        //TODO: Fikse time.. ligger i resultat
                        Time = time,
                        Position = result.Position
                    };
                if (result.User.DateOfBirth.Equals(DateTime.MinValue))
                {
                    var dataObject = Tools.Serialize(eventResultDto);
                    var tempResult = new TempResultDto
                    {
                        Data = dataObject,
                        Registered = DateTime.Now,
                        UserId = result.User.Id
                    };
                    _resultDataService.AddTempResult(tempResult);
                }
                else
                {
                    //_resultDataService.AddEventResults(eventResultDto);
                    query.Add(eventResultDto);
                }
            }
            _resultDataService.AddEventResults(query);
        }

        private double GetAgeGrade(User user, TimeSpan time, Event @event)
        {
            var age = user.DateOfBirth.Age(DateTime.Now);
            var distance = @event.Distance;
            var result = CalculateAge(age, distance, time, user.Gender);
            return result;
        }

        private double CalculateAge(int age, double distance, TimeSpan time, string gender)
        {            
            return Convert.ToDouble(_calculate.Call(_context.Globals, gender, Convert.ToDouble(age), Convert.ToDouble(distance / 1000), time.TotalSeconds).Unbox<object>());
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
            Console.WriteLine("User: " + user.Name + " " + user.DateOfBirth);
            var dob = user.DateOfBirth;
            var age = DateTime.Now.Year - user.DateOfBirth.Year;
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


        private IEnumerable<Event> GetAllEvents()
        {
            return _resultDataService.GetAllEvents();

        } 
    }
}
