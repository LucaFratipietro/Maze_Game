using System;
using System.Runtime.ConstrainedExecution;

namespace Maze
{
    public class RecursionMap : IMapProvider
    {
        private Direction[,]? directionGrid;
        private int gridWidth;
        private int gridHeight;
        private List<Direction> possibleDirections = new List<Direction>() { Direction.N, Direction.S, Direction.E, Direction.W };

        private static Random rnd = new Random();
        
        public RecursionMap() { }
        public Direction[,] CreateMap(int width, int height)
        {
            //ensure minimum width and height
            if(width <= 4 || height <= 4) { throw new Exception("Minimum Width and Height of 5"); }
            if(width % 2 == 0 || height % 2 == 0) { throw new Exception("Must be odd"); }
            
            this.gridWidth = (width - 1) / 2;
            this.gridHeight = (height - 1) / 2;

            this.directionGrid = new Direction[gridHeight, gridWidth];

            //Call recursive Walk function to fill directionGrid
            var randX = rnd.Next(gridWidth);
            var randY = rnd.Next(gridHeight);
            List<MapVector> visitedPositions = new List<MapVector>() { new MapVector(randX, randY) };
            Walk(0, visitedPositions);

            //once done walking, return populated grid
            return this.directionGrid;
            
        }

        public Direction[,] CreateMap()
        {
            //just call CreateMap with a static width and height
            return CreateMap(7, 7);
        }

        //Recusive Walking algorithm that populates the directionGrid
        private void Walk(int currentIndex, List<MapVector> visitedPositions)
        {

            if (currentIndex < 0)
            {
                //once weve returned to the original start position, and have no where to go, return
                return;
            }
            //shuffle list of directions
            List<Direction> shuffleDir = this.possibleDirections.OrderBy(x => Random.Shared.Next()).ToList();

            for (int i = 0; i < shuffleDir.Count; i++)
            {
                //see if we can move to direction in shuffleDir, if no, move onto next one
                MapVector nextPosition = visitedPositions[currentIndex] + (MapVector)shuffleDir[i];
                if (nextPosition.InsideBoundary(this.gridWidth, this.gridHeight))
                {
                    if (!visitedPositions.Contains(nextPosition))
                    {
                        visitedPositions.Add(nextPosition);
                        this.directionGrid[visitedPositions[currentIndex].Y, visitedPositions[currentIndex].X] = this.directionGrid[visitedPositions[currentIndex].Y, visitedPositions[currentIndex].X] | shuffleDir[i];
                        switch (shuffleDir[i])
                        {
                            case Direction.N:
                                this.directionGrid[nextPosition.Y, nextPosition.X] = this.directionGrid[nextPosition.Y, nextPosition.X] | Direction.S; break;
                            case Direction.S:
                                this.directionGrid[nextPosition.Y, nextPosition.X] = this.directionGrid[nextPosition.Y, nextPosition.X] | Direction.N; break;
                            case Direction.E:
                                this.directionGrid[nextPosition.Y, nextPosition.X] = this.directionGrid[nextPosition.Y, nextPosition.X] | Direction.W; break;
                            case Direction.W:
                                this.directionGrid[nextPosition.Y, nextPosition.X] = this.directionGrid[nextPosition.Y, nextPosition.X] | Direction.E; break;
                        }
                        Walk(currentIndex + 1, visitedPositions);
                    }
                }
            }
            //if none of the 4 directions worked, move back an index and recall Walk2
            Walk(currentIndex - 1, visitedPositions);
        }
    }
}
