using Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Model;
using SqlSugar;
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
        /// <param name="spacename"></param>
        [HttpGet("GetCsFile")]
        public string GetCsFile(string spacename)
        {
            if (spacename == null)
            {
                return "必须输入命名空间";
            }
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "CSFile");

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

            #region 由于需要插入数据，所以没有正常使用 （“会往数据库里面添加数据”）

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

            #endregion 由于需要插入数据，所以没有正常使用 （“会往数据库里面添加数据”）

            SnowFlakeSingle.WorkId = 1; //从配置文件读取一定要不一样
                                        //服务器时间修改一定也要修改WorkId
                                        //用雪花ID一定要设置WORKID ， 只要静态变量SnowFlakeSingle不能共享的情况都要有独的WorkId
                                        //养成良好习惯服务器上的WorkId和本地不要一样，并且多服务器都要设置不一样的WorkId

            var get_id = SnowFlakeSingle.Instance.NextId();//也可以在程序中直接获取ID
            dic.Add("在程序中直接获取ID", get_id);

            return dic;
        }
    }
}