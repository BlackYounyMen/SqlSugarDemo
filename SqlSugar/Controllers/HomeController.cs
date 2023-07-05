using Core;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Model;
using Newtonsoft.Json;
using SqlSugar;
using SqlSugarInter.Util;
using System.Collections.Generic;
using System.IO;

namespace SqlSugarInter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Home")]
    public class HomeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SqlSugarClient _context;

        public HomeController(DbContext dbContext, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = GetInstance();
        }

        /// <summary>
        /// 检查该字符串是否是路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static bool IsFilePath(string path)
        {
            if (string.IsNullOrEmpty(path)) return false;

            // 检查是否包含路径分隔符
            if (path.Contains(Path.DirectorySeparatorChar.ToString()) || path.Contains(Path.AltDirectorySeparatorChar.ToString()))
            {
                return true;
            }

            return false;
        }

        private SqlSugarClient GetInstance()
        {
            SqlSugarClient _context = new SqlSugarClient(new ConnectionConfig
            {
                ConnectionString = _configuration.GetConnectionString("SqlServerConnection"),//连接符字串
                DbType = SqlSugar.DbType.SqlServer,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute//从特性读取主键自增信息
            });
            return _context;
        }

        /// <summary>
        /// 生成实体文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="spacename"></param>
        [HttpGet("GetCsFile")]
        public string GetCsFile(string path, string spacename)
        {
            if (spacename == null)
            {
                return "必须输入命名空间";
            }
            if (!IsFilePath(path))
            {
                path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "CSFile");
            }

            _context.DbFirst.IsCreateAttribute().CreateClassFile(path, spacename);
            return "生成的实体的目标位置是：    " + path;
        }

        /// <summary>
        /// 生成雪花的几种方式
        /// </summary>
        [HttpGet("Snowflake")]
        public Dictionary<string, object> Snowflake()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();

            #region 由于需要插入数据，所以没有正常使用 （“会往数据库里面添加数据”）,雪花id会生成不同的id，但是还是自增id查询起来效率稍微好点

            Student student1 = new Student()
            {
                Name = "Alice",
                Age = 20,
                Sex = "Female"
            };

            Student student2 = new Student()
            {
                Name = "Bob",
                Age = 22,
                Sex = "Male"
            };

            Student student3 = new Student()
            {
                Name = "Charlie",
                Age = 19,
                Sex = "Male"
            };

            List<Student> students = new List<Student>();
            students.Add(student1);
            students.Add(student2);
            students.Add(student3);
            long id = _context.Insertable<Student>(student1).ExecuteReturnSnowflakeId();//单条插入返回雪花ID

            List<long> ids = _context.Insertable(students).ExecuteReturnSnowflakeIdList();//多条插入批量返回,比自增好用
            dic.Add("单条插入返回雪花ID", id.ToString());
            dic.Add("多条插入批量返回,ID", ids);

            #endregion 由于需要插入数据，所以没有正常使用 （“会往数据库里面添加数据”）,雪花id会生成不同的id，但是还是自增id查询起来效率稍微好点

            SnowFlakeSingle.WorkId = 1; //从配置文件(appsetting.json || webconfig)读取一定要不一样
                                        //服务器时间修改一定也要修改WorkId
                                        //用雪花ID一定要设置WORKID ， 只要静态变量SnowFlakeSingle不能共享的情况都要有独的WorkId
                                        //养成良好习惯服务器上的WorkId和本地不要一样，并且多服务器都要设置不一样的WorkId

            var get_id = SnowFlakeSingle.Instance.NextId();//也可以在程序中直接获取ID
            dic.Add("在程序中直接获取ID", get_id);

            return dic;
        }

        /// <summary>
        /// 使用sql 语句进行查询
        /// </summary>
        [HttpGet("UsingSql")]
        public string UsingSql(string sql = "select *  from student")
        {
            SQLScript sqlscript = new SQLScript(_configuration);
            var data = sqlscript.UsingSql(sql);
            return JsonConvert.SerializeObject(data);
        }

        /// <summary>
        /// 生成kingbase实体文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="spacename"></param>
        /// <param name="kingbase">链接数据库</param>
        [HttpGet("GetKingBaseFile")]
        public string GetKingBaseFile(string path, string spacename, string kingbase = "Host=10.168.1.138;Port=54321;database=demo;uid=system;pwd=123456;searchpath=khzn")
        {
            if (string.IsNullOrEmpty(kingbase)) kingbase = _configuration.GetConnectionString("kingbase");

            if (spacename == null)
            {
                return "必须输入命名空间";
            }
            if (!IsFilePath(path))
            {
                path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "CSFile");
            }

            SqlSugarClient _context = new SqlSugarClient(new ConnectionConfig
            {
                ConnectionString = kingbase,//连接符字串
                DbType = SqlSugar.DbType.PostgreSQL,
                IsAutoCloseConnection = false,
                InitKeyType = InitKeyType.Attribute//从特性读取主键自增信息
            });

            _context.DbFirst.IsCreateAttribute().CreateClassFile(path, spacename);
            return "生成的实体的目标位置是：    " + path;
        }
    }
}