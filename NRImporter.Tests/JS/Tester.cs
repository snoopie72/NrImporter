using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronJS;
using NUnit.Framework;

namespace NRImporter.Tests.JS
{
    [TestFixture]
    class Tester
    {

        [Test]
        public void SomeTest()
        {
            Console.WriteLine(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
            Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory);
            var context = new IronJS.Hosting.CSharp.Context();
            context.ExecuteFile(@"c:\temp\test.js");

            
            var span = new TimeSpan(0, 0, 42,37);
            var add = context.Globals.GetT<FunctionObject>("calculate");
            var age = 43;
            var distance = 10;
            var gender = "M";
            var value = Convert.ToDouble(add.Call(context.Globals, gender, Convert.ToDouble(age), Convert.ToDouble(distance), span.TotalSeconds).Unbox<object>());
            Console.WriteLine(value);
        }
    }
}
