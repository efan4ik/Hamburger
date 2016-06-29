using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamburger.UI.Messages
{
    public class JumpToPointMessage
    {
        public string Point { get; set; }

        public JumpToPointMessage(string point)
        {
            Point = point;
        }
    }
}
