using Esri.ArcGISRuntime.Controls;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Layers;
using Esri.ArcGISRuntime.Symbology;
using Hamburger.UI.Models;
using Hamburger.UI.Models.GraphicHelpers;
using Hamburger.UI.ViewModels;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Hamburger.UI.Views
{
    public sealed partial class PaintToolBoxView : UserControl
    {
        
        public static PaintToolBoxViewModel viewModel;
        public PaintToolBoxView()
        {
            viewModel = new PaintToolBoxViewModel();
            this.DataContext = viewModel;
            this.InitializeComponent();
        }

        public PaintToolBoxViewModel VM { get { return viewModel; } }

        public SceneView View
        {
            get { return (SceneView)GetValue(ViewProperty); }
            set{SetValue(ViewProperty, value);}
        }

        // Using a DependencyProperty as the backing store for View.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewProperty =
            DependencyProperty.Register("View", typeof(SceneView), typeof(PaintToolBoxView), new PropertyMetadata(null,
                new PropertyChangedCallback((DependencyObject d, DependencyPropertyChangedEventArgs e) => viewModel.View = ((SceneView)e.NewValue))));

        private void myList_ItemClick(object sender, ItemClickEventArgs e)
        {
            ListView list = sender as ListView;
            ListViewItem listItem = list.ContainerFromItem(e.ClickedItem) as ListViewItem;

            if (listItem.IsSelected)
            {
                listItem.IsSelected = false;
                list.SelectionMode = ListViewSelectionMode.None;
                (e.ClickedItem as DrawingOption).OnCancel.Invoke();
            }
            else
            {
                list.SelectionMode = ListViewSelectionMode.Single;
                listItem.IsSelected = true;
                (e.ClickedItem as DrawingOption).OnCheck.Invoke();
            }
        }
    }
}
