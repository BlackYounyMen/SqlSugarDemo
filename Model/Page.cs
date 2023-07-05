using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Page<T> where T : class
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public List<T> data { get; set; }
    }
}