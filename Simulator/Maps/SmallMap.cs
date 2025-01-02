namespace Simulator.Maps
{
    public class SmallMap : Map
    {
        public int SizeX { get; }
        public int SizeY { get; }
        public override bool Exist(Point p)
        {
            throw new NotImplementedException();
        }

        public override Point Next(Point p, Direction d)
        {
            throw new NotImplementedException();
        }

        public override Point NextDiagonal(Point p, Direction d)
        {
            throw new NotImplementedException();
        }

        public SmallMap(int sizeX, int sizeY)
        {
            if (sizeX < 5 || sizeX > 20)
            {
                throw new ArgumentOutOfRangeException(nameof(sizeX), "Size of vector X is out of respective range (5 to 20)");
            }
            if (sizeY < 5 || sizeY > 20)
            {
                throw new ArgumentOutOfRangeException(nameof(sizeY), "Size of vector Y is out of respective range (5 to 20)");
            }
            SizeX = sizeX;
            SizeY = sizeY;
        }
    }
}
