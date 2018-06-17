using System;
using System.Collections.Generic;
using BarrenFarmland.Models;

namespace BarrenFarmland
{
    class BarrenLandAnalyzer
    {
        private const int Barren = -1;
        private const string RESETCHAR = "0";
        private const string EXITVALUE = "-1";
        private const int FARMWIDTH = 400;
        private const int FARMLENGTH = 600;
        

        public static Grid theFarm = new Grid(FARMWIDTH, FARMLENGTH);

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Barren land analyzer 1.0!");
            Console.WriteLine("Please enter a set of barren rectangles, they must have the format of x y x y, x y x y");
            Console.WriteLine("If you wish to reset the farmland to have no barren regions enter 0");
            Console.WriteLine("Once you have everything input, we'll calculate the fertile regions!");
            Console.WriteLine("To exit the program, enter -1");

            string input = "";
            while (input != EXITVALUE)
            {
                input = Console.ReadLine();
                if (input.Equals(RESETCHAR))
                {
                    Console.WriteLine("Resetting the grid to be completely fertile.");
                    theFarm = new Grid(FARMWIDTH, FARMLENGTH);
                    continue;
                } else if (input.Equals(EXITVALUE))
                {
                    Console.WriteLine("Exiting the program.");
                    continue;
                } else
                {
                    List<Region> UnprocessedRectangles = RetrieveInput(input);

                    //Perform the work!
                    foreach (Region barrenRectangle in UnprocessedRectangles)
                    {
                        theFarm.ColorRegion(barrenRectangle);
                    }

                    List<int> SortedAreas = theFarm.FindConnectedNodes();
                    foreach (int area in SortedAreas)
                    {
                        Console.Write(area + ", ");
                    }
                }
            }
        }

        private static List<Region> RetrieveInput(string input)
        {
            string[] TupleCoordinates = input.Split(',');

            List<Region> barrenRectangles = new List<Region>();

            try
            {
                foreach (string Rectangle in TupleCoordinates)
                {
                    string[] RectangleAsString = Rectangle.Split(' ');

                    if (RectangleAsString.Length != 4)
                    {
                        ArgumentException e = new ArgumentException();
                        throw e;
                    }

                    //We need to define all four elements at once based on how I made us create rectangles 
                    Coordinate InputRectanglesBottomLeftCoordinate = new Coordinate(Int32.Parse(RectangleAsString[0]), Int32.Parse(RectangleAsString[1]));
                    Coordinate InputRectanglesTopRightCoordinate = new Coordinate(Int32.Parse(RectangleAsString[2]), Int32.Parse(RectangleAsString[3]));

                    Region barrenRegion = new Region(InputRectanglesBottomLeftCoordinate, InputRectanglesTopRightCoordinate, Barren);
                    barrenRectangles.Add(barrenRegion);
                }
            }
            catch(ArgumentException e)
            {
                Console.WriteLine("Rectangles MUST ONLY have four integer values in the format : {blx bly trx try, please enter a valid rectangle.");
                RetrieveInput(Console.ReadLine());
            }

            return barrenRectangles;
        }
    }
}
