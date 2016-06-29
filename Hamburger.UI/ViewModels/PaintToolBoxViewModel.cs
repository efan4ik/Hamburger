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
using Hamburger.UI.Models;
using System.ComponentModel;
using GalaSoft.MvvmLight.Command;

namespace Hamburger.UI.ViewModels
{
    public class PaintToolBoxViewModel : INotifyPropertyChanged
    {
        #region DataMembers
        private SceneView _view;
        public SceneView View
        {
            get { return _view; }
            set
            {
                _view = value;
                initiolizeMap(value);
            }
        }
        private static Color DEFAULT_POLYGON_BORFDER_COLOR = Colors.Black;
        private const double DEFAULT_WIDTH = 3;
        private const byte DEFAULT_AREA_ALPHA = 150;
        private static GraphicSelection _selection;
        private GraphicsOverlay _drawingOverlay;
        private Visibility _colorPickerVisability;
        public Visibility ColorPikerVisability
        {
            get
            {
                return _colorPickerVisability;
            }
            set
            {
                _colorPickerVisability = value;
                RaisePropertyChanged("_colorPickerVisability");
            }
        }
        private SolidColorBrush _selectedColor;

        public SolidColorBrush SelectedColor
        {
            get { return _selectedColor; }
            set {
                _selectedColor = value;
                RaisePropertyChanged("_selectedColor");
            }
        }

        private void RaisePropertyChanged(String info)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(info));
        }

        public List<DrawingOption> DrawingOptions { get; set; }
        private object _selectedDrawingOption;

        public object SelectedDrawingOption
        {
            get { return _selectedDrawingOption; }
            set
            {
                _selectedDrawingOption = value;
                RaisePropertyChanged("_selectedDrawingOption");
                onDrawingOptionSelectionChange(value);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private DrawingOption _eraseDrawingOption { get; set; }
        public List<ColorInfo> ColorPickerColors { get; set; }
        #endregion

        #region Ctor

        public PaintToolBoxViewModel()
        {
            initiolizeDrawingOptions();
            initiolizeColors();
            SelectedColor = new SolidColorBrush(Colors.Yellow);
        }

        #endregion

        #region Initiolize

        private void initiolizeColors()
        {
            ColorPickerColors = new List<ColorInfo>
            {
                new ColorInfo("Red", Colors.Red),
                new ColorInfo("Blue", Colors.Blue),
                new ColorInfo("Yellow", Colors.Yellow),
                new ColorInfo("Green", Colors.Green),
                new ColorInfo("Orange", Colors.Orange),
                new ColorInfo("Purple", Colors.Purple)
            };
        }

        private void initiolizeDrawingOptions()
        {
            _eraseDrawingOption = new DrawingOption("EraseButton", "\xE75C", OnEraseButtonClick);
            DrawingOptions = new List<DrawingOption>
            {
                new DrawingOption("FreeHandButton","\xE70F",OnFreehandButtonClick),
                new DrawingOption("LineButton","\xE738",OnLineButtonClick),
                new DrawingOption("AreaButton","\xE932",OnAreaButtonClick),
                new DrawingOption("EditButton","\xE7C9",OnEditButtonClick),
                _eraseDrawingOption
            };
        }

        private void initiolizeMap(SceneView View)
        {
            _drawingOverlay = new GraphicsOverlay();
            _drawingOverlay.ID = "PolygonGraphicsOverlay";
            View.GraphicsOverlays.Add(_drawingOverlay);
            _drawingOverlay.Renderer = new SimpleRenderer() { Symbol = new SimpleFillSymbol() { Color = Colors.Aqua } };
            View.SceneViewTapped += Map_Tapped;
        }
        #endregion

        #region Drawing

        private void onDrawingOptionSelectionChange(object value)
        {
            if (value != null)
            {
                (value as DrawingOption).OnCheck();
            }
            else
            {
                SceneEditHelper.Cancel();
            }
        }

        private void OnLineButtonClick()
        {
            ExecuteDrawing(SceneEditHelper.CreatePolylineAsync);
        }

        private void OnFreehandButtonClick()
        {
            ExecuteDrawing(SceneEditHelper.CreateFreeHandAsync);
        }

        private void OnAreaButtonClick()
        {
            Esri.ArcGISRuntime.Symbology.Symbol drawingSymbol = new SimpleFillSymbol() { Color = getColorWithAlpha(SelectedColor.Color, DEFAULT_AREA_ALPHA), Outline = new SimpleLineSymbol() { Color = DEFAULT_POLYGON_BORFDER_COLOR } };
            ExecuteDrawing(SceneEditHelper.CreatePolygonAsync, drawingSymbol);
        }

        private void ExecuteDrawing(Func<SceneView, Task<Esri.ArcGISRuntime.Geometry.Geometry>> createGeometryAsync)
        {
            ExecuteDrawing(createGeometryAsync, new SimpleLineSymbol() { Color = SelectedColor.Color, Width = DEFAULT_WIDTH });
        }

        private async void ExecuteDrawing(Func<SceneView, Task<Esri.ArcGISRuntime.Geometry.Geometry>> createGeometryAsync, Esri.ArcGISRuntime.Symbology.Symbol symbol)
        {
            Esri.ArcGISRuntime.Geometry.Geometry DrawedGeometry = null;
            bool toContinueDrawing = true;
            while (toContinueDrawing)
            {
                try
                {
                    DrawedGeometry = await createGeometryAsync(View);
                }
                catch (DrawCanceledExeption e)
                {
                    DrawedGeometry = e.DrawedGepmetry;
                    toContinueDrawing = false;
                }
                finally
                {
                    if (DrawedGeometry != null && !DrawedGeometry.IsEmpty)
                    {
                        var graphic = new Graphic(DrawedGeometry);
                        graphic.Symbol = symbol;
                        _drawingOverlay.Graphics.Add(graphic);
                    }
                }
            }
        }

        #endregion

        #region Editing
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
        #endregion

        #region Erasing

        private async void OnEraseButtonClick()
        {
            while((SelectedDrawingOption as DrawingOption) == _eraseDrawingOption)
            {
                await SceneEditHelper.EraseGeometrys(View);
            }
        }

        #endregion


        #region InternalMetods
        private Color getColorWithAlpha(Color color, byte alpha)
        {
            return Color.FromArgb(alpha, color.R, color.G, color.B);
        } 
        #endregion
    }
}
