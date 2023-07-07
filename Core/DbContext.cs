using Model;
using SqlSugar;
using System;
using System.Data;
using Microsoft.Extensions.DependencyInjection; // 引用 Microsoft.Extensions.DependencyInjection 命名空间，用于在 ConfigureServices 方法中注册服务
using System.Linq.Expressions;
using System.Threading.Tasks;
using Models;
using Newtonsoft.Json;

namespace Core
{
    //SqlSugarClient对象虽然但是用户还是会基础上在封装一层仓储然后简化增删查改，复杂的功能在用SqlSugarClient实现，所以集成了SimpleClient这个类用在去写额外代码。
    //具体参考  :  https://www.cnblogs.com/sunkaixuan/p/8454844.html
    public class DbContext
    {
        public SqlSugarClient Db;

        public DbContext(string configuration)
        {
            Db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = configuration,
                DbType = SqlSugar.DbType.SqlServer,
                IsAutoCloseConnection = true
            });
        }

        //用来处理事务多表查询和复杂的操作
        public DbSet<Student> StudentDb
        { get { return new DbSet<Student>(Db); } }//用来处理Student表的常用操作

        public DbSet<School> SchoolDb
        { get { return new DbSet<School>(Db); } }//用来处理School表的常用操作
    }

    //有了这个可以不在用创建Repository层
    public class DbSet<T> : SimpleClient<T> where T : class, new()
    {
        public DbSet(SqlSugarClient context) : base(context)
        {
        }

        public virtual Page<T> GetPageList(Expression<Func<T, bool>> whereExpression, Page<T> page)
        {
            int totalNumber = 0;
            page.data = Context.Queryable<T>().Where(whereExpression).ToPageList(page.PageIndex, page.PageSize, ref totalNumber);
            page.TotalCount = totalNumber;
            return page;
        }

        public virtual async Task<Page<T>> GetPageListAsync(Expression<Func<T, bool>> whereExpression, Page<T> page)
        {
            RefAsync<int> count = 0;
            page.data = await Context.Queryable<T>().Where(whereExpression).ToPageListAsync(page.PageIndex, page.PageSize, count);
            page.TotalCount = count;
            return page;
        }

        public virtual bool DeleteByIds(string ids)
        {
            var data = JsonConvert.DeserializeObject<dynamic[]>(ids);
            return Context.Deleteable<T>().In(data).ExecuteCommand() > 0;
        }

        public virtual async Task<bool> DeleteByIdsAsync(string ids)
        {
            var data = JsonConvert.DeserializeObject<dynamic[]>(ids);
            return await Context.Deleteable<T>().In(data).ExecuteCommandAsync() > 0;
        }
    }
}