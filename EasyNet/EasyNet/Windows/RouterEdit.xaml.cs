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
