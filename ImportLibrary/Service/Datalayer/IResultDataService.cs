using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Northernrunners.ImportLibrary.Dto;

namespace Northernrunners.ImportLibrary.Service.Datalayer
{
    public interface IResultDataService
    {
        void AddEventResults(EventResultDto eventResultDto);

    }
}
