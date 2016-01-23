using System.Collections.Generic;
using Northernrunners.ImportLibrary.Poco;

namespace Northernrunners.ImportLibrary.Service
{
    public interface IUserService
    {
        void AddUser(User user);
        User FindUser(string name);

        ICollection<User> GetAllUsers();
    }
}
