using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JD.Utils.Models
{
    public class PageData<T>
    {
        public List<T> Rows { get; set; }
        public long Totals { get; set; }
    }
}
