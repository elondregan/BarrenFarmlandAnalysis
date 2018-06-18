using NUnit.Framework;
using System.Collections.Generic;
using BarrenFarmland.Models;

namespace BarrenFarmlandTests
{
    /// <summary>
    /// Tests many of the grid methods in a more 
    /// </summary>
    [TestFixture]
    class FarmlandTestcases
    {
        public readonly int Barren = -1;

        /// <summary>
        /// If we make our entire farm barren, then we should fail to find any connected nodes.
        /// </summary>
        [Test]
        public void FullyBarrenRectangleTest()
        {
            Grid Farmland = new Grid(400, 600);
            Region TheGrid = new Region(new Coordinate(0, Farmland.Width), new Coordinate(Farmland.Length, 0), Barren);
            Farmland.ColorRegion(TheGrid);
            List<int> area = Farmland.FindConnectedNodes();
            Assert.True(Farmland.FindConnectedNodes().Count == 1 && area.Contains(0));
        }

        /// <summary>
        /// Verifies that the FindConnectedNodes method visits every vertex correctly.
        /// </summary>
        [Test]
        public void NobarrenRegionsTestCast()
        {
            Grid Farmland = new Grid(400, 600);
            List<int> area = Farmland.FindConnectedNodes();
            Assert.True(area.Contains((Farmland.Length + 1) * (Farmland.Width + 1)) && area.Count == 1);
        }
        
        /// <summary>
        /// We are just taking a small slice out of the northern edge and making sure the algorithm succesfully hits everything.
        /// </summary>
        [Test]
        public void NonDisconnectingBarrenRegionTestCase()
        {
            Grid Farmland = new Grid(400, 600);
            Region NorthernBarrenSection = new Region(new Coordinate(99, 99), new Coordinate(199, 0), Barren);
            int FertileArea = ((Farmland.Width +1) * (Farmland.Length + 1)) - NorthernBarrenSection.CalculateArea();

            Farmland.ColorRegion(NorthernBarrenSection);
            List<int> area = Farmland.FindConnectedNodes();
            Assert.True(area.Contains(FertileArea) && area.Count == 1);
        }

        /// <summary>
        /// Form a cross in the middle of the grid, make sure we end up with the correct amount of separation throughout.
        /// </summary>
        [Test]
        public void BisectingBarrenRegionTestCase()
        {
            Grid Farmland = new Grid(400, 600);
            Region NorthSouthBisectingBarrenSection = new Region(new Coordinate(200, 599), new Coordinate(200, 0), Barren);

            Farmland.ColorRegion(NorthSouthBisectingBarrenSection);
            List<int> area = Farmland.FindConnectedNodes();
            Assert.True(area.Count == 2);

            Region EastWestBisectingBarrenSection = new Region(new Coordinate(0,300), new Coordinate(399, 300), Barren);
            Farmland.ColorRegion(EastWestBisectingBarrenSection);
            area = Farmland.FindConnectedNodes();
            Assert.True(area.Count == 4);
        }

        /// <summary>
        /// Form a tic-tac-toe lattice accross the grid, we should have chunks of size of 1 and 25% of the entire grid
        /// </summary>
        [Test]
        public void StripesOfBarrenRegionTestCase()
        {
            Grid Farmland = new Grid(400, 600);
            
            for (int x = 0; x <= Farmland.Length; x += 2)
            {
                Coordinate regionVerticleStripesBottomLeft = new Coordinate(x, Farmland.Width);
                Coordinate regionVerticleStripesTopRight = new Coordinate(x, 0);

                Farmland.ColorRegion(new Region(regionVerticleStripesBottomLeft, regionVerticleStripesTopRight, Barren));
            }

            List<int> area = Farmland.FindConnectedNodes();
            Assert.True(area.Count == ((Farmland.Length + 1) / 2));
            foreach(int subArea in area)
            {
                Assert.True(subArea == Farmland.Width + 1);
            }

            for(int y = 0; y <= Farmland.Width; y+= 2)
            {
                Coordinate regionHorizontalStripesBottomLeft = new Coordinate(0,y);
                Coordinate regionHorizontalStripesTopRight = new Coordinate(Farmland.Length, y);

                Farmland.ColorRegion(new Region(regionHorizontalStripesBottomLeft, regionHorizontalStripesTopRight, Barren));
            }

            area = Farmland.FindConnectedNodes();
            Assert.True(area.Count == (((Farmland.Width + 1)  * (Farmland.Length + 1))/ 4));
            foreach(int subArea in area)
            {
                Assert.True(subArea == 1);
            }
        }
    }
}
