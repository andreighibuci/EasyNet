using EasyNet.Core;
using EasyNet.Model;
using EasyNet.Windows;
using FontAwesome.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace EasyNet.ViewModels
{
    public class MainWindowViewModel
    {
        public static int id = 1;
        public static List<PC> pcList = new List<PC>();
        public static List<Router> routerList = new List<Router>();
        public static List<Switch> switchList = new List<Switch>();
        public ICommand PcClick { get; set; }
        public ICommand RouterClick { get; set; }
        public ICommand SwitchClick { get; set; }
        public ICommand DeleteClick { get; set; }
        public ICommand ValidateClick { get; set; }



        public ImageAwesome _PcImage;
        public ImageAwesome _RouterImage;
        public ImageAwesome _SwitchImage;
        public MainWindowViewModel()
        {
            PcClick = new RelayCommand(PcClickAction);
            RouterClick = new RelayCommand(RouterClickAction);
            SwitchClick = new RelayCommand(SwitchClickAction);
            DeleteClick = new RelayCommand(DeleteClickAction);
            ValidateClick = new RelayCommand(ValidateClickAction);
        }

        private void ValidateClickAction(object obj)
        {
            bool isValidIPNotEmpty = true;
            bool isValidGateway = true;
            bool isinRange = true;
            string pcwithEmptyIP = "";
            string pcwithWrongGateway = "";
            string pcNotInRages = "";
            foreach (PC pc in pcList)
            {
                if (pc.IP == "")
                {
                    isValidIPNotEmpty = false;
                    pcwithEmptyIP += pc.name + " ";
                }
            }

            foreach (Router router in routerList)
            {

                foreach (Port port in router.ports)
                {
                    if (port.connections != null)
                    {
                        if (port.connections.pc != null)
                        {
                            if (port.connections.pc.Gateway.Trim() != router.IP.Trim())
                            {
                                isValidGateway = false;
                                pcwithWrongGateway += port.connections.pc.name + " ";

                            }

                            string finalIp = port.connections.pc.IP.Substring(port.connections.pc.IP.LastIndexOf(".") + 1, 3);
                            string[] Ranges = router.RangeIp.Split("-");
                            string minRange = Ranges[0];
                            string maxRange = Ranges[1];

                            if (Int32.Parse(finalIp) < Int32.Parse(minRange) || Int32.Parse(finalIp) > Int32.Parse(maxRange))
                            {
                                isinRange = false;
                                pcNotInRages += port.connections.pc.name + " ";
                            }
                        }
                    }


                }


            }
            bool routerFound = true;
            string switcheswithoutrouters = "";
            foreach (Switch switcher in switchList)
            {
                bool routerFoundforswitch = false;
                foreach (Port port in switcher.ports)
                {
                    if (port.connections != null)
                    {
                        if (port.connections.router != null)
                        {
                            routerFoundforswitch = true;
                            foreach (Port portWithPc in switcher.ports)
                            {
                                if(portWithPc.connections != null)
                                if (portWithPc.connections.pc != null)
                                {
                                    if (portWithPc.connections.pc.Gateway.Trim() != port.connections.router.IP.Trim())
                                    {
                                        isValidGateway = false;
                                        pcwithWrongGateway += portWithPc.connections.pc.name + " ";

                                    }

                                    string finalIp = portWithPc.connections.pc.IP.Substring(portWithPc.connections.pc.IP.LastIndexOf(".") + 1, 3);
                                    string[] Ranges = port.connections.router.RangeIp.Split("-");
                                    string minRange = Ranges[0];
                                    string maxRange = Ranges[1];

                                    if (Int32.Parse(finalIp) < Int32.Parse(minRange) || Int32.Parse(finalIp) > Int32.Parse(maxRange))
                                    {
                                        isinRange = false;
                                        pcNotInRages += portWithPc.connections.pc.name + " ";
                                    }
                                }
                            }
                        }
                    }


                }

                if(routerFoundforswitch == false)
                {
                    routerFound = false;
                    switcheswithoutrouters += switcher.name + " ";
                }


            }

            if (isValidIPNotEmpty == false)
            {
                Helper.validationContent.Foreground = new SolidColorBrush(Colors.Red);
                Helper.validationContent.FontSize = 9;
                Helper.validationContent.Content = "Not Valid Connection";
                MessageBox.Show("Following pcs IP is empty: " + pcwithEmptyIP + "");
            }

            if (isValidGateway == false)
            {
                Helper.validationContent.Foreground = new SolidColorBrush(Colors.Red);
                Helper.validationContent.FontSize = 9;
                Helper.validationContent.Content = "Not Valid Connection";
                MessageBox.Show("Following pcs Gateway is wrong: " + pcwithWrongGateway + "");
            }


            if (isinRange == false)
            {
                Helper.validationContent.Foreground = new SolidColorBrush(Colors.Red);
                Helper.validationContent.FontSize = 9;
                Helper.validationContent.Content = "Not Valid Connection";
                MessageBox.Show("Following pcs ips range is wrong:"  + pcNotInRages);
            }

            if(routerFound == false)
            {
                Helper.validationContent.Foreground = new SolidColorBrush(Colors.Red);
                Helper.validationContent.FontSize = 9;
                Helper.validationContent.Content = "Not Valid Connection";
                MessageBox.Show("Following switches have no router assigned: " + switcheswithoutrouters);
            }


            if(isinRange && routerFound && isValidGateway && isValidIPNotEmpty)
            {
                Helper.validationContent.Foreground = new SolidColorBrush(Colors.Green);
                Helper.validationContent.FontSize = 9;
                Helper.validationContent.Content = "Valid Connection";
            }
        }

        private void DeleteClickAction(object obj)
        {
            Helper.networkSheet.Children.Clear();
            id = 1;
            pcList = new List<PC>();
            routerList = new List<Router>();
            switchList = new List<Switch>();
            Helper.validationContent.Content = "";
        }


        private void PcClickAction(object obj)
        {
            FontAwesome.WPF.ImageAwesome imageAwesome = new ImageAwesome();
            imageAwesome.Icon = FontAwesomeIcon.Desktop;
            imageAwesome.Height = 20;
            imageAwesome.Name = "elem" + id.ToString();

            PC pc = new PC();
            pc.id = id;
            pcList.Add(pc);

            _PcImage = imageAwesome;
            _PcImage.MouseLeftButtonDown += Element_MouseLeftButtonDown;
            _PcImage.MouseLeftButtonUp += Element_MouseLeftButtonUp;
            _PcImage.MouseMove += Element_MouseMove;
            _PcImage.MouseLeftButtonDown += PCElement_DoubleClick;
            _PcImage.MouseRightButtonDown += Element_RightMouseDown;
            _PcImage.MouseRightButtonUp += Element_RightMouseUp;

            Helper.networkSheet.Children.Add(_PcImage);
            id++;
        }
        private void RouterClickAction(object obj)
        {
            FontAwesome.WPF.ImageAwesome imageAwesome = new ImageAwesome();
            imageAwesome.Icon = FontAwesomeIcon.Wifi;
            imageAwesome.Height = 20;
            imageAwesome.Name = "elem" + id.ToString();


            Router router = new Router();
            router.ports = new List<Port>() { new Port(), new Port(), new Port(), new Port() };
            router.id = id;
            routerList.Add(router);

            _RouterImage = imageAwesome;
            _RouterImage.MouseLeftButtonDown += Element_MouseLeftButtonDown;
            _RouterImage.MouseLeftButtonUp += Element_MouseLeftButtonUp;
            _RouterImage.MouseMove += Element_MouseMove;
            _RouterImage.MouseLeftButtonDown += RouterElement_DoubleClick;
            _RouterImage.MouseRightButtonDown += Element_RightMouseDown;
            _RouterImage.MouseRightButtonUp += Element_RightMouseUp;
            Helper.networkSheet.Children.Add(_RouterImage);
            id++;
        }
        private void SwitchClickAction(object obj)
        {

            FontAwesome.WPF.ImageAwesome imageAwesome = new ImageAwesome();
            imageAwesome.Icon = FontAwesomeIcon.ObjectGroup;
            imageAwesome.Height = 20;
            imageAwesome.Name = "elem" + id.ToString();
            Switch switcher = new Switch();
            switcher.ports = new List<Port>() { new Port(), new Port(), new Port(), new Port(), new Port(), new Port() };
            switcher.id = id;
            switchList.Add(switcher);

            _SwitchImage = imageAwesome;
            _SwitchImage.MouseLeftButtonDown += Element_MouseLeftButtonDown;
            _SwitchImage.MouseLeftButtonUp += Element_MouseLeftButtonUp;
            _SwitchImage.MouseMove += Element_MouseMove;
            _SwitchImage.MouseLeftButtonDown += SwitchElement_DoubleClick;
            _SwitchImage.MouseRightButtonDown += Element_RightMouseDown;
            _SwitchImage.MouseRightButtonUp += Element_RightMouseUp;
            Helper.networkSheet.Children.Add(_SwitchImage);
            id++;
        }

        private Point clickPosition;
        private Line line = new Line();

        private PC retainPC;
        private Router retainRouter;
        private Switch retainSwitch;
        private void Element_RightMouseDown(object sender, MouseButtonEventArgs e)
        {
            retainRouter = null;
            retainPC = null;
            retainSwitch = null;

            if (Helper.checkIfPCType((sender as ImageAwesome).Name, pcList))
            {
            

                foreach (PC pc in pcList)
                {
                    if ((sender as ImageAwesome).Name.Contains(pc.id.ToString()))
                    {
                        if (pc.port == null)
                        {
                            pc.port = new Port();
                            retainPC = pc;
                            line = new Line();
                            line.Visibility = System.Windows.Visibility.Visible;
                            line.StrokeThickness = 4;
                            line.Stroke = System.Windows.Media.Brushes.Black;
                            Point currentPosition = e.GetPosition(Helper.networkSheet as UIElement);

                            line.X1 = currentPosition.X;
                            line.Y1 = currentPosition.Y;
                        }
                        else {
                            retainPC = null;
                            MessageBox.Show("The PC " + pc.name + " has all the ports occupied"); }


                    }
                }


            } else if (Helper.checkIfRouterType((sender as ImageAwesome).Name, routerList))
            {
                foreach (Router router in routerList)
                {
                    bool allportsOccupied = true;
                    for (int i = 0; i < 4; i++)
                    {
                        if (router.ports[i].connections == null)
                        {
                            allportsOccupied = false;
                            router.ports[i] = new Port();
                            retainRouter = router;
                            line = new Line();
                            line.Visibility = System.Windows.Visibility.Visible;
                            line.StrokeThickness = 4;
                            line.Stroke = System.Windows.Media.Brushes.Black;
                            Point currentPosition = e.GetPosition(Helper.networkSheet as UIElement);

                            line.X1 = currentPosition.X;
                            line.Y1 = currentPosition.Y;
                            break;

                        }

                    }

                    if (allportsOccupied)
                    {
                        retainRouter = null;
                        MessageBox.Show("The router " + router.name + " has all the ports occupied");

                    }
                }
            }
            else if (Helper.checkIfSwitchType((sender as ImageAwesome).Name, switchList))
            {
                foreach (Switch switcher in switchList)
                {
                    bool allportsOccupied = true;
                    for (int i = 0; i < 6; i++)
                    {
                        if (switcher.ports[i].connections == null)
                        {
                            allportsOccupied = false;
                            switcher.ports[i] = new Port();
                            retainSwitch = switcher;
                            line = new Line();
                            line.Visibility = System.Windows.Visibility.Visible;
                            line.StrokeThickness = 4;
                            line.Stroke = System.Windows.Media.Brushes.Black;
                            Point currentPosition = e.GetPosition(Helper.networkSheet as UIElement);

                            line.X1 = currentPosition.X;
                            line.Y1 = currentPosition.Y;
                            break;

                        }

                    }

                    if (allportsOccupied)
                    {
                        retainSwitch = null;
                        MessageBox.Show("The switcher " + switcher.name + " has all the ports occupied");

                    }
                }
            }


        }

        private void Element_RightMouseUp(object sender, MouseButtonEventArgs e)
        {
            Point currentPosition = e.GetPosition(Helper.networkSheet as UIElement);
            if (Helper.checkIfPCType((sender as ImageAwesome).Name, pcList))
            {
                foreach (PC pc in pcList)
                {
                    if ((sender as ImageAwesome).Name.Contains(pc.id.ToString()))
                    {
                        if (pc.port == null)
                        {
                            pc.port = new Port();
                            pc.port.connections = new ConnectedDevice();
                            if (retainPC != null)
                            {
                                retainPC.port.connections.pc = pc;
                                pc.port.connections.pc = retainPC;
                                line.X2 = currentPosition.X;
                                line.Y2 = currentPosition.Y;
                            }else if(retainRouter != null)
                            {
                                foreach (Port port in retainRouter.ports)
                                {
                                    if (port.connections == null)
                                    {
                                        port.connections = new ConnectedDevice();
                                        port.connections.pc = pc;
                                        pc.port = new Port();
                                        pc.port.connections = new ConnectedDevice();
                                        pc.port.connections.router = retainRouter;
                                        line.X2 = currentPosition.X;
                                        line.Y2 = currentPosition.Y;

                                        Helper.networkSheet.Children.Remove(line);
                                        Helper.networkSheet.Children.Add(line);
                                        break;
                                    }
                                }
                            }
                            else if (retainSwitch != null)
                            {
                                foreach (Port port in retainSwitch.ports)
                                {
                                    if (port.connections == null)
                                    {
                                        port.connections = new ConnectedDevice();
                                        port.connections.pc = pc;
                                        pc.port = new Port();
                                        pc.port.connections = new ConnectedDevice();
                                        pc.port.connections.switcher = retainSwitch;
                                        line.X2 = currentPosition.X;
                                        line.Y2 = currentPosition.Y;

                                        Helper.networkSheet.Children.Remove(line);
                                        Helper.networkSheet.Children.Add(line);
                                        break;
                                    }
                                }
                            }


                        }
                        else
                        {
                          
                            MessageBox.Show("The PC " + pc.name + " has all the ports occupied");
                        }


                    }
                }
            }
            else if (Helper.checkIfRouterType((sender as ImageAwesome).Name, routerList))
            {
                foreach (Router router in routerList)
                {
                    if ((sender as ImageAwesome).Name.Contains(router.id.ToString()))
                    {
                        bool allportsOccupied = true;
                        for (int i = 0; i < 4; i++)
                        {
                            if (router.ports[i].connections == null)
                            {
                                allportsOccupied = false;


                                if (retainSwitch != null)
                                {
                                    foreach (Port port in retainSwitch.ports)
                                    {
                                        if (port.connections == null)
                                        {
                                            port.connections = new ConnectedDevice();
                                            port.connections.router = router;
                                            router.ports[i] = new Port();
                                            router.ports[i].connections = new ConnectedDevice();
                                            router.ports[i].connections.switcher = retainSwitch;
                                            line.X2 = currentPosition.X;
                                            line.Y2 = currentPosition.Y;

                                            Helper.networkSheet.Children.Remove(line);
                                            Helper.networkSheet.Children.Add(line);
                                            break;
                                        }
                                    }
                                }
                                else if (retainPC != null)
                                {


                                    if (retainPC.port.connections == null)
                                    {
                                        retainPC.port.connections = new ConnectedDevice();
                                        retainPC.port.connections.router = router;
                                        router.ports[i] = new Port();
                                        router.ports[i].connections = new ConnectedDevice();
                                        router.ports[i].connections.pc = retainPC;
                                        line.X2 = currentPosition.X;
                                        line.Y2 = currentPosition.Y;

                                        Helper.networkSheet.Children.Remove(line);
                                        Helper.networkSheet.Children.Add(line);
                                        break;
                                    }

                                }


                                break;


                            }

                        }

                        if (allportsOccupied)
                        {
                            MessageBox.Show("The router " + router.name + " has all the ports occupied");

                        }


                    }
                }
            }
            else if (Helper.checkIfSwitchType((sender as ImageAwesome).Name, switchList))
            {
                foreach (Switch switcher in switchList)
                {
                    if ((sender as ImageAwesome).Name.Contains(switcher.id.ToString()))
                    {
                        bool allportsOccupied = true;
                        for (int i = 0; i < 6; i++)
                        {
                            if (switcher.ports[i].connections == null)
                            {
                                allportsOccupied = false;


                                if (retainRouter != null)
                                {
                                    foreach (Port port in retainRouter.ports)
                                    {
                                        if (port.connections == null)
                                        {
                                            port.connections = new ConnectedDevice();
                                            port.connections.switcher = switcher;
                                            switcher.ports[i] = new Port();
                                            switcher.ports[i].connections = new ConnectedDevice();
                                            switcher.ports[i].connections.router = retainRouter;
                                            line.X2 = currentPosition.X;
                                            line.Y2 = currentPosition.Y;

                                            Helper.networkSheet.Children.Remove(line);
                                            Helper.networkSheet.Children.Add(line);
                                            break;
                                        }
                                    }
                                }else if(retainPC != null)
                                {
                                   
                                        
                                            if (retainPC.port.connections == null)
                                            {
                                                retainPC.port.connections = new ConnectedDevice();
                                                retainPC.port.connections.switcher = switcher;
                                                switcher.ports[i] = new Port();
                                                switcher.ports[i].connections = new ConnectedDevice();
                                                switcher.ports[i].connections.pc = retainPC;
                                                line.X2 = currentPosition.X;
                                                line.Y2 = currentPosition.Y;

                                                Helper.networkSheet.Children.Remove(line);
                                                Helper.networkSheet.Children.Add(line);
                                                break;
                                           }
                                    
                                }


                                break;

                            }

                        }

                        if (allportsOccupied)
                        {

                 
                            MessageBox.Show("The switcher " + switcher.name + " has all the ports occupied");

                        }


                    }
                }



            }
    }
    

        private void PCElement_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                foreach (PC pc in pcList)
                {
                    if ((sender as ImageAwesome).Name.Contains( pc.id.ToString()))
                    {
                        Helper.calledPc = pc;
                        PCEditWindow pcedit = new PCEditWindow();
                        pcedit.Show();
                    }
                }
            }
        }

        private void RouterElement_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                foreach (Router router in routerList)
                {
                    if ((sender as ImageAwesome).Name.Contains(router.id.ToString()))
                    {
                        Helper.calledRouter = router;
                        RouterEdit routeredit = new RouterEdit();
                        routeredit.Show();
                    }
                }
            }
        }

        private void SwitchElement_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                foreach (Switch switcher in switchList)
                {
                    if ((sender as ImageAwesome).Name.Contains(switcher.id.ToString()))
                    {
                        Helper.calledSwitch = switcher;
                        SwitchEdit switchedit = new SwitchEdit();
                        switchedit.Show();
                    }
                }
            }
        }
        private void Element_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var draggableControl = sender as UIElement;
            clickPosition = e.GetPosition(Helper.networkSheet);
            draggableControl.CaptureMouse();
        }

        private void Element_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var draggableControl = sender as UIElement;
            
            draggableControl.ReleaseMouseCapture();
        }

        private void Element_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point currentPosition = e.GetPosition(Helper.networkSheet as UIElement);
                var draggablecontrol = sender as UIElement;
                var transform = draggablecontrol.RenderTransform as TranslateTransform;
                if (transform == null)
                {
                    transform = new TranslateTransform();
                    draggablecontrol.RenderTransform = transform;
                }
                transform.X = currentPosition.X - clickPosition.X;
                transform.Y = currentPosition.Y - clickPosition.Y;
            }

        }

    }


}

