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

            this.directionGrid = new Direction[gridWidth, gridHeight];

            //Call recursive Walk function to fill directionGrid
            var randX = rnd.Next(gridWidth);
            var randY = rnd.Next(gridHeight);
            List<MapVector> visitedPositions = new List<MapVector>() { new MapVector(randX, randY) };
            Walk2(0, visitedPositions);

            //once done walking, return populated grid

            return this.directionGrid;
            
        }

        public Direction[,] CreateMap()
        {
            //later, just call CreateMap with a static width and height
            throw new NotImplementedException();
        }

        //bigger and better
        private void Walk2(int currentIndex, List<MapVector> visitedPositions)
        {
            if (currentIndex < 0)
            {
                //once weve returned to the original start position, and have no where to go, return
                return;
            }
            //shuffle list of directions
            List<Direction> shuffleDir = this.possibleDirections.OrderBy(x => Random.Shared.Next()).ToList();

            for(int i = 0; i < shuffleDir.Count; i++)
            {
                //see if we can move to direction in shuffleDir, if no, move onto next one
                MapVector nextPosition = visitedPositions[currentIndex] + (MapVector)shuffleDir[i];
                if(nextPosition.InsideBoundary(this.gridWidth, this.gridHeight))
                {
                    if (!visitedPositions.Contains(nextPosition))
                    {
                        visitedPositions.Add(nextPosition);
                        this.directionGrid[visitedPositions[currentIndex].X, visitedPositions[currentIndex].Y] = this.directionGrid[visitedPositions[currentIndex].X, visitedPositions[currentIndex].Y] | shuffleDir[i];
                        switch (shuffleDir[i])
                        {
                            case Direction.N:
                                this.directionGrid[nextPosition.X, nextPosition.Y] = this.directionGrid[nextPosition.X, nextPosition.Y] | Direction.S; break;
                            case Direction.S:
                                this.directionGrid[nextPosition.X, nextPosition.Y] = this.directionGrid[nextPosition.X, nextPosition.Y] | Direction.N; break;
                            case Direction.E:
                                this.directionGrid[nextPosition.X, nextPosition.Y] = this.directionGrid[nextPosition.X, nextPosition.Y] | Direction.W; break;
                            case Direction.W:
                                this.directionGrid[nextPosition.X, nextPosition.Y] = this.directionGrid[nextPosition.X, nextPosition.Y] | Direction.E; break;
                        }
                        Walk2(currentIndex + 1, visitedPositions);
                    }
                }
            }
            //if none of the 4 directions worked, move back an index and recall Walk2
            Walk2(currentIndex - 1, visitedPositions);
        }

        private void Walk()
        {
            int count = 0; //if we cant find legal start in 100 attemps, just call it
            List<Direction> possibleDirections = new List<Direction>() { Direction.N, Direction.E, Direction.S, Direction.W};

            while (count < 100)
            {
                var randX = rnd.Next(gridWidth - 1);
                var randY = rnd.Next(gridHeight - 1);

                if ((this.directionGrid[randX, randY] ^ Direction.None) == 0)
                {   
                    MapVector currentPosition = new MapVector(randX, randY);
                    HashSet<MapVector> visited = new HashSet<MapVector>() { currentPosition };
                    bool looping = true;
                    count = 0;

                    while (looping) {
                        //pick random direction
                        Direction randomDirection = possibleDirections[rnd.Next(possibleDirections.Count)];

                        MapVector movement = (MapVector)randomDirection;

                        MapVector nextPosition = currentPosition + movement;

                        if(!nextPosition.InsideBoundary(this.gridWidth, this.gridHeight))
                        {
                            looping = false;
                            break;
                        }

                        if(visited.Add(nextPosition))
                        {
                            this.directionGrid[randX, randY] = this.directionGrid[randX, randY] | randomDirection;
                            //uh idk lol big switch for now
                            switch(randomDirection)
                            {
                                case Direction.N:
                                    this.directionGrid[nextPosition.X, nextPosition.Y] = this.directionGrid[nextPosition.X, nextPosition.Y] | Direction.S; break;
                                case Direction.S:
                                    this.directionGrid[nextPosition.X, nextPosition.Y] = this.directionGrid[nextPosition.X, nextPosition.Y] | Direction.N; break;
                                case Direction.E:
                                    this.directionGrid[nextPosition.X, nextPosition.Y] = this.directionGrid[nextPosition.X, nextPosition.Y] | Direction.W; break;
                                case Direction.W:
                                    this.directionGrid[nextPosition.X, nextPosition.Y] = this.directionGrid[nextPosition.X, nextPosition.Y] | Direction.E; break;
                            }

                            currentPosition = nextPosition;
                        }
                        else
                        {
                            looping = false;
                        }
                    }
                    Walk();
                }
                else
                {
                    count++;
                }
            }
        }
        
        
    }
}
