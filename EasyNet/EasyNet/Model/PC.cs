﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyNet.Model
{
    public class PC
    {
       public int id { get; set; }
       public String name { get; set; }
       public String IP { get; set; }
       public String Gateway { get; set; }
       public Port port{ get; set; }

        public PC()
        {
            this.Gateway = "";
            this.IP = "";
        }
    }
}
