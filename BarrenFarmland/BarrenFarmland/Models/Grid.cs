using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenFarmland.Models
{
    //TODO: Time permiting, we should look into the possibility of turning everything into a quad tree.
    public class Grid
    {
        public int[,] GridRepresentation;
        public readonly int Width;
        public readonly int Length;

        public Grid(int Width, int Length)
        {
            this.Width = Width - 1;
            this.Length = Length - 1;
            if(Width < 0 || Length < 0)
            {
                throw new ArgumentOutOfRangeException("Width and Length must be greater than 0");
            }
            GridRepresentation = new int[Width, Length];
        }

        public bool ColorRegion(Region regionToColor, int Color)
        {
            try
            {
                if (regionToColor.BottomLeftCorner.YValue > Width || regionToColor.TopRightCorner.XValue > Length)
                {
                    throw new ArgumentOutOfRangeException();
                }
                for (int x = regionToColor.BottomLeftCorner.XValue; x <= regionToColor.BottomRightCorner.XValue; x++)
                {
                    for (int y = regionToColor.TopLeftCorner.YValue; y <= regionToColor.BottomLeftCorner.YValue; y++)
                    {
                        GridRepresentation[x, y] = Color;
                    }
                }
                return true;
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("The region is out of bounds of the grid, unable to color the region. " + e.StackTrace);
                return false;
            }
        }



        //public void GridRepresentation()
        //{

        //}
    }
}
