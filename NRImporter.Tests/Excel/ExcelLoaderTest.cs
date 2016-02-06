using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Northernrunners.ImportLibrary.Excel;
using NUnit.Framework;

namespace NRImporter.Tests.Excel
{
    [TestFixture]
    class ExcelLoaderTest
    {
        
        [Test]
        public void LoadMemberList()
        {
            var resource = "NRImporter.Tests.Resources.Folkeparken1404.csv";
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream(resource))
            {
                var deltakere = new ExcelLoader().LoadRaceResult(stream);
            }

        }
    }
}
