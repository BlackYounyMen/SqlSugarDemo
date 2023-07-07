using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Model.View
{
    public class wirtecs
    {
        public static void WirteCsFile(string path, string spacename, string tablename, List<ViewColumn> viewcolumn)
        {
            path = path + "\\" + tablename + ".cs";

            StringBuilder sb = new StringBuilder();

            sb.Append("using System;  \n");
            sb.Append("using System.Linq;  \n");
            sb.Append("using System.Text;  \n");
            sb.Append("namespace " + spacename + "{" + "\n\n");
            sb.Append("public class " + tablename + "{" + "\n\n");
            foreach (var item in viewcolumn)
            {
                var counttype = GetCountType(item.udt_name);
                switch (counttype)
                {
                    case "short": sb.Append("public short " + item.column_name + " { get; set; } \n \n"); break;
                    case "int": sb.Append("public int " + item.column_name + " { get; set; } \n \n"); break;
                    case "long": sb.Append("public long " + item.column_name + " { get; set; } \n \n"); break;
                    case "string": sb.Append("public string " + item.column_name + " { get; set; } \n \n"); break;
                    case "dateTime": sb.Append("public DateTime " + item.column_name + " { get; set; } \n \n"); break;
                    default:
                        sb.Append("属性没有定义 " + item.column_name);
                        break;
                }
            }
            sb.Append("}  \n");
            sb.Append("}  \n");

            var fileContent = sb.ToString();

            // 使用 File 类的 WriteAllText 方法创建并写入文件
            File.WriteAllText(path, fileContent);
        }

        public static string GetCountType(string columnName)
        {
            var counttype = "";

            switch (columnName)
            {
                case "int2": counttype = "short"; break;
                case "int4": counttype = "int"; break;
                case "int8": counttype = "long"; break;
                case "varchar": counttype = "string"; break;
                case "timestamp": counttype = "dateTime"; break;
                default:
                    counttype = "";
                    break;
            }

            if (string.IsNullOrWhiteSpace(counttype))
            {
                Console.Write(columnName);
            }

            return counttype;
        }
    }
}