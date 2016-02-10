using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IronJS;
using IronJS.Hosting;

namespace Northernrunners.ImportLibrary.Utils
{
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
    public class ScriptRunner
    {
        private  CSharp.Context _context;
        private  FunctionObject _calculate;

        public ScriptRunner()
        {
            _context = new CSharp.Context();
            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources/calculate.js");
            _context.ExecuteFile(file);
            _calculate = _context.Globals.GetT<FunctionObject>("calculate");
        }

        public double CalculateAgeGrade(int age, double distance, TimeSpan time, string gender)
        {
            var result =
                _calculate.Call(_context.Globals, gender, Convert.ToDouble(age), Convert.ToDouble(distance / 1000),
                    time.TotalSeconds).Unbox<object>();
            var resultString = Convert.ToString(result);
            return Convert.ToDouble(resultString, new CultureInfo("nb-NO"));
        }
    }
}
