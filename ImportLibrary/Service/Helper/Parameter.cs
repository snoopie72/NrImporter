using System;

namespace Northernrunners.ImportLibrary.Service.Helper
{
    /// <summary>
    /// 
    /// </summary>
    public class Parameter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
         /// <param name="value"></param>
        public Parameter(string name, Object value)
        {
            Value = value;
            Name = name;
        }
        /// <summary>
        /// 
        /// </summary>
        public Object Value { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        
    }
}
