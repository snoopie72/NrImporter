using System;
using System.Collections.Generic;
using Northernrunners.ImportLibrary.Service.Helper;

namespace Northernrunners.ImportLibrary.Service
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISqlDirectService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="statements"></param>
        /// <returns></returns>
        /// 
        [Obsolete("RunCommandsInSingleTransaction is deprecated")]
        ICollection<ICollection<Dictionary<string, object>>> RunCommandsInSingleTransaction(ICollection<string> statements);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [Obsolete("RunCommandsInSingleTransaction is deprecated")]
        ICollection<Dictionary<string, object>> RunCommand(String query);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        ICollection<Dictionary<string, object>> RunCommand(Query query);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statements"></param>
        /// <returns></returns>
        ICollection<ICollection<Dictionary<string, object>>> RunCommandsInSingleTransaction(ICollection<Query> statements);
    }
}
