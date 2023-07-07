using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace SqlSugarInter.Util
{
    public class SQLScript
    {
        private readonly IConfiguration _configuration;

        public SQLScript(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// 执行sql命令
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<object[]> UsingSql(string sql)
        {
            string connectionString = _configuration.GetConnectionString("SqlServerConnection");
            List<object[]> result = new List<object[]>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            object[] rowData = new object[reader.FieldCount];
                            reader.GetValues(rowData);
                            result.Add(rowData);
                        }
                    }
                }
            }

            return result;
        }
    }
}