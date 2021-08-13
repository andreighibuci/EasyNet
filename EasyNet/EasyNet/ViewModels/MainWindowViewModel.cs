using EasyNet.Core;
using FontAwesome.WPF;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace EasyNet.ViewModels
{
    public class MainWindowViewModel
    {
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
            _PcImage = imageAwesome;
            _PcImage.MouseLeftButtonDown += Element_MouseLeftButtonDown;
            _PcImage.MouseLeftButtonUp += Element_MouseLeftButtonUp;
            _PcImage.MouseMove += Element_MouseMove;

            Helper.networkSheet.Children.Add(_PcImage);

        }
        private void RouterClickAction(object obj)
        {
            FontAwesome.WPF.ImageAwesome imageAwesome = new ImageAwesome();
            imageAwesome.Icon = FontAwesomeIcon.Wifi;
            imageAwesome.Height = 20;
            _RouterImage = imageAwesome;
            _RouterImage.MouseLeftButtonDown += Element_MouseLeftButtonDown;
            _RouterImage.MouseLeftButtonUp += Element_MouseLeftButtonUp;
            _RouterImage.MouseMove += Element_MouseMove;

            Helper.networkSheet.Children.Add(_RouterImage);
        }
        private void SwitchClickAction(object obj)
        {
            FontAwesome.WPF.ImageAwesome imageAwesome = new ImageAwesome();
            imageAwesome.Icon = FontAwesomeIcon.ObjectGroup;
            imageAwesome.Height = 20;
            _SwitchImage = imageAwesome;
            _SwitchImage.MouseLeftButtonDown += Element_MouseLeftButtonDown;
            _SwitchImage.MouseLeftButtonUp += Element_MouseLeftButtonUp;
            _SwitchImage.MouseMove += Element_MouseMove;

            Helper.networkSheet.Children.Add(_SwitchImage);
        }

        private Point clickPosition;


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

