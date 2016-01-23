using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
