using System;
using System.Collections.Generic;
using NUnit.Framework;
using BarrenFarmland;
using BarrenFarmland.Models;

namespace BarrenFarmlandTests
{
    [TestFixture]
    public class GridTests
    {

        [Test]
        public void GridConstructorMakesCorrectSizedArray()
        {
            Grid FiveByFive = new Grid(5, 5);

            Assert.IsTrue(FiveByFive.GridRepresentation.Length == 25);
        }

        [Test]
        public void GridConstructorNegativeValueArgumentException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Grid(-5, 5));
        }

        [Test]
        public void PaintFullGrid()
        {
            Grid FiveByFive = new Grid(5, 5);

            Region FullGridEncompassed = new Region(new Coordinate(0, 4), new Coordinate(4, 0));

            FiveByFive.ColorRegion(FullGridEncompassed, -1);
            foreach (int point in FiveByFive.GridRepresentation)
            {
                Assert.IsTrue(point == -1);
            }
        }

        [Test]
        public void OutOfBoundsRegionColorTest()
        {
            Grid FiveByFive = new Grid(5, 5);
            Region OutOfBoundsPoint = new Region(new Coordinate(0, 5), new Coordinate(0, 5));

            Assert.IsTrue(FiveByFive.ColorRegion(OutOfBoundsPoint, 5) == false);
        }

        [Test]
        public void InBoundsPointColorTest()
        {
            Grid FiveByFive = new Grid(5, 5);
            Region origin = new Region(new Coordinate(0, 0), new Coordinate(0, 0));

            FiveByFive.ColorRegion(origin, -1);
            Assert.IsTrue(FiveByFive.GridRepresentation[0, 0] == -1);
        }

        [Test]
        public void WholeRectangleColorTest()
        {
            Grid ThreeByFour = new Grid(3, 4);
            Region TheGrid = new Region(new Coordinate(0, 3), new Coordinate(2, 0));
            ThreeByFour.ColorRegion(TheGrid, -1);
            foreach(int gridValue in ThreeByFour.GridRepresentation)
            {
                Assert.True(gridValue == -1);
            }

            Grid FourByThree = new Grid(4, 3);
            TheGrid = new Region(new Coordinate(0, 2), new Coordinate(3, 0));
            FourByThree.ColorRegion(TheGrid, -1);
            foreach (int gridValue in FourByThree.GridRepresentation)
            {
                Assert.True(gridValue == -1);
            }
        }

        [Test]
        public void FindOnePointGridNeighbors()
        {
            Grid OneByOne = new Grid(1, 1);
            Coordinate origin = new Coordinate(0, 0);
            Assert.True(OneByOne.FindUnvisitedNeighbors(origin) != null);
        }

        [Test]
        public void FullyBarrenRectangleTest()
        {
            Grid ThreeByFour = new Grid(3, 4);
            Region TheGrid = new Region(new Coordinate(0, 3), new Coordinate(2, 0));
            ThreeByFour.ColorRegion(TheGrid, -1);
            List<int> area = ThreeByFour.FindConnectedNodes();
            Assert.True(ThreeByFour.FindConnectedNodes().Count == 0);
        }

        [Test]
        public void OneFertileCellTest()
        {
            Grid ThreeByThree = new Grid(3, 3);
            Region TwobyThreeSlice = new Region(new Coordinate(0, 2), new Coordinate(1, 0));
            Region OneByTwoSlice = new Region(new Coordinate(2, 1), new Coordinate(2, 0));

            ThreeByThree.ColorRegion(TwobyThreeSlice, -1);
            ThreeByThree.ColorRegion(OneByTwoSlice, -1);

            List<int> area = ThreeByThree.FindConnectedNodes();
            Assert.True(area.Count == 1 && area.Contains(1));
        }
    }
}
