using System;

namespace Northernrunners.ImportLibrary.Poco
{
    public class User
    {
        public string Name { get; set; }
        public bool Male { get; set; }

        public int Id{ get; set; }

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public bool ValidUser()
        {
            return DateOfBirth.Year > 1900;
        }
    }
}
    