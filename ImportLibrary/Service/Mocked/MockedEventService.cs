using System;
using System.Collections.Generic;
using Northernrunners.ImportLibrary.Poco;

namespace Northernrunners.ImportLibrary.Service.Mocked
{
    public class MockedEventService:IEventService
    {
        public ICollection<Event> GetEvents(DateTime @from, DateTime to)
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

        public void AddEventResults(EventResult eventResult)
        {
            throw new NotImplementedException();
        }
    }
}
