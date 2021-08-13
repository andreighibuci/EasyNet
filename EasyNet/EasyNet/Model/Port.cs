using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyNet.Model
{
    class Port
    {
        int id { get; set; }
        int portNumber { get; set; }
        ConnectedDevice connections { get; set; }
    }
}
