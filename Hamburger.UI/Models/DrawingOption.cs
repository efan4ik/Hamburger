using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamburger.UI.Models
{
    public class DrawingOption
    {
        public String Name { get; set; }
        public String Icon { get; set; }
        public Action OnCheck { get; set; }

        public DrawingOption(String Name, String Icon, Action OnCheck)
        {
            this.Name = Name;
            this.Icon = Icon;
            this.OnCheck = OnCheck;
        }
    }
}
