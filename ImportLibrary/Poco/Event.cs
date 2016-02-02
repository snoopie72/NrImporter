using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northernrunners.ImportLibrary.Poco
{
    public class Event
    {
        
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public double Distance { get; set; }

        public string DisplayName => $"{Date.ToShortDateString()} {Name}";
    }
}
