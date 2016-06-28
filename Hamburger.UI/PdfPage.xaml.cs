using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Hamburger.UI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PdfPage : Page
    {
        public PdfPage()
        {
            this.InitializeComponent();
        }

        private async void SelectFile_Click(object sender, RoutedEventArgs e)
        {

            StorageFile file = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync(@ "Foldername\filename.pdf");
            await Windows.System.Launcher.LaunchFileAsync(file);
        }
    }
}
