using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace Hamburger.UI.Models
{
    public class ColorInfo
    {
        public ColorInfo(string ColorName, Color Color)
        {
            this.Color = ColorName;
            this.ColorBrush = new SolidColorBrush(Color);

        }
        public string Color
        {
            get;
            set;
        }
        public Brush ColorBrush
        {
            get;
            set;
        }
    }
}
