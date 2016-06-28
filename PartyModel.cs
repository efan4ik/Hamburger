using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartySelectionPanel.Model
{
    public class PartyModel
    {
        public PartyModel(string name, int x, int y)
        {
            this.name = name;
            this.x = x;
            this.y = y;
        }
        public string name { get; set; }
        //x and y coordinates for the location of the party
        public int x { get; set; }
        public int y { get; set; }
    }
}
