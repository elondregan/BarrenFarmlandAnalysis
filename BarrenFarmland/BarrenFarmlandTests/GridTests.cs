using System;
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
            foreach(int point in FiveByFive.GridRepresentation)
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
    }
}
