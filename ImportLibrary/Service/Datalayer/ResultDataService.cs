using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Northernrunners.ImportLibrary.Dto;
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
            const string userTemplate = "Northernrunners.ImportLibrary.Resources.CreateEventResultTemplate.txt";

            var queries = new List<Query>();
            Query query;
            using (var stream = _assembly.GetManifestResourceStream(userTemplate))
            {

                var sql = Tools.StreamToString(stream);
                query = new Query { Sql = sql };

            }
            var properties = typeof(EventResultDto).GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();
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

            _sqlDirectService.RunCommand(query);


            Console.WriteLine(query.Sql);
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
