namespace BarrenFarmland.Models
{

    public class Coordinate
    {
        public int XValue { get; private set; }
        public int YValue { get; private set; }

        public Coordinate(int xValue, int yValue)
        {
            this.XValue = xValue;
            this.YValue = yValue;
        }
    }
}
