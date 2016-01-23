using System;

namespace Northernrunners.ImportLibrary.Poco
{
    public class Result
    {
        public User User { get; set; }
        public DateTime DateCreated { get; set; }
        public long Time { get; set; }
        public int Position { get; set; }
        public string AgeCategory { get; set; }
    }
}
