using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyNet.Model
{
    class ConnectedDevice
    {
        int id { get; set; }
        PC pc { get; set; }
        Router router { get; set; }
        Switch switcher { get; set; }
    }
}
