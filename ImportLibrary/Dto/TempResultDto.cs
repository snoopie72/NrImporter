using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northernrunners.ImportLibrary.Dto
{
    public class TempResultDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Registered { get; set; }
        public string Data { get; set; }
    }
}
