using System.Collections.Generic;

namespace Northernrunners.ImportLibrary.Service.Helper
{
    /// <summary>
    /// 
    /// </summary>
    public class Query
    {
        /// <summary>
        /// 
        /// </summary>
        public Query()
        {
            ParameterValues = new List<Parameter>();
        }
        /// <summary>
        /// 
        /// </summary>
        public string Sql { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ICollection<Parameter> ParameterValues { get; set; }
    }
}
