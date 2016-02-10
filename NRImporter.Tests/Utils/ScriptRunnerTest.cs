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
    internal class ScriptRunnerTest
    {
        private ScriptRunner _subject;

        [SetUp]
        public void Setup()
        {
            _subject = new ScriptRunner();
        }

        [TestCase(20, 10000, 0, 30, 20, "M")]
        [TestCase(20, 10000, 0, 30, 20, "F")]
        public void TestCalculateTime(int age, double distance, int hours, int minutes, int seconds, string gender)
        {
            var timespan = new TimeSpan(hours, minutes, seconds);
            var result = _subject.CalculateAgeGrade(age, distance, timespan, gender);
            Console.WriteLine(result);


        }
    }
}
