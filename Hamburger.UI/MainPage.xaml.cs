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
using Windows.Storage.Pickers;

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


        //    public async void OpenLocal()
        //    {
        //        StorageFolder appInstalledFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
        //        StorageFile file = await appInstalledFolder.GetFileAsync("Assets\\sampleLinks.pdf");
        //        PdfDocument doc = await PdfDocument.LoadFromFileAsync(file);

        //        Load(doc);
        //    }

        //    async void Load(PdfDocument pdfDoc)
        //    {
        //        PdfPages.Clear();

        //        for (uint i = 0; i < pdfDoc.PageCount; i++)
        //        {
        //            BitmapImage image = new BitmapImage();

        //            var page = pdfDoc.GetPage(i);

        //            using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
        //            {
        //                await page.RenderToStreamAsync(stream);
        //                await image.SetSourceAsync(stream);
        //            }

        //            PdfPages.Add(image);
        //        }
        //    }

        //    public ObservableCollection<BitmapImage> PdfPages
        //    {
        //        get;
        //        set;
        //    } = new ObservableCollection<BitmapImage>();

        //    private void PdfFrame_Loaded(object sender, RoutedEventArgs e)
        //    {
        //        OpenLocal();
        //    }

        private PdfDocument pdfDocument;
        private uint pageNumber = 1;


        private async void LoadDocument()
        {
            //LoadButton.IsEnabled = false;

            pdfDocument = null;
            Output.Source = null;
            PageNumberBox.Text = "1";
            RenderingPanel.Visibility = Visibility.Collapsed;

            //var picker = new FileOpenPicker();
            //picker.FileTypeFilter.Add(".pdf");
            //StorageFile file = await picker.PickSingleFileAsync();

            StorageFolder appInstalledFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            StorageFile file = await appInstalledFolder.GetFileAsync("Assets\\sampleLinks.pdf");
            //PdfDocument doc = await PdfDocument.LoadFromFileAsync(file);
            if (file != null)
            {
                ProgressControl.Visibility = Visibility.Visible;
                try
                {
                    pdfDocument = await PdfDocument.LoadFromFileAsync(file);
                }
                catch (Exception ex) { }

                if (pdfDocument != null)
                {
                    RenderingPanel.Visibility = Visibility.Visible;
                   
                    PageCountText.Text = pdfDocument.PageCount.ToString();
                }
                ProgressControl.Visibility = Visibility.Collapsed;
            }
            //LoadButton.IsEnabled = true;
            ViewPage();

        }

        private async void ViewPage()
        {

            
            if ( (pageNumber < 1) || (pageNumber > pdfDocument.PageCount)) //!uint.TryParse(PageNumberBox.Text, out pageNumber) ||
            {
                return;
            }

            Output.Source = null;
            ProgressControl.Visibility = Visibility.Visible;

            // Convert from 1-based page number to 0-based page index.
            uint pageIndex = pageNumber - 1;

            using (PdfPage page = pdfDocument.GetPage(pageIndex))
            {
                var stream = new InMemoryRandomAccessStream();
                await page.RenderToStreamAsync(stream);
                /*switch (Options.SelectedIndex)
                //{
                //    // View actual size.
                //    case 0:
                //        await page.RenderToStreamAsync(stream);
                //        break;

                //    // View half size on beige background.
                //    case 1:
                //        var options1 = new PdfPageRenderOptions();
                //        options1.BackgroundColor = Windows.UI.Colors.Beige;
                //        options1.DestinationHeight = (uint)(page.Size.Height / 2);
                //        options1.DestinationWidth = (uint)(page.Size.Width / 2);
                //        await page.RenderToStreamAsync(stream, options1);
                //        break;

                //    // Crop to center.
                //    case 2:
                //        var options2 = new PdfPageRenderOptions();
                //        var rect = page.Dimensions.TrimBox;
                //        options2.SourceRect = new Rect(rect.X + rect.Width / 4, rect.Y + rect.Height / 4, rect.Width / 2, rect.Height / 2);
                //        await page.RenderToStreamAsync(stream, options2);
                //        break;
               }*/
                BitmapImage src = new BitmapImage();
                Output.Source = src;
                await src.SetSourceAsync(stream);
            }
            ProgressControl.Visibility = Visibility.Collapsed;
        }

        private void LoadDocument(object sender, RoutedEventArgs e)
        {
            LoadDocument();
        }

    }
}

