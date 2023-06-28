using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.CrossQuery
{
    [Tenant("db1")] //实体标为db1
    public class Order
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        public string Name { get; set; }
        public decimal Price { get; set; }

        [SugarColumn(IsNullable = true)]
        public DateTime CreateTime { get; set; }

        [SugarColumn(IsNullable = true)]
        public int CustomId { get; set; }

        [Navigate(NavigateType.OneToMany, nameof(OrderItem.OrderId))]//
        public List<OrderItem> Items { get; set; }
    }
}