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
            var context = new IronJS.Hosting.CSharp.Context();
            context.ExecuteFile(@"c:\temp\test.js");

            //object result = context.Execute("1 + 2;");
            //var result = context.Execute("calculate('M',30, 10, 1875421)");

            //Console.WriteLine("{0} ({1})", obj, obj.GetType());
            // "3 (System.Double)"

            var add = context.Globals.GetT<FunctionObject>("calculate");
            var value = Convert.ToDouble(add.Call(context.Globals, "F", 43D, 10000D, 2098421D).Unbox<object>());
            Console.WriteLine(value);
        }
    }
}
