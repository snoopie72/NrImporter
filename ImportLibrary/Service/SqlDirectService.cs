using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using Northernrunners.ImportLibrary.Service.Helper;

namespace Northernrunners.ImportLibrary.Service
{
    /// <summary>
    /// </summary>
    public class SqlDirectService : ISqlDirectService
    {
        private readonly String _connectionString;

        /// <summary>
        /// </summary>
        /// <param name="connectionString"></param>
        public SqlDirectService(string connectionString)
        {
            _connectionString = connectionString;
            Console.WriteLine(_connectionString);
        }

        /// <summary>
        /// </summary>
        /// <param name="statements"></param>
        /// <returns></returns>
        public ICollection<ICollection<Dictionary<string, object>>> RunCommandsInSingleTransaction(
            ICollection<string> statements)
        {
            var returnCollection = new List<ICollection<Dictionary<string, object>>>();

            var conn = new MySqlConnection();
            //conn.Unicode = true;
            conn.Open();

            using (var trans = conn.BeginTransaction())
            {
                try
                {
                    foreach (var statement in statements)
                    {
                        var coll = new List<Dictionary<string, object>>();
                        using (var cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = trans;
                            cmd.CommandText = statement;
                            var reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                var dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
                                for (var i = 0; i < reader.FieldCount; i++)
                                {
                                    var columnName = reader.GetName(i);
                                    var columnValue = reader.GetValue(i);
                                    dictionary.Add(columnName, columnValue);
                                }
                                //LogEventBusSingleton.Instance.Debug("Result", DictionaryToString(dictionary));
                                coll.Add(dictionary);
                            }
                        }
                        returnCollection.Add(coll);
                    }


                    trans.Commit();
                }
                catch (Exception e)
                {
                    trans.Rollback();
                }
            }
            conn.Close();

            return returnCollection;
        }

        

        public ICollection<Dictionary<string, object>> RunCommand(Query query)
        {
            var list = new List<Query> { query };
            var resultFromQuery = RunCommandsInSingleTransaction(list);
            if (resultFromQuery.Count != 1)
            {
                throw new Exception("Invalid results");
            }
            var iterator = resultFromQuery.GetEnumerator();
            iterator.MoveNext();
            return iterator.Current;
        }

        public ICollection<ICollection<Dictionary<string, object>>> RunCommandsInSingleTransaction(ICollection<Query> statements)
        {
           var returnCollection = new List<ICollection<Dictionary<string, object>>>();

            var conn = new MySqlConnection(_connectionString);
            //conn.Unicode = true;
            conn.Open();

            using (var trans = conn.BeginTransaction())
            {
                try
                {
                    foreach (var statement in statements)
                    {
                        var coll = new List<Dictionary<string, object>>();
                        using (var cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = trans;
                            cmd.CommandText = statement.Sql;
                            foreach (var param in statement.ParameterValues)
                            {
                                cmd.Parameters.AddWithValue(param.Name, param.Value);
                            }
                            var reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                var dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
                                for (var i = 0; i < reader.FieldCount; i++)
                                {
                                    var columnName = reader.GetName(i);
                                    var columnValue = reader.GetValue(i);
                                    dictionary.Add(columnName, columnValue);
                                }
                                //LogEventBusSingleton.Instance.Debug("Result", DictionaryToString(dictionary));
                                coll.Add(dictionary);
                            }
                            reader.Close();
                        }
                        returnCollection.Add(coll);
                    }


                    trans.Commit();
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    throw;
                }
            }
             conn.Close();

            return returnCollection;
        }
        
    }
}
