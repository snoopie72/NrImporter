using System;
using System.Xml.Serialization;

namespace Northernrunners.ImportLibrary.Poco
{
    public class Result
    {
        public User User { get; set; }

        //Timespan kan ikke serialiseres av en eller annen merkelig grunn
        [XmlIgnore]
        public TimeSpan Time { get; set; }
        public int Position { get; set; }
        public string AgeCategory { get; set; }

        public long TimespanTicks
        {
            get { return Time.Ticks; }
            set { Time = new TimeSpan(value); }
        }
    }
}
