using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Northernrunners.ImportLibrary.Poco;
using Northernrunners.ImportLibrary.Service.Helper;

namespace Northernrunners.ImportLibrary.Service
{
    public class EventService:IEventService
    {
        private readonly ISqlDirectService _sqlDirectService;

        public EventService(ISqlDirectService sqlDirectService)
        {
            _sqlDirectService = sqlDirectService;
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
            throw new NotImplementedException();
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
