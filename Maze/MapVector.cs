using System.Reflection.Metadata.Ecma335;

namespace Maze
{
    public class MapVector : IMapVector
    {

        public MapVector(int  x, int y)
        {
            this.X = x; this.Y = y;
        }

        public bool IsValid { get; private set; }

        public int X { get; private set; }

        public int Y { get; private set; }

        public bool InsideBoundary(int width, int height)
        {
            // if x or y position of vector is negative, its outside the boundry

           if (this.X < 0 || this.Y < 0)
            {
                this.IsValid = false;
                return false;
            }

           // if x or y position of the MapVector is greater than the map height, its outside

           if (this.X >= width || this.Y >= height)
            {
                this.IsValid = false;
                return false;
            }

           return true;


        }

        public double Magnitude()
        {
            return Math.Sqrt((this.X * this.X) + (this.Y * this.Y));
        }

        //adding and subtracting operators

        public static MapVector operator + (MapVector v1, MapVector v2)
        {
            int newX, newY;
            newX = v1.X + v2.X;
            newY = v1.Y + v2.Y;
            return new MapVector(newX, newY);
        }

        public static MapVector operator - (MapVector v1, MapVector v2)
        {
            int newX, newY;
            newX = v1.X - v2.X;
            newY = v1.Y - v2.Y;
            return new MapVector(newX, newY);
        }

    }
}
