﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Northernrunners.ImportLibrary.Dto;
using Northernrunners.ImportLibrary.Poco;
using Northernrunners.ImportLibrary.Service.Helper;
using Northernrunners.ImportLibrary.Utils;

namespace Northernrunners.ImportLibrary.Service.Datalayer
{
    public class ResultDataService:IResultDataService
    {
        private readonly ISqlDirectService _sqlDirectService;
        private readonly Assembly _assembly;

        public ResultDataService(ISqlDirectService sqlDirectService)
        {
            _sqlDirectService = sqlDirectService;
            _assembly = Assembly.GetAssembly(typeof(ResultDataService));
        }


        public void AddEventResults(EventResultDto eventResultDto)
        {
            var query = GetAddEventResultQuery(eventResultDto);

            _sqlDirectService.RunCommand(query);


            
        }

        private Query GetAddEventResultQuery(EventResultDto eventResultDto)
        {
            const string userTemplate = "Northernrunners.ImportLibrary.Resources.CreateEventResultTemplate.txt";

            var queries = new List<Query>();
            Query query;
            using (var stream = _assembly.GetManifestResourceStream(userTemplate))
            {
                var sql = Tools.StreamToString(stream);
                query = new Query {Sql = sql};
            }
            var properties = typeof (EventResultDto).GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();
            var sqlParams = GetParams(query.Sql);

            if (properties.Count != sqlParams.Count)
            {
                throw new Exception("Invalid properties");
            }

            var sqlParameters = query.ParameterValues;
            foreach (var property in properties)
            {
                if (!sqlParams.Contains(property.Name))
                {
                    throw new Exception("Cannot find property: " + property.Name);
                }
                sqlParameters.Add(new Parameter("@" + property.Name, property.GetValue(eventResultDto)));
            }
            return query;
        }

        public void AddEventResults(ICollection<EventResultDto> eventResultsDto)
        {

            var list = eventResultsDto.Select(GetAddEventResultQuery).ToList();
            _sqlDirectService.RunCommandsInSingleTransaction(list);
        }

        public ICollection<UserDto> GetAllUsers()
        {
            const string userTemplate = "Northernrunners.ImportLibrary.Resources.GetAllUsersTemplate.txt";
            using (var stream = _assembly.GetManifestResourceStream(userTemplate))
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                var sql = Tools.StreamToString(stream);
                var query = new Query { Sql = sql };
                var result = _sqlDirectService.RunCommand(query);
                var returnList = result.Select(data => new UserDto
                {
                    DateOfBirth = Tools.ParseDate(Convert.ToString(data["dob"])),
                    Gender = Convert.ToString(data["gender"]),
                    Email = Convert.ToString(data["email"]),
                    Name = Convert.ToString(data["name"]),
                    Id = Convert.ToInt32(data["id"])
                }).ToList();
                stopwatch.Stop();
                Console.WriteLine("Time: " + stopwatch.ElapsedMilliseconds);
                return returnList;


            }
        }

        public void AddUser(UserDto user)
        {
            //if (user.DateOfBirth.Equals(DateTime.MinValue))
            //{
            //    user.DateOfBirth = Tools.Randomize(new Random());
            //}
            var userTemplate = "Northernrunners.ImportLibrary.Resources.CreateUserTemplate.txt";

            var queries = new List<Query>();
            Query query;
            using (var stream = _assembly.GetManifestResourceStream(userTemplate))
            {

                var sql = Tools.StreamToString(stream);
                query = new Query { Sql = sql };

            }
            var username = user.Name.Replace(" ", string.Empty);
            query.ParameterValues.Add(new Parameter("@username", username));
            query.ParameterValues.Add(new Parameter("@email", ""));
            query.ParameterValues.Add(new Parameter("@date", user.DateOfBirth));
            query.ParameterValues.Add(new Parameter("@fullname", user.Name));

            queries.Add(query);

            query = new Query { Sql = "select id from kai_users where user_login = @username" };
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
            var gender = user.Gender;
            query.ParameterValues.Add(new Parameter("@userid", userId));
            query.ParameterValues.Add(new Parameter("@username", username));
            query.ParameterValues.Add(new Parameter("@firstname", firstname));
            query.ParameterValues.Add(new Parameter("@lastname", lastname));
            query.ParameterValues.Add(new Parameter("@dob", Tools.ParseDate(user.DateOfBirth)));
            query.ParameterValues.Add(new Parameter("@gender", gender));
            _sqlDirectService.RunCommand(query);
        }

        public void AddTempResult(TempResultDto tempResultDto)
        {
            const string userTemplate = "Northernrunners.ImportLibrary.Resources.CreateTempResult.txt";

            Query query;
            using (var stream = _assembly.GetManifestResourceStream(userTemplate))
            {

                var sql = Tools.StreamToString(stream);
                query = new Query { Sql = sql };

            }
            
            query.ParameterValues.Add(new Parameter("@userid", tempResultDto.UserId));
            query.ParameterValues.Add(new Parameter("@registered", tempResultDto.Registered));
            query.ParameterValues.Add(new Parameter("@data", tempResultDto.Data));

            _sqlDirectService.RunCommand(query);



        }

        public ICollection<TempResultDto> GetTempResults()
        {
            throw new NotImplementedException();
        }

        public void DeleteTempResult(TempResultDto tempResultDto)
        {
            throw new NotImplementedException();
        }

        public ICollection<Event> GetAllEvents()
        {
            var sql = "select date, id, name from kai_wpa_event";
            var query = new Query { Sql = sql };
            var result = _sqlDirectService.RunCommand(query);
            return result.Select(item => new Event
            {
                Id = (int)item["id"],
                Date = (DateTime)item["date"],
                Name = (string)item["name"]
            }).ToList();
        }

        private static List<string> GetParams(string sql)
        {
            var list = new List<string>
            {
                "DateCreated",
                "UserId",
                "EventId",
                "Time",
                "Position",
                "AgeCategory",
                "Gender",
                "AgeGrade"
            };
            return list;
        }
    }
}