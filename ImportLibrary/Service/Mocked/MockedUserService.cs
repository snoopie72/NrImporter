using System;
using System.Collections.Generic;
using System.IO;
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
                Gender = "M",
                Name = "Jon-Vidar Schneider",
                Id = 1
            };
            users.Add(user);

            user = new User
            {
                Gender = "M",
                Id = 2,
                Name = "Kai Hugo Sørensen"
            };
            users.Add(user);
            user = new User
            {
                Gender = "F",
                Id = 3,
                Name = "Sylwia Smiegel"
            };
            users.Add(user);
            return users;

        }

        public void AddUser(User user)
        {
            
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

        public void AddUsers(ICollection<User> users)
        {
            
        }

        public ICollection<User> CreateAndGetUsers(ICollection<User> users, StreamWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
