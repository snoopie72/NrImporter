namespace Northernrunners.ImportLibrary.Poco
{
    public class UserEventInfo
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public string Name => Firstname + " " + Lastname;

        public string Gender { get; set; }

        public string Time { get; set; }

        public string Stage { get; set; }

        public string Place { get; set; }

        public string Club { get; set; }

        public bool ValidDate { get; set; }

    }
}
