using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.DataLayer.Entities
{
    public class OrderTroubleshoot
    {
        public long Error_id { get; set; }
        public string summary { get; set; }
        public long iinc_id { get; set; }
        public string solution { get; set; }
    }
}
