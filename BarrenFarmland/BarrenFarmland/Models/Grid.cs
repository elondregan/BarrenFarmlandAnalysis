using System;
using System.Collections.Generic;
using System.Linq;

namespace BarrenFarmland.Models
{
    //TODO: Time permiting, we should look into the possibility of turning the grid into a quad tree for log(# of Rectangles) lookups as an alternate solution.
    public class Grid
    {
        public int[,] GridRepresentation;
        public readonly int Width;
        public readonly int Length;

        private const int Barren = -1;
        private const int Unvisited = 0;
        private const int Queued = 1;
        private const int Processed = 2;

        //X Value corresponds to Length, Y Value to Width
        public Grid(int Length, int Width)
        {
            this.Width = Width - 1;
            this.Length = Length - 1;
            if(Width < 0 || Length < 0)
            {
                throw new ArgumentOutOfRangeException("Width and Length must be greater than 0");
            }
            GridRepresentation = new int[Width, Length];
        }

        /// <summary>
        /// This is a source of some nasty overhead when dealing with large rectangles. Operation is O(w * l) 
        /// </summary>
        /// <param name="regionToColor"></param>
        /// <returns>Whether is succesfully colored the region, if it fails then it will spit out a message claiming the region is out of bounds.</returns>
        public bool ColorRegion(Region regionToColor)
        {
            try
            {
                if (regionToColor.BottomLeftCorner.YValue > Width || regionToColor.TopRightCorner.XValue > Length)
                {
                    throw new ArgumentOutOfRangeException();
                }
                else if(regionToColor.BottomLeftCorner.XValue < 0 || regionToColor.BottomLeftCorner.YValue < 0 || regionToColor.TopRightCorner.XValue < 0 || regionToColor.TopRightCorner.YValue < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }
                //Y iterates since we are populating ROWS first, and we are comparing them to x values at a time.
                for (int y = regionToColor.TopLeftCorner.XValue; y <= regionToColor.TopRightCorner.XValue; y++)
                {
                    for (int x = regionToColor.TopLeftCorner.YValue; x <= regionToColor.BottomLeftCorner.YValue; x++)
                    {
                        GridRepresentation[x, y] = regionToColor.Color;
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


        /// <summary>
        /// This is performing the majority of the work, this has an overhead of O(W * L) with W and L representing Grid length and width
        /// </summary>
        /// <returns>The areas of the non barren sections in ascending order</returns>
        public List<int> FindConnectedNodes()
        {
            //Need to force this to pass by value, otherwise changes made to representation affect gridClone as well.
            int[,] gridClone = (int[,])this.GridRepresentation.Clone();
            List<int> connectedRegions = new List<int>();

            for(int x = 0; x <= Width; x++)
            {
                for(int y = 0; y <= Length; y++)
                {
                    Coordinate currentGridPoint = new Coordinate(x, y);
                    if (GetGridValue(currentGridPoint) == Barren || GetGridValue(currentGridPoint) == Processed)
                    {
                        continue;
                    }
                    else
                    {
                        int area = 0;

                        List<Coordinate> unprocessedNeighbors;
                        Queue<Coordinate> CoordinateBuffer = new Queue<Coordinate>();
                        CoordinateBuffer.Enqueue(currentGridPoint);

                        while(CoordinateBuffer.Count > 0)
                        {
                            currentGridPoint = CoordinateBuffer.Dequeue();
                            SetGridValue(currentGridPoint, Processed);
                            unprocessedNeighbors = FindUnvisitedNeighbors(currentGridPoint);
                            MarkQueuedPoints(unprocessedNeighbors);
                            EnqueueEnumerable<Coordinate>(ref CoordinateBuffer, unprocessedNeighbors);
                            area++;
                        }

                        connectedRegions.Add(area);
                    }
                }
            }

            //Using LINQ to sort the connected Regions we have input.
            connectedRegions = connectedRegions.OrderBy(ProcessedRegion => ProcessedRegion).ToList<int>();
            //This is done so we don't have to reset the grid everytime we want to find connected nodes.
            GridRepresentation = gridClone;

            //in case our grid is entirely barren
            if(connectedRegions.Count == 0)
            {
                connectedRegions.Add(0);
            }
            return connectedRegions;
        }

        /// <summary>
        /// Makes a list holding neighbors in the format of E N W S.
        /// </summary>
        /// <param name="gridPoint"></param>
        /// <returns></returns>
        public List<Coordinate> FindUnvisitedNeighbors(Coordinate gridPoint)
        {
            List<Coordinate> neighboringCoordinates = new List<Coordinate>();

            if(gridPoint.XValue > 0)
            {
                Coordinate WestNeighbor = new Coordinate(gridPoint.XValue - 1, gridPoint.YValue);
                neighboringCoordinates.Add(WestNeighbor);
            }

            if(gridPoint.YValue > 0)
            {
                Coordinate NorthNeighbor = new Coordinate(gridPoint.XValue, gridPoint.YValue - 1);
                neighboringCoordinates.Add(NorthNeighbor);
            }

            if(gridPoint.XValue < Width)
            {
                Coordinate EastNeighbor = new Coordinate(gridPoint.XValue + 1, gridPoint.YValue);
                neighboringCoordinates.Add(EastNeighbor);
            }

            if(gridPoint.YValue < Length)
            {
                Coordinate SouthNeighbor = new Coordinate(gridPoint.XValue, gridPoint.YValue + 1);
                neighboringCoordinates.Add(SouthNeighbor);
            }

            return neighboringCoordinates.Where(point => GetGridValue(point) == Unvisited).ToList<Coordinate>();
        }

        public void MarkQueuedPoints(List<Coordinate> pointsToSetQueued)
        {
            foreach (Coordinate queuedPoint in pointsToSetQueued)
            {
                SetGridValue(queuedPoint, Queued);
            }
        }

        /// <summary>
        /// Oddly enough this is not built in so I made a new generic method.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Queue"></param>
        /// <param name="ToQueue"></param>
        public void EnqueueEnumerable<T>(ref Queue<T> Queue, IEnumerable<T> ToQueue)
        {
            foreach(T element in ToQueue)
            {
                Queue.Enqueue(element);
            }
        }

        public int GetGridValue(Coordinate gridCoordinate)
        {
            return GridRepresentation[gridCoordinate.XValue, gridCoordinate.YValue];
        }

        public void SetGridValue(Coordinate gridCoordinate, int Value)
        {
            GridRepresentation[gridCoordinate.XValue, gridCoordinate.YValue] = Value;
        }

    }
}
