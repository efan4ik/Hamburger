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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Hamburger.UI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            MainFrame.Navigate(typeof(Views.MapView));
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


        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            var page = (e.OriginalSource as Button).CommandParameter.ToString();
            if (page == "Map")
            {
                MainFrame.Navigate(typeof(Views.MapView));
            }
            else if (page == "Text")
            {
                MainFrame.Navigate(typeof(TextView));
            }
            else if (page == "Image")
            {
                MainFrame.Navigate(typeof(ImageView));
            }
        }

        private void LeftHamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            //LeftPanel.IsPaneOpen = !LeftPanel.IsPaneOpen;
        }

        private void RightHamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            //RightPanel.IsPaneOpen = !RightPanel.IsPaneOpen;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}
