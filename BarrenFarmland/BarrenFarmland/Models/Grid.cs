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
        public readonly int Barren = -1;
        public readonly int Unvisited = 0;
        public readonly int Queued = 1;
        public readonly int Processed = 2;

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

        public bool ColorRegion(Region regionToColor, int Color)
        {
            try
            {
                if (regionToColor.BottomLeftCorner.YValue > Width || regionToColor.TopRightCorner.XValue > Length)
                {
                    throw new ArgumentOutOfRangeException();
                }
                //Y iterates since we are populating ROWS first, and we are comparing them to x values at a time.
                for (int y = regionToColor.TopLeftCorner.XValue; y <= regionToColor.TopRightCorner.XValue; y++)
                {
                    for (int x = regionToColor.TopLeftCorner.YValue; x <= regionToColor.BottomLeftCorner.YValue; x++)
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

        public List<int> FindConnectedNodes()
        {
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
            //Still need to sort this.
            return connectedRegions;
        }

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
