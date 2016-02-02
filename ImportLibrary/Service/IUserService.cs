using System.Collections.Generic;
using Northernrunners.ImportLibrary.Poco;

namespace Northernrunners.ImportLibrary.Service
{
    public interface IUserService
    {
        User AddUser(User user);
        User FindUser(string name);

        ICollection<User> GetAllUsers();
    }
}
