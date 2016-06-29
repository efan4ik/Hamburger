using Hamburger.UI.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// Displays pictures of parties!

namespace Hamburger.UI.ViewModel
{
    public class PartyImageViewModel
    {
        private DispatcherTimer timer = new DispatcherTimer();
        public int timeToDisplayLocationBar = 3;
        public double transparency = 0.3;
        public bool allowClickOnLocationBar = true;
        private PartyImageModel _partyImageModel;
        public PartyImageModel partyImageModel {
            get
            {
                return _partyImageModel;
            }
            set
            {
                _partyImageModel = value;
                LoadImages();
            }
       }
        public List<object> listOfItems = new List<object>();
        private List<Image> previewPics = new List<Image>();
        private StackPanel locationBar;
        private FlipView flipView;
        private bool locationBarLoaded = false;

        public PartyImageViewModel()
        {
            timer.Tick += CollapseLocationBar;
            timer.Interval = new TimeSpan(0, 0, timeToDisplayLocationBar);
        }

        private void LoadImages ()
        {
            Image firstImage = new Image();
            Image lastImage = new Image();
            firstImage.Source = partyImageModel.Images.Last().Source;
            lastImage.Source = partyImageModel.Images.First().Source;
            listOfItems.Add(ConstructScrollViewer(firstImage));
            foreach (Image image in partyImageModel.Images)
            {
                listOfItems.Add(ConstructScrollViewer(image));
                Image previewPic = new Image();
                previewPic.Source = image.Source;
                previewPic.Opacity = transparency;
                previewPic.PointerPressed += NavigateToPic;
                previewPics.Add(previewPic);
            }
            listOfItems.Add(ConstructScrollViewer(lastImage));
        }

        internal ScrollViewer ConstructScrollViewer(Image image)
        {
            ScrollViewer scrollViewer = new ScrollViewer();
            scrollViewer.ZoomMode = ZoomMode.Enabled;
            scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            scrollViewer.Content = image;
            return scrollViewer;
        }

        public void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (locationBarLoaded)
            {
                ResetTimer();
                ManageLocationBarSelected((FlipView)sender);
                ManageCyclicalNavigation((FlipView)sender);
            }
        }

        void CollapseLocationBar(object sender, object e)
        {
            locationBar.Visibility = Visibility.Collapsed;
        }

        private void NavigateToPic(object sender, PointerRoutedEventArgs e)
        {
            if (allowClickOnLocationBar)
            {
                flipView.SelectedIndex = locationBar.Children.IndexOf((UIElement)sender) + 1;
            }
        }

        internal void ResetTimer()
        {
            timer.Stop();
            locationBar.Visibility = Visibility.Visible;
            timer.Start();
        }
        
        internal void ManageLocationBarSelected(FlipView flipview)
        {

            if (locationBar.Children.Count >= flipview.SelectedIndex && flipview.SelectedIndex > 0)
            {
                foreach (Image pic in locationBar.Children)
                {
                    pic.Opacity = transparency;
                }
                locationBar.Children.ElementAt(flipview.SelectedIndex - 1).Opacity = 1;
            }
        }
        
        internal void ManageCyclicalNavigation(FlipView flipview)
        {
            if (flipview.SelectedIndex == 0)
            {
                flipview.SelectedIndex = listOfItems.Count - 2;
            }
            else if (flipview.SelectedIndex == listOfItems.Count - 1)
            {
                flipview.SelectedIndex = 1;
            }
            if (listOfItems.Count > 3)
            {
                ((ScrollViewer)listOfItems.ElementAt(listOfItems.Count - 1)).ZoomToFactor(((ScrollViewer)listOfItems.ElementAt(1)).ZoomFactor);
                ((ScrollViewer)listOfItems.ElementAt(listOfItems.Count - 1)).InvalidateScrollInfo();
                ((ScrollViewer)listOfItems.ElementAt(0)).ZoomToFactor(((ScrollViewer)listOfItems.ElementAt(listOfItems.Count - 2)).ZoomFactor);
                ((ScrollViewer)listOfItems.ElementAt(0)).InvalidateScrollInfo();
            }
        }
        public void ResetZoom(object sender, DoubleTappedRoutedEventArgs e)
        {
            ((ScrollViewer)((FlipView)sender).Items.ElementAt(((FlipView)sender).SelectedIndex)).ZoomToFactor(1);
        }
        public void SetFirstPic(FlipView flip)
        {
            flip.SelectedIndex = 1;
            flipView = flip;
        }
        public void LoadLocationBar(StackPanel locationBar)
        {
            this.locationBar = locationBar;
            foreach(Image pic in previewPics)
            {
                locationBar.Children.Add(pic);
            }
            locationBar.Visibility = Visibility.Collapsed;
            locationBarLoaded = true;
        }
    }
}
