using System;
using BarrenFarmland.Models;
using NUnit.Framework;

namespace BarrenFarmlandTests
{
    [TestFixture]
    public class RegionTests
    {

        [Test]
        public void StandardRegionAreaTest()
        {
            Coordinate SizeTenAreaBottomLeft = new Coordinate(0, 9);
            Coordinate SizeTenAreaTopRight = new Coordinate(9, 0);

            Region SizeOneHundredArea = new Region(SizeTenAreaBottomLeft, SizeTenAreaTopRight);
            int ExpectedAreaOfOneHundred = SizeOneHundredArea.CalculateArea();

            Assert.IsTrue(ExpectedAreaOfOneHundred == 100);
        }

        [Test]
        public void SinglePointRegionAreaTest()
        {
            Coordinate SizeOneRegion = new Coordinate(0, 0);

            Region SizeOneArea = new Region(SizeOneRegion, SizeOneRegion);
            int ExpectedAreaOfOne = SizeOneArea.CalculateArea();

            Assert.IsTrue(ExpectedAreaOfOne == 1);
        }

        [Test]
        public void AllBoundaryPointsRegionAreaTest()
        {
            Region SizeTwoByTwoRegion = new Region(new Coordinate(0, 1), new Coordinate(1, 0));
            int ExpectedAreaOfFour = SizeTwoByTwoRegion.CalculateArea();

            Assert.IsTrue(ExpectedAreaOfFour == 4);
        }

        [Test]
        public void InvalidAreaDefintion()
        {
            Coordinate SizeTenAreaBottomLeft = new Coordinate(0, 9);
            Coordinate SizeTenAreaTopRight = new Coordinate(9, 0);

            Assert.Throws<ArgumentOutOfRangeException>(() => new Region(SizeTenAreaTopRight, SizeTenAreaBottomLeft));
        }
    }
}
