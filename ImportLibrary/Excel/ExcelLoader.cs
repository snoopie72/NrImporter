using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using NR_Resultat_Import;

namespace Northernrunners.ImportLibrary.Excel
{
    public static class ExcelLoader
    {
        public static ICollection<Deltaker> LoadRaceResult(Stream input, string separator)
        {
            //var x = new MockedEventService();
            //textBox1.Text = x.GetEvents("adsf", 2016).ToList().FirstOrDefault().Id1.ToString();
            //var enc = Encoding.GetEncoding("ISO-8859-1");
            var enc = Encoding.GetEncoding("ISO-8859-1");

            var lines = StreamToString(input, enc);
            return (from t in lines
                select t.Split(";".ToCharArray(), StringSplitOptions.None)
                into data
                where data[4].Contains(separator)
                select new Deltaker
                {
                    Name = $"{data[1]} {data[2]}", Gender = data[3], Time = data[8], Stage = data[5], Place = data[11]
                }).ToList();
        }

        private static IEnumerable<string> StreamToString(Stream stream, Encoding encoding)
        {
            var list = new List<string>();
            using (var streamReader = new StreamReader(stream, encoding))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    list.Add(line);
                }
                return list.ToArray();
            }
            
            



        }
    }
}
