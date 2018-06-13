using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenFarmland.Models
{

    class Coordinate
    {
        public int XValue { get; private set; }
        public int YValue { get; private set; }

        public Coordinate(int xValue, int yValue)
        {
            this.XValue = xValue;
            this.YValue = yValue;
        }
    }
}
