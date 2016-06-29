using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamburger.UI.Models
{
    public class RestrauntModel
    {
        public string Name { get; set; }

        public double X { get; set; }

        public double Y { get; set; }

        public CommandHandler JumpToPointCommand = new CommandHandler(handlr);

        private static void handlr()
        {
            throw new NotImplementedException();
        }
    }
}
