using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterBillingApp.Model
{
    public class ApiResponse<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int Count { get; set; }
        public bool HasNext { get; set; }
    }


}
