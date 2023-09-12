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
            this.Width = (directionGrid.GetLength(1) * 2) + 1;
            this.Height = (directionGrid.GetLength(0) * 2) + 1;

            //Set this MapGrid to match Height and width of maze
            this.MapGrid = new Block[this.Height, this.Width];

            //Set all blocks as solid
            for (int i = 0; i < this.Height; i++)
            {
                for(int j =  0; j < this.Width; j++)
                {
                    this.MapGrid[i, j] = Block.Solid;
                }
            }

            //loops through coloums and rows and uses directionGrid to determine which blocks are empty

            int directionMapY = 0;
            int directionMapX = 0;

            for (int i = 1; i < this.Height; i+= 2)
            {
                for(int j = 1; j < this.Width; j+= 2)
                {

                    //sets this position on the MapGrid as empty
                    this.MapGrid[i, j] = Block.Empty;

                    var dir = directionGrid[directionMapY, directionMapX];

                    var isEast = (dir & Direction.E) > 0;
                    var isSouth = (dir & Direction.S) > 0;

                    if (isEast)
                    {
                        this.MapGrid[i, j + 1] = Block.Empty;
                    }
                    if (isSouth) 
                    {
                        this.MapGrid[i + 1, j] = Block.Empty;
                    }
                    directionMapX++;
                }
             
                directionMapX = 0;
                directionMapY++;
            }

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
