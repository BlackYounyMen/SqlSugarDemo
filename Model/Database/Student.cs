using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model
{
    [SugarTable("Student")]//当和数据库名称不一致时候可以设置别名
    public class Student
    {
        [SugarColumn(IsPrimaryKey = true, ColumnDataType = "bigint")] //通过特性设置主键和自增列  （注：DataBase不能使用 int）
        public long Id { get; set; }

        public string Name { get; set; }
        public int Age { get; set; }

        public string Sex { get; set; }
    }
}