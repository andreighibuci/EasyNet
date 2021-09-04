using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyNet.Model
{
    public class Port
    {
        int id { get; set; }
       public ConnectedDevice connections { get; set; }
    }
}
