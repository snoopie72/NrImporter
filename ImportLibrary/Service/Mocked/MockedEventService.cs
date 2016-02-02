using System;
using System.Collections.Generic;
using Northernrunners.ImportLibrary.Dto;
using Northernrunners.ImportLibrary.Poco;

namespace Northernrunners.ImportLibrary.Service.Mocked
{
    public class MockedEventService:IEventService
    {


        public ICollection<Event> GetEvents(DateTime @from, DateTime to)
        {
            throw new NotImplementedException();
        }

        public ICollection<Event> GetEvents(string name, int year)
        {
            List<Event> eventer = new List<Event>();
            var ev1 = new Event()
            {
                Id = 1,
                Date = new DateTime(2015, 4, 14),
                Name = "Folkeparken"
            };
            var ev2 = new Event()
            {
                Id = 1,
                Date = new DateTime(2015, 5, 11),
                Name = "Krokenmila"
            };
            var ev3 = new Event()
            {
                Id = 1,
                Date = new DateTime(2015, 6, 8),
                Name = "Folkeparken"
            };
            eventer.Add(ev1);
            eventer.Add(ev2);
            eventer.Add(ev3);
            return eventer;
        }

        public void AddEventResults(EventResult eventResult)
        {
            throw new NotImplementedException();
        }

        public ICollection<Event> GetEvents(string name)
        {
            throw new NotImplementedException();
        }

        public Event GetEvent(int eventId)
        {
            throw new NotImplementedException();
        }

        public void AddEventResults(EventResultDto eventResult)
        {
            throw new NotImplementedException();
        }
    }
}
