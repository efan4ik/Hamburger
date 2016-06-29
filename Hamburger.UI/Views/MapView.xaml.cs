using Esri.ArcGISRuntime.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Hamburger.UI.ViewModels;
using Esri.ArcGISRuntime.Geometry;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Hamburger.UI.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MapView : Page, IMapView
    {
        public MapView()
        {
            this.InitializeComponent();
            (DataContext as MapViewModel).MapView = this as IMapView;
        }

        private void OnRootSceneViewLayerLoaded(object sender, LayerLoadedEventArgs e)
        {
            if (e.LoadError == null)
                return;

            //Debug.WriteLine($"Error while loading layer : {0} - {1}", e.Layer.ID, e.LoadError.Message);
        }

        private void NorthButton_Click(object sender, RoutedEventArgs e)
        {
            RooSceneView.SetViewAsync(new Camera(RooSceneView.Camera.Location, 0, RooSceneView.Camera.Pitch));
        }

        public void JumpToPoint(string pointString)
        {
            MapPoint point = ConvertCoordinate.FromDecimalDegrees(pointString, RooSceneView.SpatialReference);
            RooSceneView.SetViewAsync(new Camera(point.Y, point.X, 1000, 0, 0));
        }
    }
}
