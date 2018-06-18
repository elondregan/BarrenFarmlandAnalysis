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
        //Width corresponds to the X-Axis
        private const int FARMWIDTH = 400;
        //Length corresponds to the Y_Axis
        private const int FARMLENGTH = 600;
        

        public static Grid theFarm = new Grid(FARMWIDTH, FARMLENGTH);

        /// <summary>
        /// Executes the program, makes all the calls we are going to need that dp all our calculation
        /// </summary>
        /// <param name="args">We don't use this here.</param>
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
                }
                else if (input.Equals(EXITVALUE))
                {
                    Console.WriteLine("Exiting the program.");
                    continue;
                }
                else
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
                        Console.Write(area + " ");
                    }
                }
            }
        }

        /// <summary>
        /// Handles reading in what the user wrote and converting into values our Algorithm can handle.
        /// </summary>
        /// <param name="input">The string we read from the user</param>
        /// <returns>A list of regions to be input</returns>
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
                    try
                    {
                        Region barrenRegion = new Region(InputRectanglesBottomLeftCoordinate, InputRectanglesTopRightCoordinate, Barren);
                        barrenRectangles.Add(barrenRegion);
                    }
                    catch(ArgumentOutOfRangeException e)
                    {
                        Console.WriteLine("The selected points cannot form a rectangle, first value represents the bottom left corner and the second is the top right. Skipping the rectangle.");
                        continue;
                    }

                }
            }
            catch(ArgumentException e)
            {
                //Console.WriteLine("Rectangles MUST ONLY have four positive integer values in the format : blx bly trx try where blx and trx < 400 and bly and try < 600, please enter a valid rectangle.");
                RetrieveInput(Console.ReadLine());
            }

            return barrenRectangles;
        }
    }
}
