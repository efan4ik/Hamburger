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
using Windows.Storage;
using Windows.Data.Pdf;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage.Streams;
using System.Collections.ObjectModel;

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
            var frames = FramesContainer.Children.Where(c => c is Frame).Cast<Frame>();
            foreach (var frame in frames)
            {
                _frames.Add(frame.Name, frame);
            }
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

        private Dictionary<string, Frame> _frames = new Dictionary<string, Frame>();
        public Dictionary<string, Frame> Frames
        {
            get { return _frames; }
            set
            {
                _frames = value;
            }
        }


        private string _currentFrame;
        public string CurrentFrame
        {
            get { return _currentFrame; }
            set
            {
                if (_currentFrame == null)
                    _currentFrame = "MapFrame";

                if (!Frames.ContainsKey(value))
                    return;

                Frames[_currentFrame].Visibility = Visibility.Collapsed;
                _currentFrame = value;
                Frames[_currentFrame].Visibility = Visibility.Visible;
            }
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            CurrentFrame = (e.OriginalSource as Button).CommandParameter.ToString();
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
        

        public async void OpenLocal()
        {
            StorageFolder appInstalledFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            StorageFile file = await appInstalledFolder.GetFileAsync("Assets\\sampleLinks.pdf");
            PdfDocument doc = await PdfDocument.LoadFromFileAsync(file);

            Load(doc);
        }

        async void Load(PdfDocument pdfDoc)
        {
            PdfPages.Clear();

            for (uint i = 0; i < pdfDoc.PageCount; i++)
            {
                BitmapImage image = new BitmapImage();

                var page = pdfDoc.GetPage(i);

                using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
                {
                    await page.RenderToStreamAsync(stream);
                    await image.SetSourceAsync(stream);
                }

                PdfPages.Add(image);
            }
        }

        public ObservableCollection<BitmapImage> PdfPages
        {
            get;
            set;
        } = new ObservableCollection<BitmapImage>();

        private void PdfFrame_Loaded(object sender, RoutedEventArgs e)
        {
            OpenLocal();
        }
    }
}
