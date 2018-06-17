using System;

namespace BarrenFarmland.Models
{
    public class Region
    {
        //Remember, all Coordinates are being placed in Q3 on the Real plane.
        public Coordinate BottomLeftCorner { get; private set; }
        public Coordinate BottomRightCorner { get; private set; }
        public Coordinate TopRightCorner { get; private set; }
        public Coordinate TopLeftCorner { get; private set; }
        public int Color { get; private set; }

        public Region(Coordinate BottomLeftCorner, Coordinate TopRightCorner,int Color)
        {
            if(BottomLeftCorner.YValue < TopRightCorner.YValue || TopRightCorner.XValue < BottomLeftCorner.XValue)
            {
                throw new ArgumentOutOfRangeException("The selected points cannot form a rectangle, first value represents the bottom left corner and the second is the top right.");
            }
            this.BottomLeftCorner = BottomLeftCorner;
            this.BottomRightCorner = new Coordinate(TopRightCorner.XValue, BottomLeftCorner.YValue);
            this.TopRightCorner = TopRightCorner;
            this.TopLeftCorner = new Coordinate(BottomLeftCorner.XValue, TopRightCorner.YValue);
            this.Color = Color;
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
