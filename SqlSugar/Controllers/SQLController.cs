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
            string connectionString = _configuration.GetConnectionString("SqlServerConnection"); // 替换为你的数据库连接字符串

            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = connectionString,
                DbType = DbType.SqlServer, // 替换为你使用的数据库类型
                IsAutoCloseConnection = true,
            });
            // 执行联查查询
            var result = db.Queryable<Student, School>((a, b) => a.Id == b.Id)
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
    }
}