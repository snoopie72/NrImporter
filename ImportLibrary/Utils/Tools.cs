using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

        public static object GetCellValueFromColumnHeader(this DataGridViewCellCollection cellCollection, string headerText)
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
    }
}
