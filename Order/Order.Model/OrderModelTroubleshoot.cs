using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Model
{
    public class OrderModelTroubleshoot
    {
        public long IncriptionId { get; set; }
        public long ErrorId { get; set; }
        public string Summary { get; set; }
        public string Solution { get; set; }
    }
}
