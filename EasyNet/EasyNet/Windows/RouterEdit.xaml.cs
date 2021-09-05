using EasyNet.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EasyNet.Windows
{
    /// <summary>
    /// Interaction logic for RouterEdit.xaml
    /// </summary>
    public partial class RouterEdit : Window
    {
        public RouterEdit()
        {
            InitializeComponent();

            if (Helper.calledRouter.name != null)
            {
                NameTB.Text = Helper.calledRouter.name;
            }

            if (Helper.calledRouter.IP != null)
            {
                IPTB.Text = Helper.calledRouter.IP;
            }

            if (Helper.calledRouter.RangeIp != null)
            {
                RangeIPTB.Text = Helper.calledRouter.RangeIp;
            }

            if(Helper.calledRouter.ports[0].connections != null)
            {
                if (Helper.calledRouter.ports[0].connections.pc != null)
                    Port4TB.Text = Helper.calledRouter.ports[0].connections.pc.name;
                else if (Helper.calledRouter.ports[0].connections.switcher != null)
                    Port4TB.Text = Helper.calledRouter.ports[0].connections.switcher.name;
               
            }

            if (Helper.calledRouter.ports[1].connections != null)
            {
                if (Helper.calledRouter.ports[1].connections.pc != null)
                    PortTB.Text = Helper.calledRouter.ports[1].connections.pc.name;
                else if (Helper.calledRouter.ports[1].connections.switcher != null)
                    PortTB.Text = Helper.calledRouter.ports[1].connections.switcher.name;
            }

            if (Helper.calledRouter.ports[2].connections != null)
            {
                if (Helper.calledRouter.ports[2].connections.pc != null)
                    Port2TB.Text = Helper.calledRouter.ports[2].connections.pc.name;
                else if (Helper.calledRouter.ports[2].connections.switcher != null)
                    Port2TB.Text = Helper.calledRouter.ports[2].connections.switcher.name;
            }

            if (Helper.calledRouter.ports[3].connections != null)
            {
                if (Helper.calledRouter.ports[3].connections.pc != null)
                    Port3TB.Text = Helper.calledRouter.ports[3].connections.pc.name;
                else if (Helper.calledRouter.ports[3].connections.switcher != null)
                    Port3TB.Text = Helper.calledRouter.ports[3].connections.switcher.name;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Helper.calledRouter.name = NameTB.Text;
            Helper.calledRouter.IP = IPTB.Text;
            Helper.calledRouter.RangeIp = RangeIPTB.Text;
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
