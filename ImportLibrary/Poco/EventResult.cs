using System.Collections.Generic;

namespace Northernrunners.ImportLibrary.Poco
{
    public class EventResult
    {
        public Event Event;
        public ICollection<Result> Results { get; set; }
        

    }
}
