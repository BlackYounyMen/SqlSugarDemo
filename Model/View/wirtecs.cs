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

        public static void WirteCreateServices(string path, string spacename, string ControllerName, bool isShow)
        {
            string updatedName = ControllerName + "Service";

            path = path + "\\" + updatedName + ".cs";

            StringBuilder sb = new StringBuilder();
            if (isShow)
            {
                sb.Append("using System;  \n");
                sb.Append("using System.Linq;  \n");
                sb.Append("using System.Text;  \n");

                sb.Append("using khzn.Models;  \n");
                sb.Append("using KHZN.Support.Service;  \n");
            }

            sb.Append("namespace " + spacename + "{" + "\n\n");

            sb.Append("public class " + updatedName + " : BLLService<" + ControllerName + ">{" + "\n\n");
            sb.Append("public override void Validator(ValidatorType type)   {}");
            sb.Append("}  \n");
            sb.Append("}  \n");

            var fileContent = sb.ToString();

            // 使用 File 类的 WriteAllText 方法创建并写入文件
            File.WriteAllText(path, fileContent);
        }

        public static void WirteControllerFile(string path, string spacename, string ControllerName, bool isShow)
        {
            path = path + "\\" + ControllerName + ".cs";

            string updatedName = ControllerName.Replace("Controller", "Service");

            StringBuilder sb = new StringBuilder();
            if (isShow)
            {
                sb.Append("using System;  \n");
                sb.Append("using System.Linq;  \n");
                sb.Append("using System.Text;  \n");

                sb.Append("using KHZN.Kernel.Services;  \n");
                sb.Append("using KHZN.Support.Attribute;  \n");
                sb.Append("using KHZN.Support.Controller;  \n");
            }

            sb.Append("namespace " + spacename + "{" + "\n\n");

            sb.Append("public class " + ControllerName + " : KHZNController{" + "\n\n");

            sb.Append(" [Autowired] \n\n");
            sb.Append("  public " + updatedName + "  service { get; set; } \n\n");

            sb.Append(ControllerConfig());

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

        public static StringBuilder ControllerConfig()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("   /// <summary> \n");
            sb.Append("   /// 获取一条 \n");
            sb.Append("   /// </summary>\n");
            sb.Append("   /// <returns></returns> \n\n");

            sb.Append("public ResponseMsg load() { \n\n");
            sb.Append(" return ResponseMsg.Success(service.Load());");
            sb.Append(" } \n\n");

            sb.Append("   /// <summary>\n");
            sb.Append("   /// 获取全部 \n");
            sb.Append("   /// </summary>\n");
            sb.Append("   /// <returns></returns>   \n\n");

            sb.Append("public ResponseMsg list() { \n\n");
            sb.Append(" return ResponseMsg.Success(service.List(), service.TurnPage);");
            sb.Append(" } \n\n");

            sb.Append("   /// <summary>\n ");
            sb.Append("   /// 添加\n ");
            sb.Append("   /// </summary>\n");
            sb.Append("   /// <returns></returns>   \n\n");

            sb.Append("public ResponseMsg add() { \n\n");
            sb.Append(" return ResponseMsg.Success(service.Add());");
            sb.Append(" } \n\n");

            sb.Append("   /// <summary>\n");
            sb.Append("   /// 删除 \n");
            sb.Append("   /// </summary>\n");
            sb.Append("   /// <returns></returns>   \n\n");

            sb.Append("public ResponseMsg delete() { \n\n");
            sb.Append(" return ResponseMsg.Success(service.Delete());");
            sb.Append(" } \n\n");

            sb.Append("   /// <summary>\n ");
            sb.Append("   /// 修改 \n");
            sb.Append("   /// </summary>\n");
            sb.Append("   /// <returns></returns>   \n\n");

            sb.Append("public ResponseMsg Update() { \n\n");
            sb.Append(" return ResponseMsg.Success(service.Update());");
            sb.Append(" } \n\n");

            return sb;
        }
    }
}