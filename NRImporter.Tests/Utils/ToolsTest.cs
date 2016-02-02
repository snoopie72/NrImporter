using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Northernrunners.ImportLibrary.Dto;
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

        [Test]
        public void TestConvertStringToDate()
        {
            var input = "23 jan 2010";
            var result = Tools.ParseDate(input);
            Assert.AreEqual(new DateTime(2010, 1, 23), result);
        }

        [Test]
        public void Serializer()
        {
            var input = new EventResultDto();
            input.AgeGrade = 3;
            Console.WriteLine(Tools.Serialize(input));
        }

        [Test]
        public void TestAge()
        {
            var dato = new DateTime(1972, 10, 05);
            Console.WriteLine(dato.Age(DateTime.Now));
        }
    }
}
