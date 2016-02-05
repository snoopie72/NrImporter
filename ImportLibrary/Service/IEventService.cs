using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Northernrunners.ImportLibrary.Dto;
using Northernrunners.ImportLibrary.Poco;

namespace Northernrunners.ImportLibrary.Service
{
    public interface IEventService
    {
        ICollection<Event> GetEvents(DateTime from, DateTime to);

        ICollection<Event> GetEvents(string name, int year);
        void AddEventResults(EventResult eventResult);

        ICollection<Event> GetEvents(string name);

        ICollection<TempResultDto> GetAllTempResults();

        void DeleteTempResult(TempResultDto tempResult);


    }
}
