using Hamburger.UI.Models;
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
        }

        private void initializeDummyRestraunts()
        {
            Restraunts = new List<RestrauntModel>() { new RestrauntModel { Name = "Mex&Co" }, new RestrauntModel { Name = "Segev" }, new RestrauntModel { Name = "Gordos" },
                                                     new RestrauntModel {Name = "Humangous" } , new RestrauntModel {Name="Blondie" }, new RestrauntModel {Name="MeatNight" } };
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
            }
        }
    }
}
