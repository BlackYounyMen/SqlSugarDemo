using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositorySQLSugar.Base
{
    /// <summary>
    /// SqlSugar 自带的仓储模式
    /// </summary>
    public class IRepository : SimpleClient<T> where T : class, new()
    {
    }
}