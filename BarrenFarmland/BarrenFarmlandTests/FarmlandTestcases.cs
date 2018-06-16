using NUnit.Framework;
using System.Collections.Generic;
using BarrenFarmland.Models;

namespace BarrenFarmlandTests
{
    [TestFixture]
    class FarmlandTestcases
    {
        [Test]
        public void NobarrenRegionsTestCast()
        {
            Grid Farmland = new Grid(400, 600);
            List<int> area = Farmland.FindConnectedNodes();
            Assert.True(area.Contains(240000) && area.Count == 1);
        }
        
        [Test]
        public void NonDisconnectingBarrenRegionTestCase()
        {
            Grid Farmland = new Grid(400, 600);
            Region NorthernBarrenSection = new Region(new Coordinate(99, 99), new Coordinate(199, 0));
            int FertileArea = 240000 - NorthernBarrenSection.CalculateArea();

            Farmland.ColorRegion(NorthernBarrenSection, -1);
            List<int> area = Farmland.FindConnectedNodes();
            Assert.True(area.Contains(FertileArea) && area.Count == 1);
        }

        [Test]
        public void BisectingBarrenRegionTestCase()
        {
            Grid Farmland = new Grid(400, 600);
            Region NorthSouthBisectingBarrenSection = new Region(new Coordinate(199, 599), new Coordinate(199, 0));

            Farmland.ColorRegion(NorthSouthBisectingBarrenSection, -1);
            List<int> area = Farmland.FindConnectedNodes();
            Assert.True(area.Count == 2);

            Region EastWestBisectingBarrenSection = new Region(new Coordinate(0,299), new Coordinate(399, 299));
            Farmland = new Grid(400, 600);
            Farmland.ColorRegion(EastWestBisectingBarrenSection, -1);
            area = Farmland.FindConnectedNodes();
            Assert.True(area.Count == 2);

            Farmland = new Grid(400, 600);
            Farmland.ColorRegion(NorthSouthBisectingBarrenSection, -1);
            Farmland.ColorRegion(EastWestBisectingBarrenSection, -1);
            area = Farmland.FindConnectedNodes();
            Assert.True(area.Count == 4);
        }
    }
}
