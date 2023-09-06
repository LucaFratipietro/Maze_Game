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
