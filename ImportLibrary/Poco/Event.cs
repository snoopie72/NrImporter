using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northernrunners.ImportLibrary.Poco
{
    public class Event
    {
        int id;
        string name;
        DateTime date;
        string displayName;

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public DateTime Date
        {
            get
            {
                return date;
            }

            set
            {
                date = value;
            }
        }

        public string DisplayName
        {
            get
            {
                return String.Format("{0} {1}", date.ToShortDateString(), name);
            }
        }
    }
}
