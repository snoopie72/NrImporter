using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Northernrunners.ImportLibrary.Dto;
using Northernrunners.ImportLibrary.Poco;

namespace Northernrunners.ImportLibrary.Service.Datalayer
{
    public interface IResultDataService
    {
        void AddEventResults(EventResultDto eventResultDto);

        void AddEventResults(ICollection<EventResultDto> eventResultsDto);
        ICollection<UserDto> GetAllUsers();

        void AddUser(UserDto user);

        void AddUsers(ICollection<UserDto> users);
        void AddTempResult(TempResultDto tempResultDto);

        ICollection<TempResultDto> GetTempResults();


        void DeleteTempResult(TempResultDto tempResultDto);

        ICollection<Event> GetAllEvents();

        void UpdateUser(UserDto user);
    }
}
