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
    /// Interaction logic for PCEditWindow.xaml
    /// </summary>
    public partial class PCEditWindow : Window
    {
        public PCEditWindow()
        {
            InitializeComponent();
            if(Helper.calledPc.name != null)
            {
                NameTB.Text = Helper.calledPc.name;
            }

            if(Helper.calledPc.IP != null)
            {
                IPTB.Text = Helper.calledPc.IP;
            }

            if (Helper.calledPc.Gateway != null)
            {
                GatewayTB.Text = Helper.calledPc.Gateway;
            }

            if (Helper.calledPc.port!= null)
            {
                if (Helper.calledPc.port.connections.pc != null)
                    PortTB.Text = Helper.calledPc.port.connections.pc.name;

                if (Helper.calledPc.port.connections.router != null)
                    PortTB.Text = Helper.calledPc.port.connections.router.name;
               
                if (Helper.calledPc.port.connections.switcher != null)
                    PortTB.Text = Helper.calledPc.port.connections.switcher.name;

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Helper.calledPc.name = NameTB.Text;
            Helper.calledPc.IP = IPTB.Text;
            Helper.calledPc.Gateway = GatewayTB.Text;
            this.Close();
        }
    }
}
