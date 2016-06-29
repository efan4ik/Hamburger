using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Pdf;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Hamburger.UI.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PdfReader : Page
    {
        public PdfReader()
        {
            this.InitializeComponent();
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


            if ((pageNumber < 1) || (pageNumber > pdfDocument.PageCount)) //!uint.TryParse(PageNumberBox.Text, out pageNumber) ||
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