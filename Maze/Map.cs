namespace Maze
{
    public class Map : IMap
    {
        public MapVector Goal => throw new NotImplementedException();

        public int Height => throw new NotImplementedException();

        public bool IsGameFinished => throw new NotImplementedException();

        public Block[,] MapGrid => throw new NotImplementedException();

        public IPlayer Player => throw new NotImplementedException();

        public int Width => throw new NotImplementedException();

        public void CreateMap()
        {
            throw new NotImplementedException();
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
