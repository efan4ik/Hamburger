using GalaSoft.MvvmLight.Messaging;
using Hamburger.UI.Messages;
using Hamburger.UI.Models;
using Hamburger.UI.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamburger.UI.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public CommandHandler JumpToPointCommand { get; private set; }

        private List<RestrauntModel> _restraunts;
        public List<RestrauntModel> Restraunts
        {
            get { return _restraunts; }
            set { _restraunts = value; }
        }

        private List<BarModel> _bars;
        public List<BarModel> Bars
        {
            get { return _bars; }
            set { _bars = value; }
        }

        public MainPageViewModel()
        {
            initializeDummyBars();
            initializeDummyRestraunts();
            JumpToPointCommand = new CommandHandler(SendJumpToPointMessage);
        }

        private void SendJumpToPointMessage()
        {
            Messenger.Default.Send<JumpToPointMessage>(new JumpToPointMessage("32.083918, 34.772206"));
        }

        private void initializeDummyRestraunts()
        {
            Restraunts = new List<RestrauntModel>() { new RestrauntModel { Name = "Mex&Co", X = 32.083918, Y = 34.772206 }, new RestrauntModel { Name = "Segev", X = 32.110503, Y = 34.842539 }, new RestrauntModel { Name = "Gordos", X = 32.003019, Y = 34.797489 },
                                                     new RestrauntModel {Name = "Humongous", X = 32.167619, Y = 34.928926 } , new RestrauntModel {Name="Blondie", X = 31.779957, Y = 35.209074 }, new RestrauntModel {Name="MeatNight", X = 29.759694, Y = 34.970440 } };
        }

        private void initializeDummyBars()
        {
            Bars = new List<BarModel>() { new BarModel { Name = "Renato" }, new BarModel { Name = "Mitch" }, new BarModel { Name = "MikesPlace" }
                                        , new BarModel { Name = "Idea" }, new BarModel { Name = "Pow Wow" }, new BarModel { Name = "Leo Blooms" } };
        }

        private void RaisePropertyChanged(String info)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(info));
        }

        private bool? _isDrawingOn = false;
        public bool? IsDrawingOn
        {
            get
            {
                return _isDrawingOn;
            }
            set
            {
                _isDrawingOn = value;
                RaisePropertyChanged("IsDrawingOn");
                Messenger.Default.Send<PaintModeMessage>(new PaintModeMessage(_isDrawingOn));
            }
        }

        private bool? _isScalebarVisible = true;
        public bool? IsScalebarVisible
        {
            get
            {
                return _isScalebarVisible;
            }
            set
            {
                _isScalebarVisible = value;
                RaisePropertyChanged("IsScalebarVisible");
                Messenger.Default.Send<ScalebarModeMessage>(new ScalebarModeMessage(_isScalebarVisible));
            }
        }
    }
}
