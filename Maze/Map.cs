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
