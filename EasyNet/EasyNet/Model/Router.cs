using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyNet.Model
{
    public class Router
    {
      public  int id { get; set; }
      public String name { get; set; }
      public  String IP { get; set; }
      public  String RangeIp { get; set; }
      public  List<Port> ports { get; set; }

        public Router()
        {
            this.IP = "";
            this.RangeIp = "0-0";
        }
    }
}
