using System;
using System.Numerics;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Intrinsics;

namespace Maze
{
    public class RecursionMap : IMapProvider
    {
        private Direction[,]? _directionGrid;
        private int gridWidth;
        private int gridHeight;
        private List<Direction> possibleDirections = new List<Direction>() { Direction.N, Direction.S, Direction.E, Direction.W };

        private static Random rnd = new Random();
        private List<MapVector> _visited = new List<MapVector>();
        
        public RecursionMap() { }
        public Direction[,] CreateMap(int width, int height)
        {
            //ensure minimum width and height
            if(width <= 4 || height <= 4) { throw new Exception("Minimum Width and Height of 5"); }
            if(width % 2 == 0 || height % 2 == 0) { throw new Exception("Must be odd"); }
            
            this.gridWidth = (width - 1) / 2;
            this.gridHeight = (height - 1) / 2;

            this._directionGrid = new Direction[gridHeight, gridWidth];

            //Call recursive Walk function to fill directionGrid
            var randX = rnd.Next(gridWidth);
            var randY = rnd.Next(gridHeight);
            Walk(new MapVector(randX,randY));

            //once done walking, return populated grid
            return this._directionGrid;
            
        }

        public Direction[,] CreateMap()
        {
            //just call CreateMap with a static width and height
            return CreateMap(7, 7);
        }

        //Recusive Walking algorithm that populates the directionGrid
        private void Walk(MapVector currentVector)
        {

            //add vector to private visited list
            _visited.Add(currentVector);
            //shuffle list of directions
            possibleDirections = this.possibleDirections.OrderBy(x => rnd.Next()).ToList();

            foreach(Direction dir in possibleDirections) 
            {
                var nextVector = currentVector + (MapVector)dir;
                var oppositeDir = GetReverseDirection(dir);

                if (nextVector.X >= 0 && nextVector.X < this.gridWidth && nextVector.Y >= 0 && nextVector.Y < this.gridHeight)
                {

                    if (!_visited.Contains(nextVector))
                    {
                        _directionGrid[currentVector.Y, currentVector.X] |= dir;
                        _directionGrid[nextVector.Y, nextVector.X] |= oppositeDir;

                        Walk(nextVector);
                    }
                }
            }
        }
        
        private Direction GetReverseDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.N:
                    return Direction.S;
                case Direction.S:
                    return Direction.N;
                case Direction.E:
                    return Direction.W;
                case Direction.W:
                    return Direction.E;
                default:
                    return Direction.None;
            }
        }
    }
}
