using Core;
using LinqKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using System.Collections.Generic;
using Masuit.Tools.Net;
using Masuit.Tools.Models;
using System.Linq;

namespace SqlSugar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Package")]
    public class SchoolController : ControllerBase
    {
        private readonly DbContext _dbContext;

        public SchoolController(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="school"></param>
        /// <returns></returns>
        [HttpPost("Insert")]
        public bool Insert(School school)
        {
            return _dbContext.SchoolDb.Insert(school);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="school"></param>
        /// <returns></returns>
        [HttpPost("Update")]
        public bool Update(School school)
        {
            return _dbContext.SchoolDb.Update(school);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("Delete")]
        public bool Delete(int id)
        {
            return _dbContext.SchoolDb.Delete(t => t.Id == id);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("Query")]
        public List<School> Query(School school)
        {
            PageModel g = new PageModel();

            g.PageIndex = 1;
            g.PageSize = 3;
            //linqkit 语法糖
            var predicate = PredicateBuilder.New<School>(true);

            if (school.Id > 0)
            {
                predicate.And(t => t.Id == school.Id);
            }

            if (!string.IsNullOrEmpty(school.Name))
            {
                predicate.And(t => t.Name == school.Name);
            }

            if (!string.IsNullOrEmpty(school.Location))
            {
                predicate.And(t => t.Location == school.Location);
            }

            if (school.Capacity > 0)
            {
                predicate.And(t => t.Capacity == school.Capacity);
            }

            var data = _dbContext.SchoolDb.GetList();

            return _dbContext.SchoolDb.GetList(predicate);
        }

        /// <summary>
        /// 查询第一条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("FirstOrDefault")]
        public School FirstOrDefault(int id)
        {
            //linqkit 语法糖
            var predicate = PredicateBuilder.New<School>(true);
            predicate.And(t => t.Id == id);

            //_dbContext.SchoolDb.GetById(id);   //此种语句试用与id是主键并且自增的地方

            //sqlsugar 语法

            return _dbContext.SchoolDb.GetSingle(it => it.Id == 1);//根据条件查询一条 如果只是用id查询的话，建议用上方方法
        }
    }
}