using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// Displays pictures of parties!

namespace PartyViewer
{
    public sealed partial class PartyDisplay : UserControl
    {
        private DispatcherTimer timer = new DispatcherTimer();
        public int timeToDisplayLocationBar = 3;
        public double transparency = 0.3;
        public bool allowClickOnLocationBar = true;

        private List<Image> _Images;
        public List<Image> Images {
            get { return _Images; }
            set {
                _Images = value;
                LoadImages();
                currentIndex = 1;
            }
        }

        public int currentIndex
        {
            get
            {
                return flipView.SelectedIndex;
            }
            set
            {
                flipView.SelectedIndex = value;
            }
        }

        public PartyDisplay()
        {
            this.InitializeComponent();
            timer.Tick += CollapseLocationBar;
            timer.Interval = new TimeSpan(0, 0, timeToDisplayLocationBar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void LoadImages()
        {
            Image firstImage = new Image();
            Image lastImage = new Image();
            firstImage.Source = Images.Last().Source;
            lastImage.Source = Images.First().Source;
            flipView.Items.Add(ConstructScrollViewer(firstImage));
            foreach( Image image in Images)
            {
                flipView.Items.Add(ConstructScrollViewer(image));
                Image previewPic = new Image();
                previewPic.Source = image.Source;
                previewPic.Opacity = transparency;
                previewPic.PointerPressed += NavigateToPic;
                locationBar.Children.Add(previewPic);
            }
            flipView.Items.Add(ConstructScrollViewer(lastImage));           
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

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ResetTimer();
            ManageLocationBarSelected();
            ManageCyclicalNavigation();
        }

        void CollapseLocationBar(object sender, object e)
        {
            locationBar.Visibility = Visibility.Collapsed;
        }

        private void ResetZoom(object sender, DoubleTappedRoutedEventArgs e)
        {
            ((ScrollViewer)flipView.Items.ElementAt(flipView.SelectedIndex)).ZoomToFactor(1);
        }

        private void NavigateToPic(object sender, PointerRoutedEventArgs e)
        {
            if (allowClickOnLocationBar)
            {
                currentIndex = locationBar.Children.IndexOf((UIElement)sender) + 1;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void ResetTimer()
        {
            timer.Stop();
            locationBar.Visibility = Visibility.Visible;
            timer.Start();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void ManageLocationBarSelected()
        {
            if (locationBar.Children.Count >= currentIndex && currentIndex > 0)
            {
                foreach (Image pic in locationBar.Children)
                {
                    pic.Opacity = transparency;
                }
                locationBar.Children.ElementAt(currentIndex - 1).Opacity = 1;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void ManageCyclicalNavigation()
        {
            if (currentIndex == 0)
            {
                currentIndex = flipView.Items.Count - 2;
            }
            else if (currentIndex == flipView.Items.Count - 1)
            {
                flipView.SelectedIndex = 1;
            }
            if (flipView.Items.Count > 3)
            {
                ((ScrollViewer)flipView.Items.ElementAt(flipView.Items.Count - 1)).ZoomToFactor(((ScrollViewer)flipView.Items.ElementAt(1)).ZoomFactor);
                ((ScrollViewer)flipView.Items.ElementAt(flipView.Items.Count - 1)).InvalidateScrollInfo();
                ((ScrollViewer)flipView.Items.ElementAt(0)).ZoomToFactor(((ScrollViewer)flipView.Items.ElementAt(flipView.Items.Count - 2)).ZoomFactor);
                ((ScrollViewer)flipView.Items.ElementAt(0)).InvalidateScrollInfo();
            }
        }

    }
}
