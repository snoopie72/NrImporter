using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Northernrunners.ImportLibrary.Poco;

namespace Northernrunners.ImportLibrary.Service
{
    public class EventService:IEventService
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
