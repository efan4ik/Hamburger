using Esri.ArcGISRuntime.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamburger.UI.Models.GraphicHelpers
{
    class DrawCanceledExeption : TaskCanceledException
    {
        public Geometry DrawedGepmetry{ get; set; }
        public DrawCanceledExeption(String message, TaskCanceledException e, Geometry Drawed)
            :base(message,e)
        {
            this.DrawedGepmetry = Drawed;
        }
    }
}
