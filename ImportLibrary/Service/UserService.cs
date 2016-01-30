using System;
using System.Collections.Generic;
using System.Linq;
using Northernrunners.ImportLibrary.Poco;
using Northernrunners.ImportLibrary.Service.Helper;

namespace Northernrunners.ImportLibrary.Service
{
    public class UserService:IUserService
    {
        private readonly ISqlDirectService _sqlDirectService;

        public UserService(ISqlDirectService sqlDirectService)
        {
            _sqlDirectService = sqlDirectService;
        }

        public void AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public User FindUser(string name)
        {
            return  GetAllUsers().FirstOrDefault(t => t.Name.Equals(name));
        }

        public ICollection<User> GetAllUsers()
        {
            var query = new Query {Sql = "select a.id, a.display_name , b.meta_value from kai_users a, kai_usermeta b where a.ID = b.user_id and b.meta_key = 'wp-athletics_gender'" };
            var result = _sqlDirectService.RunCommand(query);
            return (from item in result
                let id = Convert.ToInt32(item["id"])
                let name = (string) item["display_name"]
                let metaValue = (string) item["meta_value"]
                let gender = metaValue.Equals("M")
                select new User
                {
                    Name = name, Id = id, Male = gender
                }).ToList();
        }
    }
}
