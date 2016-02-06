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
    public  class ExcelLoader
    {
        private const string ColumnFirstNameIdentifier = "Firstname";
        private const string ColumnLastNameIdentifier = "Surname";
        private const string ColumnGenderIdentifier = "Gender";
        private const string ColumnTimeIdentifier = "Total Time";
        private const string ColumnStageIdentifier = "Stage";
        private const string ColumnPlaceIdentifier = "Place Class";
        private const string ColumnClubIdentifier = "Club";
        public  ICollection<UserEventInfo> LoadRaceResult(Stream input)
        {
            var enc = Encoding.GetEncoding("ISO-8859-1");

            var lines = StreamToString(input, enc).ToList();
            var header = lines[0].Split(';');
            var firstNameColumn = FindColumn(header, ColumnFirstNameIdentifier);
            var lastNameColumn = FindColumn(header, ColumnLastNameIdentifier);
            var genderColumn = FindColumn(header, ColumnGenderIdentifier);
            var timeColumn = FindColumn(header, ColumnTimeIdentifier);
            var stageColumn = FindColumn(header, ColumnStageIdentifier);
            var placeColumn = FindColumn(header, ColumnPlaceIdentifier);
            var clubColumn = FindColumn(header, ColumnClubIdentifier);


            lines = lines.GetRange(1, lines.Count - 1);
            return (from t in lines
                select t.Split(";".ToCharArray(), StringSplitOptions.None)
                into data               
                select new UserEventInfo
                {
                    Firstname = firstNameColumn > -1 ? data[firstNameColumn].Trim() : null,
                    Lastname = lastNameColumn > -1 ? data[lastNameColumn].Trim() : null,
                    Gender = genderColumn > -1 ? data[genderColumn].Trim() : null,
                    Time = timeColumn > -1 ? data[timeColumn].Trim() : null,
                    Stage = stageColumn > -1 ? data[stageColumn].Trim() : null,
                    Place = placeColumn > -1 ? data[placeColumn].Trim() : null,
                    Club = clubColumn > -1 ? data[clubColumn].Trim() : null,


                }).ToList();
        }

        private static int FindColumn(IReadOnlyList<string> header, string columnIdentifier)
        {
            var i = 0;
            for (; i < header.Count; i++)
            {
                if (header[i].Equals(columnIdentifier))
                {
                    return i;
                }
            }
            return -1;
        }

        public  ICollection<User> LoadMemberFile(Stream input)
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
        private  IEnumerable<string> StreamToString(Stream stream, Encoding encoding)
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
