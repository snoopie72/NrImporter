using System;
using Northernrunners.ImportLibrary.Poco;

namespace Northernrunners.ImportLibrary.Service.Mocked
{
    public class MockedUserService:IUserService
    {
        public void AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public User FindUser(string name)
        {
            throw new NotImplementedException();
        }
    }
}
