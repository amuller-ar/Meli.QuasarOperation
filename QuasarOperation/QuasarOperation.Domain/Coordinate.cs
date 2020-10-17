namespace QuasarOperation.Domain
{
    public class Coordinate
    {
        public double X { get; set; }
        public double Y { get; set; }
        public Coordinate()
        {
        }

        public Coordinate(double x,double y)
        {
            X = x;
            Y = y;
        }
    }
}