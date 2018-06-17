using System;
using BarrenFarmland.Models;
using NUnit.Framework;

namespace BarrenFarmlandTests
{
    [TestFixture]
    public class RegionTests
    {
        /// <summary>
        /// Take an arbitrary area and make sure that it has the expected area.
        /// </summary>
        [Test]
        public void StandardRegionAreaTest()
        {
            Coordinate SizeTenAreaBottomLeft = new Coordinate(0, 9);
            Coordinate SizeTenAreaTopRight = new Coordinate(9, 0);

            Region SizeOneHundredArea = new Region(SizeTenAreaBottomLeft, SizeTenAreaTopRight, 1);
            int ExpectedAreaOfOneHundred = SizeOneHundredArea.CalculateArea();

            Assert.IsTrue(ExpectedAreaOfOneHundred == 100);
        }

        /// <summary>
        /// If our region is a single point, make sure it still has an area. 
        /// </summary>
        [Test]
        public void SinglePointRegionAreaTest()
        {
            Coordinate SizeOneRegion = new Coordinate(0, 0);

            Region SizeOneArea = new Region(SizeOneRegion, SizeOneRegion, 1);
            int ExpectedAreaOfOne = SizeOneArea.CalculateArea();

            Assert.IsTrue(ExpectedAreaOfOne == 1);
        }

        /// <summary>
        /// This makes sure that all edges are included in the area rather than having unexpected edges.
        /// </summary>
        [Test]
        public void AllBoundaryPointsRegionAreaTest()
        {
            Region SizeTwoByTwoRegion = new Region(new Coordinate(0, 1), new Coordinate(1, 0),1);
            int ExpectedAreaOfFour = SizeTwoByTwoRegion.CalculateArea();

            Assert.IsTrue(ExpectedAreaOfFour == 4);
        }

        /// <summary>
        /// If the BottomLeft and TopRight cannot exist, ergo the Bottomleft has a a smaller Y value than TopRight and the opposite for X Values.
        /// </summary>
        [Test]
        public void InvalidAreaDefintion()
        {
            Coordinate SizeTenAreaTopRight = new Coordinate(0, 9);
            Coordinate SizeTenAreaBottomLeft = new Coordinate(9, 0);

            Assert.Throws<ArgumentOutOfRangeException>(() => new Region(SizeTenAreaBottomLeft, SizeTenAreaTopRight,  1));
        }
    }
}
