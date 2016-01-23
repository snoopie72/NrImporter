using System;
using System.Collections.Generic;
using Northernrunners.ImportLibrary.Poco;

namespace Northernrunners.ImportLibrary.Service.Mocked
{
    public class MockedTemporaryEventResultService:ITemporaryResultService
    {
        public void AddEventResults(EventResult eventResult)
        {
            throw new NotImplementedException();
        }

        public void RemoveResult(Result result)
        {
            throw new NotImplementedException();
        }

        public ICollection<EventResult> GetEventResults()
        {
            throw new NotImplementedException();
        }
    }
}
