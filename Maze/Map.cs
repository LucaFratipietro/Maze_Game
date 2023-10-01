using System.ComponentModel.DataAnnotations;

namespace Maze
{
    public class Map : IMap
    {

        //IMapProvider field
        private IMapProvider _provider;

        public Map(IMapProvider mapProvider)
        {
            this._provider = mapProvider;
        }

        public MapVector Goal { get; private set; }

        public int Height { get; private set; }

        public bool IsGameFinished { get; private set; }

        public Block[,] MapGrid { get; private set; }

        public IPlayer Player { get; private set; }

        public int Width { get; private set; }

        public void CreateMap()
        {
            Direction[,] directionGrid = _provider.CreateMap();

            //Determine width and height of maze
            this.Width = (directionGrid.GetLength(1) * 2) + 1; // 9
            this.Height = (directionGrid.GetLength(0) * 2) + 1; // 7

            //Set this MapGrid to match Height and width of maze
            this.MapGrid = new Block[this.Width, this.Height]; // CHANGE

            //Set all blocks as solid
            for (int i = 0; i < this.Width; i++)
            {
                for (int j = 0; j < this.Height; j++)
                {
                    this.MapGrid[i, j] = Block.Solid;
                }
            }

            //loops through coloums and rows and uses directionGrid to determine which blocks are empty
            setEmptyBlocks(directionGrid);

            // Generates a Player object and tries to place it in a legal positon

            placePlayer();

            //places a goal sufficiently far away from the player at a dead end

            placeEndGoal(directionGrid);

        }


        //takes this class mapGird of Block.Solid and uses the directionGrid of Direction enums to determine which positions should be empty 
        private void setEmptyBlocks(Direction[,] directionGrid)
        {
            int directionMapX = 0;
            int directionMapY = 0;

            for (int i = 1; i < this.Height; i += 2)
            {
                for (int j = 1; j < this.Width; j += 2)
                {

                    //sets this position on the MapGrid as empty
                    this.MapGrid[j, i] = Block.Empty;

                    var dir = directionGrid[directionMapX, directionMapY];

                    var isEast = (dir & Direction.E) > 0;
                    var isSouth = (dir & Direction.S) > 0;

                    if (isEast)
                    {
                        this.MapGrid[j + 1, i] = Block.Empty;
                    }
                    if (isSouth)
                    {
                        this.MapGrid[j, i + 1] = Block.Empty;
                    }
                    directionMapY++;
                }

                directionMapY = 0;
                directionMapX++;
            }
        }

        private void placePlayer()
        {
            // Generates a Player object and tries to place it in a legal positon

            Player player = new Player(this.MapGrid);
            bool legal = false;
            int lowerBound = 1;
            int YUpperBound = this.Height - 1;
            int XUpperBound = this.Width - 1;
            var random = new Random();

            while (!legal)
            {
                var randY = random.Next(lowerBound, YUpperBound);
                var randX = random.Next(lowerBound, XUpperBound);

                if (this.MapGrid[randX, randY] == Block.Empty)
                {
                    legal = true;
                    player.StartX = randX;
                    player.StartY = randY;
                    player.Position = new MapVector(randY, randX);
                    this.Player = player;
                }

            }
        }


        //DETERMINE GOAL POSITION
        //finds all spots in directionGrid with only 1 move possible,
        //Determines magnitude between that position and player, point with largest magnitude becomes the goal
        private void placeEndGoal(Direction[,] directionGrid)
        {
            int goalX = 0;
            int goalY = 0;
            double goalDistance = 0;
            int directionMapX = 0;
            int directionMapY = 0;

            for (int i = 1; i < this.Height; i += 2)
            {
                for (int j = 1; j < this.Width; j += 2)
                {

                    var dir = directionGrid[directionMapX, directionMapY];

                    //if direction can only go W or N, it is a dead end
                    if ((dir ^ Direction.N) == 0 || (dir ^ Direction.W) == 0)
                    {
                        //determine the distance between this point and player position
                        double distance = Math.Sqrt((i - this.Player.StartX) * (i - this.Player.StartX) + (j - this.Player.StartY) * (j - this.Player.StartY));
                        if (distance > goalDistance)
                        {
                            goalDistance = distance;
                            goalX = j;
                            goalY = i;
                        }

                    }

                    directionMapY++;
                }

                directionMapY = 0;
                directionMapX++;
            }

            this.Goal = new MapVector(goalX, goalY);
        }

        public void CreateMap(int width, int height)
        {
            throw new NotImplementedException();
        }

        public void SaveDirectionMap(string path)
        {
            throw new NotImplementedException();
        }
    }
}
