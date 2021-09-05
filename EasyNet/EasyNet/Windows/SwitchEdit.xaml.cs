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
    /// Interaction logic for SwitchEdit.xaml
    /// </summary>
    public partial class SwitchEdit : Window
    {
        public SwitchEdit()
        {
            InitializeComponent();
            if (Helper.calledSwitch.name != null)
            {
                NameTB.Text = Helper.calledSwitch.name;
            }

           

            if (Helper.calledSwitch.ports[0].connections != null)
            {
                if(Helper.calledSwitch.ports[0].connections.pc != null)
                    Port4TB.Text = Helper.calledSwitch.ports[0].connections.pc.name;
                else if(Helper.calledSwitch.ports[0].connections.router != null)
                    Port4TB.Text = Helper.calledSwitch.ports[0].connections.router.name;
            }

            if (Helper.calledSwitch.ports[1].connections != null)
            {
                if (Helper.calledSwitch.ports[1].connections.pc != null)
                    PortTB.Text = Helper.calledSwitch.ports[1].connections.pc.name;
                else if (Helper.calledSwitch.ports[1].connections.router != null)
                    PortTB.Text = Helper.calledSwitch.ports[1].connections.router.name;
            }

            if (Helper.calledSwitch.ports[2].connections != null)
            {
                if (Helper.calledSwitch.ports[2].connections.pc != null)
                    Port2TB.Text = Helper.calledSwitch.ports[2].connections.pc.name;
                else if (Helper.calledSwitch.ports[2].connections.router != null)
                    Port2TB.Text = Helper.calledSwitch.ports[2].connections.router.name;
            }

            if (Helper.calledSwitch.ports[3].connections != null)
            {
                if (Helper.calledSwitch.ports[3].connections.pc != null)
                    Port3TB.Text = Helper.calledSwitch.ports[3].connections.pc.name;
                else if (Helper.calledSwitch.ports[3].connections.router != null)
                    Port3TB.Text = Helper.calledSwitch.ports[3].connections.router.name;
            }

            if (Helper.calledSwitch.ports[4].connections != null)
            {
                if (Helper.calledSwitch.ports[4].connections.pc != null)
                    Port5.Text = Helper.calledSwitch.ports[4].connections.pc.name;
                else if (Helper.calledSwitch.ports[4].connections.router != null)
                    Port5.Text = Helper.calledSwitch.ports[4].connections.router.name;
            }

            if (Helper.calledSwitch.ports[5].connections != null)
            {
                if (Helper.calledSwitch.ports[5].connections.pc != null)
                    Port6.Text = Helper.calledSwitch.ports[5].connections.pc.name;
                else if (Helper.calledSwitch.ports[5].connections.router != null)
                    Port6.Text = Helper.calledSwitch.ports[5].connections.router.name;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Helper.calledSwitch.name = NameTB.Text;
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

