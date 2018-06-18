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

        //Color is simply how we represent the region on a grid as an integer value.
        public int Color { get; private set; }

        /// <summary>
        /// As per requirement, we want to read in regions as BLC, TRC Coordinates.
        /// </summary>
        /// <param name="BottomLeftCorner"></param>
        /// <param name="TopRightCorner"></param>
        /// <param name="Color"></param>
        public Region(Coordinate BottomLeftCorner, Coordinate TopRightCorner,int Color)
        {
            if(BottomLeftCorner.YValue < TopRightCorner.YValue || TopRightCorner.XValue < BottomLeftCorner.XValue)
            {
                throw new ArgumentOutOfRangeException();
            }
            //if(BottomLeftCorner.YValue < 0 || BottomLeftCorner.XValue < 0 || TopRightCorner.YValue < 0 || TopRightCorner.XValue < 0)
            //{
            //    throw new ArgumentOutOfRangeException("No negative values are allowed for regions.");
            //}
            this.BottomLeftCorner = BottomLeftCorner;
            this.BottomRightCorner = new Coordinate(TopRightCorner.XValue, BottomLeftCorner.YValue);
            this.TopRightCorner = TopRightCorner;
            this.TopLeftCorner = new Coordinate(BottomLeftCorner.XValue, TopRightCorner.YValue);
            this.Color = Color;
        }

        public int CalculateArea()
        {
            int yLength = (BottomLeftCorner.YValue - TopLeftCorner.YValue) + 1;
            int xLength = (BottomRightCorner.XValue - BottomLeftCorner.XValue) + 1;

            return yLength * xLength;
        }
    }
}
