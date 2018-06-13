using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenFarmland.Models
{
    class Grid
    {
        public int[,] GridRepresentation;

        public Grid(int Width, int Length)
        {
            GridRepresentation = new int[Width, Length];
        }

        //public ColorRegion(Region regionToColor, int Color)
        //{

        //}
    }
}
