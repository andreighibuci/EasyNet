using EasyNet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EasyNet.Core
{
    public static class Helper
    {
        public static Grid networkSheet;

        public static PC calledPc;
        public static Router calledRouter;
        public static Switch calledSwitch;

        public static Boolean checkIfPCType(string element,List<PC> list)
        {
            foreach(PC elem in list)
            {
                if (element.Contains(elem.id.ToString()))
                {
                    return true;
                }

            }

            return false;
        }

        public static Boolean checkIfRouterType(string element, List<Router> list)
        {
            foreach (Router elem in list)
            {
                if (element.Contains(elem.id.ToString()))
                {
                    return true;
                }

            }

            return false;
        }

        public static Boolean checkIfSwitchType(string element, List<Switch> list)
        {
            foreach (Switch elem in list)
            {
                if (element.Contains(elem.id.ToString()))
                {
                    return true;
                }

            }

            return false;
        }
    }
}
