using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace NR_Resultat_Import
{
    public class Deltaker
    {
        public string Name { get; set; }

        public string Gender { get; set; }

        public string Time { get; set; }

        public string Stage { get; set; }

        public string Place { get; set; }

        public bool ValidDate { get; set; }

    }
}
