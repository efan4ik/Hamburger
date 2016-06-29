using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Hamburger.UI.Models
{
    class SolidBrushToBrushConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (null == value)
            {
                return null;
            }
            return ((SolidColorBrush)value);
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (null == value)
            {
                return null;
            }
            return ((Brush)value);
        }
    }
}
