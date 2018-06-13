using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenFarmland.Models
{
    class Region
    {
        //Remember, all Coordinates are being placed in Q3 on the Real plane.
        public Coordinate BottomLeftCorner { get; private set; }
        public Coordinate BottomRightCorner { get; private set; }
        public Coordinate TopRightCorner { get; private set; }
        public Coordinate TopLeftCorner { get; private set; }

        public Region(Coordinate BottomLeftCorner, Coordinate TopRightCorner)
        {
            this.BottomLeftCorner = BottomLeftCorner;
            this.BottomRightCorner = new Coordinate(BottomLeftCorner.YValue, TopRightCorner.XValue);
            this.TopRightCorner = TopRightCorner;
            this.TopLeftCorner = new Coordinate(BottomLeftCorner.XValue, TopRightCorner.YValue);
        }

        public int CalculateArea()
        {
            //+1 just means we are including all our edges and creating closed sets.
            int yLength = (BottomLeftCorner.YValue - TopLeftCorner.YValue) + 1;
            int xLength = (BottomRightCorner.XValue - BottomLeftCorner.XValue) + 1;

            return yLength * xLength;
        }
    }
}
