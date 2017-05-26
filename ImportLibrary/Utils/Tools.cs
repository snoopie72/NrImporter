using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using Northernrunners.ImportLibrary.Poco;

namespace Northernrunners.ImportLibrary.Utils
{
    public static class Tools
    {

        public static string StreamToString(Stream stream)
        {
            stream.Position = 0;
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }

        public static string ParseDate(DateTime date)
        {
            return $"{date:dd MMM yyyy}";
        }

        public static DateTime ParseDate(string date)
        {
            try
            {
                return DateTime.ParseExact(date, "dd MMM yyyy", CultureInfo.GetCultureInfo("en-GB"));
            }
            catch (System.FormatException)
            {
                date = date.Replace("mai", "May");
                date = date.Replace("okt", "Oct");
                date = date.Replace("des", "Dec");
                date = date.Replace("Mai", "May");
                date = date.Replace("Okt", "Oct");
                date = date.Replace("Des", "Dec");
                return DateTime.ParseExact(date, "dd MMM yyyy", CultureInfo.GetCultureInfo("en-GB"));
            }
            catch (Exception exp)
            {
                throw;
            }
            
        }

        public static DateTime ParseDateMember(string date)
        {
            return DateTime.ParseExact(date, "dd.MM.yyyy", CultureInfo.GetCultureInfo("no-NO"));
        }

        public static object GetCellValueFromColumnHeader(this DataGridViewCellCollection cellCollection,
            string headerText)
        {
            return cellCollection.Cast<DataGridViewCell>().First(c => c.OwningColumn.HeaderText == headerText).Value;
        }

        public static void RandomizeDateOfBirth(ICollection<User> users)
        {

            var random = new Random();
            foreach (var user in users)
            {
                user.DateOfBirth = Randomize(random);
            }
        }

        public static DateTime Randomize(Random random)
        {
            var year = random.Next(1940, 2000);
            var month = random.Next(1, 12);
            var day = random.Next(1, 28);
            return new DateTime(year, month, day);
        }

        public static string Serialize<T>(T datatype)
        {
            var xsSubmit = new XmlSerializer(typeof (T));
            using (var sww = new StringWriter())
            using (var writer = XmlWriter.Create(sww))
            {
                xsSubmit.Serialize(writer, datatype);
                var xml = sww.ToString(); // Your XML
                return xml;
            }
        }

        public static T Deserializate<T>(string document)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(document);
            var xmlSerializer = new XmlSerializer(typeof(T));
            using (var xmlReader = new XmlNodeReader(xmlDocument))
            {
                var result = (T) xmlSerializer.Deserialize(xmlReader);
                return result;
            }
            
        }

        public static int Age(this DateTime birthDate, DateTime laterDate)
        {
            var age = laterDate.Year - birthDate.Year;
            if (age > 0)
            {
                age -= Convert.ToInt32(laterDate.Date < birthDate.Date.AddYears(age));
            }
            else
            {
                age = 0;
            }
            return age;
        }
    }
}
