using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarrenFarmland.Models;

namespace BarrenFarmland
{
    class Program
    {
        static void Main(string[] args)
        {
            Coordinate testCoordinate = new Coordinate(0, 9);

            Coordinate testCoordinateTwo = new Coordinate(9, 0);

            Region testRegion = new Region(testCoordinate, testCoordinateTwo);
            PrintRegionCornerCoordinates(testRegion);
            Console.ReadLine();
        }

        public static void PrintRegionCornerCoordinates(Region regionToPrint)
        {
            Console.WriteLine("TopLeft: " + regionToPrint.TopLeftCorner.XValue + " " + regionToPrint.TopLeftCorner.YValue);
            Console.WriteLine("BottomLeft: " + regionToPrint.BottomLeftCorner.XValue + " " + regionToPrint.BottomLeftCorner.YValue);
            Console.WriteLine("BottomRight: " + regionToPrint.BottomRightCorner.XValue + " " + regionToPrint.BottomLeftCorner.YValue);
            Console.WriteLine("TopRight: " + regionToPrint.TopRightCorner.XValue + " " + regionToPrint.TopRightCorner.YValue);
            Console.WriteLine(regionToPrint.CalculateArea());
        }

        //private Tuple<int, int, int, int>[] RetrieveInput(string input)
        //{

        //    int counter = 0;
        //    string[] TupleCoordinates = input.Split(',');

        //    List<Tuple<int, int, int, int>> barrenRectangles = new List<Tuple<int, int, int, int>>();

        //    foreach (string Rectangle in TupleCoordinates)
        //    {
        //        string[] RectangleAsString = Rectangle.Split(' ');
        //        foreach(string Corners in RectangleAsString)
        //        {
        //            Tuple<int, int, int, int> newRectangle = new Tuple<int, int, int, int>();
        //            newRectangle.Item1 = 
        //            barrenRectangles.Add();
        //        }

        //    }
        //}
    }
}
