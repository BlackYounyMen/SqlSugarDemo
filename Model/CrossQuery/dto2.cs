using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.CrossQuery
{
    [Tenant("db2")] //实体标为db2
    public class OrderItem
    {
        [SqlSugar.SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int ItemId { get; set; }

        public int OrderId { get; set; }
        public decimal? Price { get; set; }

        [SqlSugar.SugarColumn(IsNullable = true)]
        public DateTime? CreateTime { get; set; }

        [Navigate(NavigateType.OneToOne, nameof(OrderId))] //设置关系 对应Order表主键
        public Order Order { get; set; }
    }
}