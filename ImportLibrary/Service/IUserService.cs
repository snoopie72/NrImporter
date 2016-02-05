using System.Collections.Generic;
using System.IO;
using Northernrunners.ImportLibrary.Poco;

namespace Northernrunners.ImportLibrary.Service
{
    public interface IUserService
    {
        void AddUser(User user);
        User FindUser(string name);

        Poco.User FindUser(int id);

        ICollection<User> GetAllUsers();

        void AddUsers(ICollection<User> users);

        ICollection<User> CreateAndGetUsers(ICollection<User> users, StreamWriter writer);

        ICollection<User> GetAllUsersWithInvalidDate();

        void UpdateUser(User user);

    }
}