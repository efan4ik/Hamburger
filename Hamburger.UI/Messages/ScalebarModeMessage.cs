using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamburger.UI.Messages
{
    class ScalebarModeMessage
    {
        public bool? IsVisible;

        public ScalebarModeMessage(bool? isVisible)
        {
            IsVisible = isVisible;
        }
    }
}
