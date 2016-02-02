using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northernrunners.ImportLibrary.Dto
{
    public class EventResultDto
    {
        public DateTime DateCreated { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
        public int Time { get; set; }
        public int Position { get; set; }
        public string AgeCategory { get; set; }
        public char Gender { get; set; }
        public double AgeGrade { get; set; }
    }
}
