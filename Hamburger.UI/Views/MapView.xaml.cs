using Esri.ArcGISRuntime.Controls;
using System;
using System.Collections.Generic;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Hamburger.UI.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MapView : UserControl
    {
        public MapView()
        {
            this.InitializeComponent();
        }


        private void OnRootSceneViewLayerLoaded(object sender, LayerLoadedEventArgs e)
        {
            if (e.LoadError == null)
                return;

            //Debug.WriteLine($"Error while loading layer : {0} - {1}", e.Layer.ID, e.LoadError.Message);
        }
    }
}
