using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Northernrunners.ImportLibrary.Poco;
using Northernrunners.ImportLibrary.Service.Helper;
using Northernrunners.ImportLibrary.Utils;

namespace Northernrunners.ImportLibrary.Service
{
    public class UserService:IUserService
    {
        private readonly ISqlDirectService _sqlDirectService;
        private Assembly _assembly;
        public UserService(ISqlDirectService sqlDirectService)
        {
            _sqlDirectService = sqlDirectService;
            _assembly = Assembly.GetAssembly(typeof(UserService));
        }

        public User AddUser(User user)
        {
            if (user.DateOfBirth.Equals(DateTime.MinValue))
            {
                user.DateOfBirth = Tools.Randomize(new Random());
            }
            var userTemplate = "Northernrunners.ImportLibrary.Resources.CreateUserTemplate.txt";

            var queries = new List<Query>();
            Query query;
            using (var stream = _assembly.GetManifestResourceStream(userTemplate))
            {
                
                var sql = Tools.StreamToString(stream);
                query = new Query {Sql = sql};

            }
            var username = user.Name.Replace(" ", string.Empty);
            query.ParameterValues.Add(new Parameter("@username", username));
            query.ParameterValues.Add(new Parameter("@email", ""));
            query.ParameterValues.Add(new Parameter("@date", user.DateOfBirth));
            query.ParameterValues.Add(new Parameter("@fullname", user.Name));

            queries.Add(query);

            query = new Query {Sql = "select id from kai_users where user_login = @username"};
            query.ParameterValues.Add(new Parameter("username", username));
            queries.Add(query);
            var result = _sqlDirectService.RunCommandsInSingleTransaction(queries);

            var dataset = result.ToList()[1];
            var dictionary = dataset.ToList()[0];
            var userId = Convert.ToInt32(dictionary["id"]);

            userTemplate = "Northernrunners.ImportLibrary.Resources.CreateUsermetaTemplate.txt";
            using (var stream = _assembly.GetManifestResourceStream(userTemplate))
            {

                var sql = Tools.StreamToString(stream);
                query = new Query { Sql = sql };

            }
            var split = user.Name.Split(' ');
            var size = split.Length;
            var lastname = split[size - 1];
            var firstname = user.Name.Replace(lastname, string.Empty);
            var gender = user.Male ? "M" : "F";
            query.ParameterValues.Add(new Parameter("@userid", userId));
            query.ParameterValues.Add(new Parameter("@username", username));
            query.ParameterValues.Add(new Parameter("@firstname", firstname));
            query.ParameterValues.Add(new Parameter("@lastname", lastname));
            query.ParameterValues.Add(new Parameter("@dob", Tools.ParseDate(user.DateOfBirth)));
            query.ParameterValues.Add(new Parameter("@gender", gender));
            _sqlDirectService.RunCommand(query);

            return FindUser(username);

        }

        public User FindUser(string name)
        {
            return  GetAllUsers().FirstOrDefault(t => t.Name.Equals(name));
        }

        public ICollection<User> GetAllUsers()
        {
            //TODO: Endre slik at dato også kommer tilbake, ligger i usermeta
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
