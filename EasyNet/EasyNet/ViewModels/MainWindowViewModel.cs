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



        public ImageAwesome _PcImage;
        public ImageAwesome _RouterImage;
        public ImageAwesome _SwitchImage;
        public MainWindowViewModel()
        {
            PcClick = new RelayCommand(PcClickAction);
            RouterClick = new RelayCommand(RouterClickAction);
            SwitchClick = new RelayCommand(SwitchClickAction);
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
            imageAwesome.Name = "elem"+id.ToString();


            Router router = new Router();
            router.id = id;
            routerList.Add(router);

            _RouterImage = imageAwesome;
            _RouterImage.MouseLeftButtonDown += Element_MouseLeftButtonDown;
            _RouterImage.MouseLeftButtonUp += Element_MouseLeftButtonUp;
            _RouterImage.MouseMove += Element_MouseMove;
            _RouterImage.MouseLeftButtonDown += RouterElement_DoubleClick;

            Helper.networkSheet.Children.Add(_RouterImage);
            id++;
        }
        private void SwitchClickAction(object obj)
        {

            FontAwesome.WPF.ImageAwesome imageAwesome = new ImageAwesome();
            imageAwesome.Icon = FontAwesomeIcon.ObjectGroup;
            imageAwesome.Height = 20;
            imageAwesome.Name = "elem"+id.ToString();

            _SwitchImage = imageAwesome;
            _SwitchImage.MouseLeftButtonDown += Element_MouseLeftButtonDown;
            _SwitchImage.MouseLeftButtonUp += Element_MouseLeftButtonUp;
            _SwitchImage.MouseMove += Element_MouseMove;

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

            if(Helper.checkIfPCType((sender as ImageAwesome).Name, pcList))
            {
                foreach (PC pc in pcList)
                {
                    if ((sender as ImageAwesome).Name.Contains(pc.id.ToString()))
                    {
                        if (pc.port == null)
                        {
                            pc.port = new Port();
                            pc.port.connections = new ConnectedDevice();
                            retainPC = pc;
                            line = new Line();
                            line.Visibility = System.Windows.Visibility.Visible;
                            line.StrokeThickness = 4;
                            line.Stroke = System.Windows.Media.Brushes.Black;
                            Point currentPosition = e.GetPosition(Helper.networkSheet as UIElement);

                            line.X1 = currentPosition.X;
                            line.Y1 = currentPosition.Y;
                        }
                        else { MessageBox.Show("The PC " + pc.name + " has all the ports occupied"); }
                        

                    }
                }
            }
            else
            {

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
                            retainPC.port.connections.pc = pc;
                            pc.port.connections.pc = retainPC;
                            line.X2 = currentPosition.X;
                            line.Y2 = currentPosition.Y;

                            Helper.networkSheet.Children.Add(line);

                        }
                        else {
                            retainPC.port = null;
                            MessageBox.Show("The PC " + pc.name + " has all the ports occupied"); }


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

