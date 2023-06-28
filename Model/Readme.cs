using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    internal class Readme
    {
        #region 语法

        //[[Tenant("db2")] ] :  用于指定其他数据库 

        //[SugarColumn]: 用于指定实体属性对应数据库表中的列名或其他属性，例如指定列名、设置主键、标识列等。

        //[SugarTable]: 用于指定实体对应的数据库表名或其他属性，例如指定表名、设置主键列等。

        //[SugarColumn(IsIgnore = true)]: 用于标记实体属性不与数据库表中的列进行映射。

        //[SugarColumn(ColumnDataType = "varchar(100)")]: 用于指定实体属性映射的数据库列的数据类型。

        //[SugarColumn(IsNullable = true)]: 用于指定实体属性对应的数据库列是否允许为空。

        //[SugarColumn(Length = 50)]: 用于指定实体属性映射的数据库列的长度限制。

        //[SugarColumn(IsPrimaryKey = true)]: 用于指定实体属性作为数据库表的主键。

        //[SugarColumn(IsIdentity = true)]: 用于指定实体属性作为数据库表的标识列。

        //[SugarColumn(DefaultValue = "default")]: 用于指定实体属性映射的数据库列的默认值。

        #endregion

    }
}
