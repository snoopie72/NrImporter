using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Northernrunners.ImportLibrary.Poco;

namespace Northernrunners.ImportLibrary.Service
{
    public interface IUserHandler
    {
        ICollection<User> LoadUserDetails(ICollection<User> users);


    }
}
