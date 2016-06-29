using Esri.ArcGISRuntime.Controls;
using GalaSoft.MvvmLight.Messaging;
using Hamburger.UI.Messages;
using Hamburger.UI.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamburger.UI.ViewModels
{
    public class MapViewModel : INotifyPropertyChanged
    {

        public IMapView MapView { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        
      
        private void RaisePropertyChanged(String info)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(info));
        }

        private bool _isToolboxVisible = false;
        public bool IsToolboxVisible
        {
            get
            {
                return _isToolboxVisible;
            }
            set
            {
                _isToolboxVisible = value;
                RaisePropertyChanged("IsToolboxVisible");
            }
        }

        private bool _isScalebarVisible = true;
        public bool IsScalebarVisible
        {
            get
            {
                return _isScalebarVisible;
            }
            set
            {
                _isScalebarVisible = value;
                RaisePropertyChanged("IsScalebarVisible");
            }
        }

        public MapViewModel()
        {
            Messenger.Default.Register<PaintModeMessage>(this, HandlePaintModeMessage);
            Messenger.Default.Register<ScalebarModeMessage>(this, HandleScalebarModeMessage);
            Messenger.Default.Register<JumpToPointMessage>(this, HandleJumpToPointMessage);
        }

        private void HandleScalebarModeMessage(ScalebarModeMessage message)
        {
            IsScalebarVisible = (message.IsVisible == true);
        }

        private void HandleJumpToPointMessage(JumpToPointMessage message)
        {
            MapView.JumpToPoint(message.Point);
        }

        private void HandlePaintModeMessage(PaintModeMessage message)
        {
            IsToolboxVisible = (message.IsOn == true);
        }
    }
}
