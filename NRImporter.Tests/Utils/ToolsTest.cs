using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Northernrunners.ImportLibrary.Utils;
using NUnit.Framework;

namespace NRImporter.Tests.Utils
{
    [TestFixture]
    class ToolsTest
    {
        [Test]
        public void TestConvertDate()
        {
            var date = new DateTime(2010, 10,5);
            var result = Tools.ParseDate(date);
            Console.Write(result);
        }
    }
}
