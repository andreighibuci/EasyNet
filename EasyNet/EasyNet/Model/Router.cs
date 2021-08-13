using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyNet.Model
{
    class Router
    {
        int id { get; set; }
        String name { get; set; }
        String IP { get; set; }
        String RangeIp { get; set; }
        List<Port> ports { get; set; }
    }
}
