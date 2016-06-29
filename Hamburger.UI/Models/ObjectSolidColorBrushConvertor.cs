using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Hamburger.UI.Models
{
    class ObjectSolidColorBrushConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (null == value)
            {
                return null;
            }
            var colorValue = ((SolidColorBrush)value).Color;
            return (new ColorInfo(colorValue.ToString(), colorValue));
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (null == value)
            {
                return null;
            }
            return ((SolidColorBrush)((ColorInfo)value).ColorBrush);
        }
    }
}
