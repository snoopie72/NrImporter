using System;
using System.Collections.Generic;
using System.Linq;
using Northernrunners.ImportLibrary.Poco;

namespace Northernrunners.ImportLibrary.Service.Mocked
{
    public class MockedUserService:IUserService
    {
        private readonly ICollection<User> _users;

        public MockedUserService()
        {
            _users = FillUsers();
        }

        private ICollection<User> FillUsers()
        {
            var users = new List<User>();
            var user = new User
            {
                Male = true,
                Name = "Jon-Vidar Schneider",
                Id = 1
            };
            users.Add(user);

            user = new User
            {
                Male = true,
                Id = 2,
                Name = "Kai Hugo Sørensen"
            };
            users.Add(user);
            user = new User
            {
                Male = false,
                Id = 3,
                Name = "Sylwia Smiegel"
            };
            users.Add(user);
            return users;

        }

        public User AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public User FindUser(string name)
        {
            return _users.FirstOrDefault(user => user.Name.Equals(name));
        }

        public ICollection<User> GetAllUsers()
        {
            return _users;
            ;

        }
    }
}
