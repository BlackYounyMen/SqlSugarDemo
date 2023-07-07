using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.View
{
    public class ViewColumn
    {
        public string column_name { get; set; }

        public string udt_name { get; set; }

        public static string GetSchema(string consqlcommand)
        {
            // 使用连接字符串的 Split 方法，按分号进行分割
            string[] parts = consqlcommand.Split(';');

            // 获取最后一个部分，并使用 Split 方法按等号进行分割
            string lastPart = parts[parts.Length - 1];
            string[] lastPartParts = lastPart.Split('=');

            return lastPartParts[1];
        }
    }
}