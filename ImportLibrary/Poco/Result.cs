using System;

namespace Northernrunners.ImportLibrary.Poco
{
    public class Result
    {
        public User User { get; set; }
        public TimeSpan Time { get; set; }
        public int Position { get; set; }
        public string AgeCategory { get; set; }
    }
}
