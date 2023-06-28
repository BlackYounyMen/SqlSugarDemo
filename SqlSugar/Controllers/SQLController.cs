using Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Model;
using SqlSugar;
using System.Linq;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Model.CrossQuery;

namespace SqlSugarInter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "SQL")]
    public class SQLController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SqlSugarClient _context;

        public SQLController(DbContext dbContext, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = GetInstance();
        }

        private SqlSugarClient GetInstance()
        {
            SqlSugarClient _context = new SqlSugarClient(new ConnectionConfig
            {
                ConnectionString = _configuration.GetConnectionString("SqlServer"),//连接符字串
                DbType = SqlSugar.DbType.SqlServer,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute//从特性读取主键自增信息
            });
            return _context;
        }

        /// <summary>
        /// 创建数据库
        /// </summary>
        [HttpGet("CreateDataBase")]
        public string CreateDataBase()
        {
            //建库：如果不存在创建数据库存在不会重复创建
            _context.DbMaintenance.CreateDatabase(); // 注意 ：Oracle和个别国产库需不支持该方法，需要手动建库

            //创建表：根据实体类CodeFirstTable1  (所有数据库都支持)
            _context.CodeFirst.InitTables(typeof(Student));//这样一个表就能成功创建了

            /***创建单个表***/
            _context.CodeFirst.SetStringDefaultLength(200).InitTables(typeof(Courses));//这样一个表就能成功创建了
            /***手动建多个表***/
            _context.CodeFirst.SetStringDefaultLength(200)
            .InitTables(typeof(School), typeof(StudentCourses));

            return "你在此" + _configuration.GetConnectionString("SqlServer").ToString() + "服务器上面创建了数据库";  //可以实现一键生成数据库脚本
        }

        /// <summary>
        /// 获取数据库所有表名
        /// </summary>
        [HttpGet("ShowTableName")]
        public Dictionary<string, object> ShowTableName()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            string connectionString = _configuration.GetConnectionString("SqlServerConnection"); // 替换为你的数据库连接字符串

            #region 根据所选的字符串进行截取出数据库名称

            int startIndex = connectionString.IndexOf("Database=") + "Database=".Length;
            int endIndex = connectionString.IndexOf(";", startIndex);

            string databaseName = connectionString.Substring(startIndex, endIndex - startIndex);

            dic.Add("你使用的数据库是", databaseName);

            #endregion 根据所选的字符串进行截取出数据库名称

            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = connectionString,
                DbType = DbType.SqlServer, // 替换为你使用的数据库类型
                IsAutoCloseConnection = true,
            });

            var tableNames = db.DbMaintenance.GetTableInfoList().Select(t => t.Name).ToList();   //查询出所有的表名
            var i = 1;
            foreach (var tableName in tableNames)
            {
                dic.Add(i.ToString(), tableName);
                i++;
            }
            return dic;
        }

        /// <summary>
        /// 最为简单的两表联查
        /// </summary>
        [HttpGet("TableJoin")]
        public string TableJoin()
        {
            // 执行联查查询
            var result = _context.Queryable<Student, School>((a, b) => a.Id == b.Id)
                .Select((a, b) => new
                {
                    a.Id,
                    a.Name,
                    schoolname = b.Name,
                    b.Principal
                })
                .ToList();

            return JsonConvert.SerializeObject(result);
        }

        /// <summary>
        /// 创建数据库(DB1)
        /// </summary>
        [HttpGet("CreateDataBaseDB1")]
        public string CreateDataBaseDB1()
        {
            SqlSugarClient _db = new SqlSugarClient(new ConnectionConfig
            {
                ConnectionString = _configuration.GetConnectionString("db1"),//连接符字串
                DbType = SqlSugar.DbType.SqlServer,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute//从特性读取主键自增信息
            });

            //建库：如果不存在创建数据库存在不会重复创建
            _db.DbMaintenance.CreateDatabase(); // 注意 ：Oracle和个别国产库需不支持该方法，需要手动建库

            /***创建单个表***/
            _db.CodeFirst.SetStringDefaultLength(200).InitTables(typeof(Order));//这样一个表就能成功创建了

            return "你在此" + _configuration.GetConnectionString("db1").ToString() + "服务器上面创建Order数据库";  //可以实现一键生成数据库脚本
        }

        /// <summary>
        /// 创建数据库(DB2)
        /// </summary>
        [HttpGet("CreateDataBaseDB2")]
        public string CreateDataBaseDB2()
        {
            SqlSugarClient _db = new SqlSugarClient(new ConnectionConfig
            {
                ConnectionString = _configuration.GetConnectionString("db2"),//连接符字串
                DbType = SqlSugar.DbType.SqlServer,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute//从特性读取主键自增信息
            });

            //建库：如果不存在创建数据库存在不会重复创建
            _db.DbMaintenance.CreateDatabase(); // 注意 ：Oracle和个别国产库需不支持该方法，需要手动建库

            /***创建单个表***/
            _db.CodeFirst.SetStringDefaultLength(200).InitTables(typeof(OrderItem));//这样一个表就能成功创建了

            return "你在此" + _configuration.GetConnectionString("db2").ToString() + "服务器上面创建OrderItem数据库";  //可以实现一键生成数据库脚本
        }

        /// <summary>
        /// 跨库的两表联查
        /// </summary>
        [HttpGet("CrossTableJoin")]
        public string CrossTableJoin()
        {
            var db = new SqlSugarClient(new List<ConnectionConfig>()
            {
              new ConnectionConfig(){ConfigId="db1",DbType=DbType.Sqlite,
              ConnectionString="DataSource=/Db_OrderDb.sqlite",IsAutoCloseConnection=true},

              new ConnectionConfig(){ConfigId="db2",DbType=DbType.Sqlite,
              ConnectionString="DataSource=/Db_OrderItemDb.sqlite",IsAutoCloseConnection=true }
            });

            //不通过特性实现跨库导航
            var list = db.GetConnection("db1").Queryable<OrderItem>()//Orderitem是db1
                           .CrossQuery(typeof(Order), "db2")//Order是db2
                           .Includes(z => z.Order)
                           .ToList();

            //通过实体类特性Tenant自动映射不同数据库进行查询
            var list1 = db.QueryableWithAttr<OrderItem>()
            .Includes(z => z.Order)
            .ToList(); //1行代码就搞定了2个库联表查询

            return JsonConvert.SerializeObject(list);
        }
    }
}