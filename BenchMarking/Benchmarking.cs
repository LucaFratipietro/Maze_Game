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

                int runs = 0;
                while (runs < 1000) {
                TimeSpan span = Timeit(() => { Huntmap.CreateMap(9, 9) };
            }

            private TimeSpan Timeit(Action bench)
            {

            }

            private void WriteToFile(string path, string data)
            {

            }
    }
    }
