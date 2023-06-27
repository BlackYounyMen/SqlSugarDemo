using SqlSugar;
using System;

namespace Model
{
    [SugarTable("School")]//当和数据库名称不一致时候可以设置别名
    public class School
    {
        /// <summary>
        /// 学校的唯一标识符，主键且自增。
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)] //通过特性设置主键和自增列
        public int Id { get; set; }

        /// <summary>
        /// 学校名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 学校所在地
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 学校的容量
        /// </summary>
        public int Capacity { get; set; }

        /// <summary>
        /// 学校的校长
        /// </summary>
        public string Principal { get; set; }

        /// <summary>
        /// 学校成立日期
        /// </summary>
        public DateTime FoundedDate { get; set; }
    }
}