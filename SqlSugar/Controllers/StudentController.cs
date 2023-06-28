using LinqKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Model;
using System;
using System.Collections.Generic;

namespace SqlSugar.Controllers
{
    //引用工具        Masuit.Tools

    // _context.Insertable<Student>(student).ExecuteCommand()   严格来说这种写法最为标准
    // _context.Insertable(student).ExecuteCommand()   貌似这样也可以，好像会自动识别
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Base")]
    public class StudentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SqlSugarClient _context;

        public StudentController(IConfiguration configuration)
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

        #region 简单语法

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPost("Insert")]
        public int Insert(Student student)
        {
            return _context.Insertable<Student>(student).ExecuteCommand();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPost("Update")]
        public int Update(Student student)
        {
            return _context.Updateable<Student>(student).ExecuteCommand();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("Delete")]
        public int Delete(int id)
        {
            return _context.Deleteable<Student>(t => t.Id == id).ExecuteCommand();
        }

        /// <summary>
        /// 查询  sex  F/M
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>

        [HttpPost("Query")]
        public List<Student> Query(Student student)
        {
            //linqkit 语法糖
            var predicate = PredicateBuilder.New<Student>(true);

            if (student.Id > 0)
            {
                predicate.And(t => t.Id == student.Id);
            }

            if (student.Age > 0)
            {
                predicate.And(t => t.Age == student.Age);
            }
            if (student.Sex == "F" || student.Sex == "M")
            {
                predicate.And(t => t.Sex == student.Sex);
            }

            if (!string.IsNullOrEmpty(student.Name))
            {
                predicate.And(t => t.Name == student.Name);
            }
            // 分页写法  _context.Queryable<Student>().ToPageList(1,10);  Masuit.Tools 工具当中

            //sqlsugar 语法
            return _context.Queryable<Student>().Where(predicate).ToList();
        }

        /// <summary>
        /// 查询第一条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("FirstOrDefault")]
        public Student FirstOrDefault(int id)
        {
            //linqkit 语法糖
            var predicate = PredicateBuilder.New<Student>();
            predicate.And(t => t.Id == id);

            //sqlsugar 语法
            return _context.Queryable<Student>().First(predicate);
        }

        #endregion 简单语法
    }
}