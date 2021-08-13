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

        public ImageAwesome _imageAwesome;
        public MainWindowViewModel()
        {
            PcClick = new RelayCommand(PcClickAction);
        }

        private void PcClickAction(object obj)
        {
            FontAwesome.WPF.ImageAwesome imageAwesome = new ImageAwesome();
            imageAwesome.Icon = FontAwesomeIcon.Desktop;
            imageAwesome.Height = 20;
            _imageAwesome = imageAwesome;
            _imageAwesome.MouseLeftButtonDown += Element_MouseLeftButtonDown;
            _imageAwesome.MouseLeftButtonUp += Element_MouseLeftButtonUp;
            _imageAwesome.MouseMove += Element_MouseMove;

            Helper.networkSheet.Children.Add(_imageAwesome);
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

