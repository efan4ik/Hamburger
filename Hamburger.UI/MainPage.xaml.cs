using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Esri.ArcGISRuntime.Controls;
using Hamburger.UI.Views;
using Hamburger.UI.Models;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Hamburger.UI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public List<RestrauntModel> Restraunts { get; set; }
        public List<BarModel> Bars { get; set; }

        public MainPage()
        {
            DataContext = this;
            initializeDummyBars();
            initializeDummyRestraunts();
            InitializeComponent();
            _views = new Dictionary<string, Page>()
            {
                 {"MapView", new Views.MapView()},
                {"TextView", new TextView()},
                {"ImageView", new ImageView()}
            };
            Navigate("MapView");
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
              

        #region properties
        //private Map _map;

        ///// <summary>
        ///// Gets the Map rendered in the MapView
        ///// </summary>
        //public Map Map
        //{
        //    get
        //    {
        //        if (_map == null)
        //        {
        //            _map = new Map(Basemap.CreateTopographic());
        //        }
        //        return _map;
        //    }
        //}

        #endregion

        private Dictionary<string, Page> _views;
        public Dictionary<string, Page> Views
        {
            get { return _views; }
            set
            {
                _views = value;

            }
        }
        

        private void Navigate(string viewName)
        {
            if (Views.ContainsKey(viewName))
            {
                MainFrame.Content = Views[viewName];
            }
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            var viewName = (e.OriginalSource as ToggleButton).CommandParameter.ToString();
            Navigate(viewName);
        }

        private void LeftHamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            LeftPanel.IsPaneOpen = !LeftPanel.IsPaneOpen;
        }

        private void RightHamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            RightPanel.IsPaneOpen = !RightPanel.IsPaneOpen;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (MapListItem.IsSelected)
            //    CurrentFrame = "MapFrame";
            //else if (TextListItem.IsSelected)
            //    CurrentFrame = "TextFrame";
            //else if (ImageListItem.IsSelected)
            //    CurrentFrame = "ImageFrame";
        }

        private void TopPanelListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Views == null)
                return;

            if (MapListItem.IsSelected)
                Navigate("MapView");
            else if (TextListItem.IsSelected)
                Navigate("TextView");
            else if (ImageListItem.IsSelected)
                Navigate("ImageView");
        }
    }
}

