using Esri.ArcGISRuntime.Controls;
using Esri.ArcGISRuntime.Symbology;
using Hamburger.UI.Models.GraphicHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.Foundation;
using Esri.ArcGISRuntime.Geometry;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using System.Windows.Input;
using Esri.ArcGISRuntime.Layers;
using Windows.UI.Xaml.Controls;
using System.Diagnostics;
using Prism.Mvvm;
using Prism.Commands;
using Hamburger.UI.Models;

namespace Hamburger.UI.ViewModels
{
    public class PaintToolBoxViewModel : BindableBase
    {
        private SceneView _view;
        public SceneView View
        {
            get { return _view; }
            set {
                _view = value;
                initiolizeMap(value);
            }
        }
        private static Color DEFAULT_POLYGON_BORFDER_COLOR = Colors.Black;
        private const double DEFAULT_WIDTH = 3;
        private const byte DEFAULT_AREA_ALPHA = 150;
        private static GraphicSelection _selection;
        private GraphicsOverlay _polygonsOverlay;
        private GraphicsOverlay _polylinesOverlay;
        private Visibility _colorPickerVisability;
        public Visibility ColorPikerVisability
        {
            get
            {
                return _colorPickerVisability;
            }
            set
            {
                SetProperty(ref _colorPickerVisability, value);
            }
        }
        private SolidColorBrush SelectedColor { get; set; } = new SolidColorBrush(Colors.Yellow);
        public ICommand ColorPickerButton_Click { get; set; }
        public List<DrawingOption> DrawingOptions { get; set; }

        public PaintToolBoxViewModel()
        {
            ColorPickerButton_Click = new DelegateCommand(() => OnColorPickButtonClick());
            initiolizeDrawingOptions();
        }

        private void initiolizeDrawingOptions()
        {
            DrawingOptions = new List<DrawingOption>
            {
                new DrawingOption("FreeHandButton","\xE70F",OnFreehandButtonClick,SceneEditHelper.Cancel),
                new DrawingOption("LineButton","\xE738",OnLineButtonClick,SceneEditHelper.Cancel),
                new DrawingOption("AreaButton","\xE932",OnAreaButtonClick,SceneEditHelper.Cancel),
                new DrawingOption("EditButton","\xE8D3",OnEditButtonClick,SceneEditHelper.Cancel)
            };
        }

        private void initiolizeMap(SceneView View)
        {
            _polygonsOverlay = new GraphicsOverlay();
            _polygonsOverlay.ID = "PolygonGraphicsOverlay";
            View.GraphicsOverlays.Add(_polygonsOverlay);
            _polylinesOverlay = new GraphicsOverlay();
            _polylinesOverlay.ID = "PolylineGraphicsOverlay";
            View.GraphicsOverlays.Add(_polylinesOverlay);
            _polygonsOverlay.Renderer = new SimpleRenderer() { Symbol = new SimpleFillSymbol() { Color = Colors.Aqua } };
            View.SceneViewTapped += Map_Tapped;
        }

        private async void Map_Tapped(object sender, MapViewInputEventArgs e)
        {
            // If draw or edit is active, return
            if (SceneEditHelper.IsActive) return;
            // Try to select a graphic from the map location
            await SelectGraphicAsync(e.Position);
        }

        private async Task SelectGraphicAsync(Point position)
        {
            // Clear previous selection
            if (_selection != null)
            {
                _selection.Unselect();
                _selection.SetVisible();
            }
            _selection = null;

            // Find first graphic from the overlays
            foreach (var overlay in View.GraphicsOverlays)
            {
                var foundGraphic = await overlay.HitTestAsync(
                        View,
                        position);

                if (foundGraphic != null)
                {
                    _selection = new GraphicSelection(foundGraphic, overlay);
                    _selection.Select();
                    break;
                }
            }
        }

        private async void OnEditButtonClick()
        {
            if (_selection == null) return; // Selection missing

            // Cancel previous edit
            if (SceneEditHelper.IsActive)
                SceneEditHelper.Cancel();

            Esri.ArcGISRuntime.Geometry.Geometry editedGeometry = null;

            try
            {
                // Edit selected geometry and set it back to the selected graphic
                switch (_selection.GeometryType)
                {
                    case GeometryType.Point:
                        editedGeometry = await SceneEditHelper.CreatePointAsync(
                            View);
                        break;
                    case GeometryType.Polyline:
                        _selection.SetHidden(); // Hide selected graphic from the UI
                        editedGeometry = await SceneEditHelper.EditPolylineAsync(
                            View,
                            _selection.SelectedGraphic.Geometry as Polyline);
                        break;
                    case GeometryType.Polygon:
                        _selection.SetHidden(); // Hide selected graphic from the UI
                        editedGeometry = await SceneEditHelper.EditPolygonAsync(
                            View,
                            _selection.SelectedGraphic.Geometry as Polygon);
                        break;
                    default:
                        break;
                }

                _selection.SelectedGraphic.Geometry = editedGeometry; // Set edited geometry to selected graphic
            }
            catch (TaskCanceledException tce)
            {
                // This occurs if draw operation is canceled or new operation is started before previous was finished.
                Debug.WriteLine("Previous edit operation was canceled.");
            }
            finally
            {
                _selection.Unselect();
                _selection.SetVisible(); // Show selected graphic from the UI
            }
        }

        private void OnColorPickButtonClick()
        {
            ColorPikerVisability = ColorPikerVisability == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

        private void OnColorChanged(object sender, SolidColorBrush e)
        {
            SelectedColor = e;
            ColorPikerVisability = Visibility.Collapsed;
        }

        private async void OnLineButtonClick()
        {
            var geometry = await SceneEditHelper.CreatePolylineAsync(View);
            var graphic = new Graphic(geometry);
            graphic.Symbol = new SimpleLineSymbol() { Color = SelectedColor.Color, Width = DEFAULT_WIDTH };
            _polylinesOverlay.Graphics.Add(graphic);
        }

        private async void OnFreehandButtonClick()
        {
            var geometry = await SceneEditHelper.CreateFreeHandAsync(View);
            var graphic = new Graphic(geometry);
            graphic.Symbol = new SimpleLineSymbol() { Color = SelectedColor.Color, Width = DEFAULT_WIDTH };
            _polylinesOverlay.Graphics.Add(graphic);
        }

        private async void OnAreaButtonClick()
        {
            var geometry = await SceneEditHelper.CreatePolygonAsync(View);
            var graphic = new Graphic(geometry);
            graphic.Symbol = new SimpleFillSymbol() { Color = getColorWithAlpha(SelectedColor.Color, DEFAULT_AREA_ALPHA), Outline = new SimpleLineSymbol() { Color = DEFAULT_POLYGON_BORFDER_COLOR } };
            _polygonsOverlay.Graphics.Add(graphic);
        }

        private Color getColorWithAlpha(Color color, byte alpha)
        {
            return Color.FromArgb(alpha, color.R, color.G, color.B);
        }
    }
}
