using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamburger.UI.Messages
{
    public class PaintModeMessage
    {
        public bool? IsOn;

        public PaintModeMessage(bool? isOn)
        {
            IsOn = isOn;
        }
    }
}
