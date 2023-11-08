using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Maze
{
    public class MazeHuntKill : IMapProvider
    {
        private Direction[,]? _directionGrid;
        private int _gridHeight;
        private int _gridWidth;
        private List<Direction> _possibleDirections = new List<Direction>() { Direction.N, Direction.S, Direction.E, Direction.W };
        private List<MapVector> _visited = new List<MapVector>();

        private Random _rnd;

        public Direction[,] CreateMap(int width, int height)
        {
            //ensure minimum width and height
            if (width <= 4 || height <= 4) { throw new ArgumentException("Minimum Width and Height of 5"); }
            if (width % 2 == 0 || height % 2 == 0) { throw new ArgumentException("Must be odd"); }

            this._gridWidth = (width - 1) / 2;
            this._gridHeight = (height - 1) / 2;

            this._directionGrid = new Direction[_gridHeight, _gridWidth];

            var randX = _rnd.Next(_gridWidth);
            var randY = _rnd.Next(_gridHeight);

            //Call Walk
            Walk(new MapVector(randX, randY));

            

        }

        public Direction[,] CreateMap()
        {
            throw new NotImplementedException();
        }

        private void Walk(MapVector currentVector)
        {
            //get all possible directions you can walk
            List <Direction> validMovements = new List<Direction>();
            foreach(Direction dir in _possibleDirections)
            {
                var nextVector = currentVector + (MapVector)dir;
                var oppositeDir = GetReverseDirection(dir);

                if (nextVector.X >= 0 && nextVector.X < this._gridWidth && nextVector.Y >= 0 && nextVector.Y < this._gridHeight)
                {

                    if (!_visited.Contains(nextVector))
                    {
                        validMovements.Add(dir);
                    }
                }
            }

            //chose a random direction in the random directions to walk to
            if(validMovements.Count > 0)
            {
                int index = _rnd.Next(validMovements.Count);
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
