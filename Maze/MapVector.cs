namespace Maze
{
    public class MapVector : IMapVector
    {
        public bool IsValid { get; private set; }

        public int X { get; private set; }

        public int Y { get; private set; }

        public bool InsideBoundary(int width, int height)
        {
           throw new NotImplementedException();
        }

        public double Magnitude()
        {
            throw new NotImplementedException();
        }
    }
}
