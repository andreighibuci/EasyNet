using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyNet.Model
{
   public class ConnectedDevice
    {
        int id { get; set; }
      public  PC pc { get; set; }
        public Router router { get; set; }
       public Switch switcher { get; set; }
    }
}
