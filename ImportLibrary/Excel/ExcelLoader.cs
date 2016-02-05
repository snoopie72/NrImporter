using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Northernrunners.ImportLibrary.Poco;
using Northernrunners.ImportLibrary.Utils;

namespace Northernrunners.ImportLibrary.Excel
{
    public static class ExcelLoader
    {
        public static ICollection<UserEventInfo> LoadRaceResult(Stream input, string separator)
        {
            var enc = Encoding.GetEncoding("ISO-8859-1");

            var lines = StreamToString(input, enc);
            return (from t in lines
                select t.Split(";".ToCharArray(), StringSplitOptions.None)
                into data
                where data[4].Contains(separator)
                select new UserEventInfo
                {
                    Name = $"{data[1]} {data[2]}", Gender = data[3], Time = data[8], Stage = data[5], Place = data[11]
                }).ToList();
        }

        public static ICollection<User> LoadMemberFile(Stream input)
        {
            var enc = Encoding.GetEncoding("ISO-8859-1");
            var lines = StreamToString(input, enc);
            var users = new List<User>();
            int i = 0;
            foreach (var line in lines)
            {
                if (i > 0) {
                    var data = line.Split(';');
                    var dato = Convert.ToString(data[6]);
                    Console.WriteLine(dato);
                    var gender = data[7].Equals("Mann") ? "M" : "F";
                    var user = new User
                    {
                        DateOfBirth = Tools.ParseDateMember(dato),
                        Email = Convert.ToString(data[1]).Trim(),
                        Gender = gender,
                        Name = Convert.ToString(data[0]).Trim()
                    };
                    users.Add(user);
                }
                i++;


            }
            return users;


        } 
        private static IEnumerable<string> StreamToString(Stream stream, Encoding encoding)
        {
            var list = new List<string>();
            using (var streamReader = new StreamReader(stream, encoding))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    line = line.Replace("\"", "");                        
                    list.Add(line);
                }
                return list.ToArray();
            }
            
            



        }
    }
}
