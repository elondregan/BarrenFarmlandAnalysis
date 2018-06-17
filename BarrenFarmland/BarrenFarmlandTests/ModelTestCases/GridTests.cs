using System;
using System.Collections.Generic;
using NUnit.Framework;
using BarrenFarmland.Models;

namespace BarrenFarmlandTests
{
    [TestFixture]
    public class GridTests
    {
        public readonly int Barren = -1;

        /// <summary>
        /// Makes sure that the grid constructor plays nice with the fields.
        /// </summary>
        [Test]
        public void GridConstructorMakesCorrectSizedArray()
        {
            Grid FiveByFive = new Grid(5, 5);

            Assert.IsTrue(FiveByFive.GridRepresentation.Length == (FiveByFive.Length + 1) * (FiveByFive.Width + 1));
        }

        /// <summary>
        /// We need positive Grid values for the code.
        /// </summary>
        [Test]
        public void GridConstructorNegativeValueArgumentException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Grid(-5, 5));
        }

        /// <summary>
        /// Makes sure that the color Region if given a region that encompasses the entire grid paints it.
        /// </summary>
        [Test]
        public void PaintFullGrid()
        {
            Grid ThreeByFive = new Grid(3, 5);

            Region FullGridEncompassed = new Region(new Coordinate(0, ThreeByFive.Width), new Coordinate(ThreeByFive.Length, 0), Barren);

            ThreeByFive.ColorRegion(FullGridEncompassed);
            foreach (int point in ThreeByFive.GridRepresentation)
            {
                Assert.IsTrue(point == Barren);
            }
        }

        /// <summary>
        /// We should get an error when trying to color off the grid, execution can continue but the chosen region will not add anything.
        /// </summary>
        [Test]
        public void OutOfBoundsRegionColorTest()
        {
            Grid FiveByFive = new Grid(5, 5);
            Region OutOfBoundsPoint = new Region(new Coordinate(0, FiveByFive.Width + 1), new Coordinate(0, FiveByFive.Width + 1) ,Barren);

            Assert.IsTrue(FiveByFive.ColorRegion(OutOfBoundsPoint) == false);
        }

        /// <summary>
        /// Making sure we can point an arbitrary point on the grid
        /// </summary>
        [Test]
        public void InBoundsPointColorTest()
        {
            Grid threeByFive = new Grid(3, 5);
            Region origin = new Region(new Coordinate(1, 1), new Coordinate(1, 1), Barren);

            threeByFive.ColorRegion(origin);
            Assert.IsTrue(threeByFive.GridRepresentation[1, 1] == Barren);
        }

        /// <summary>
        /// We want to paint an entire rectangle 
        /// </summary>
        [Test]
        public void WholeRectangleColorTest()
        {
            Grid ThreeByFour = new Grid(3, 4);
            Region TheGrid = new Region(new Coordinate(0, ThreeByFour.Width), new Coordinate(ThreeByFour.Length, 0), Barren);
            ThreeByFour.ColorRegion(TheGrid);
            foreach(int gridValue in ThreeByFour.GridRepresentation)
            {
                Assert.True(gridValue == Barren);
            }

            Grid FourByThree = new Grid(4, 3);
            TheGrid = new Region(new Coordinate(0, FourByThree.Width), new Coordinate(FourByThree.Length, 0), Barren);
            FourByThree.ColorRegion(TheGrid);
            foreach (int gridValue in FourByThree.GridRepresentation)
            {
                Assert.True(gridValue == Barren);
            }
        }

        /// <summary>
        /// This tests all four conditions at once, and ensures that we won't have any false positives.
        /// </summary>
        [Test]
        public void FindOnePointGridNeighbors()
        {
            Grid OneByOne = new Grid(1, 1);
            Coordinate origin = new Coordinate(0, 0);
            Assert.True(OneByOne.FindUnvisitedNeighbors(origin) != null);
        }

        /// <summary>
        /// This test makes sure if we are a middle point that we have all four neighbors picked up
        /// </summary>
        [Test]
        public void FindAllPointGridNeighbors()
        {
            Grid ThreeByThree = new Grid(3, 3);
            Coordinate center = new Coordinate(1, 1);
            List<Coordinate> ShouldHaveFourPoints = ThreeByThree.FindUnvisitedNeighbors(center);
            Assert.True(ShouldHaveFourPoints.Count == 4);
        }

        /// <summary>
        /// This is a test to make sure if we have a single non-barren spot that we are good
        /// </summary>
        [Test]
        public void OneFertileCellTest()
        {
            Grid ThreeByThree = new Grid(3, 3);
            Region TwobyThreeSlice = new Region(new Coordinate(0, ThreeByThree.Width), new Coordinate(1, 0), Barren);
            Region OneByTwoSlice = new Region(new Coordinate(ThreeByThree.Length, 1), new Coordinate(ThreeByThree.Length, 0), Barren);

            ThreeByThree.ColorRegion(TwobyThreeSlice);
            ThreeByThree.ColorRegion(OneByTwoSlice);

            List<int> area = ThreeByThree.FindConnectedNodes();
            Assert.True(area.Count == 1 && area.Contains(1));
        }
    }
}
