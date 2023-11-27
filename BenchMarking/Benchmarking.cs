using Maze;
using MazeHuntKillSpace;

namespace performance
{
    public class Benchmarking
    {

        public static void Main(string[] args)
        {
            //get both IMaps using the factory methods
            Map Huntmap = new Map(IMapFactory.MapFactory(null, "Hunt"));
            Map Recmap = new Map(IMapFactory.MapFactory(null, "Recursion"));
        }

        public TimeSpan Timeit(Action bench)
        {

        }

        public void WriteToFile(string path, string data)
        {

        }
    }
}