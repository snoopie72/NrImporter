using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Northernrunners.ImportLibrary.Poco;

namespace Northernrunners.ImportLibrary.Service
{
    public class UserHandler:IUserHandler
    {
        private readonly IUserService _userService;

        public UserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public ICollection<User> LoadUserDetails(ICollection<User> users)
        {

            var returnCollection = users.Select(user => _userService.FindUser(user.Name) ?? _userService.AddUser(user)).ToList();
            return returnCollection;
        }
    }
}
