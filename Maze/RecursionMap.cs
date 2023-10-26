using System;
using System.Runtime.ConstrainedExecution;

namespace Maze
{
    public class RecursionMap : IMapProvider
    {
        private Direction[,]? directionGrid;
        private int gridWidth;
        private int gridHeight;

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
            Walk();

            //once done walking, return populated grid

            return this.directionGrid;
            
        }

        public Direction[,] CreateMap()
        {
            //later, just call CreateMap with a static width and height
            throw new NotImplementedException();
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
