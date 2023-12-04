using System.Diagnostics;
using Maze;
using MazeHuntKillSpace;

namespace performance
{
    public class Benchmarking
    {
        public static void Main(string[] args)
        {

            //get both IMaps using the factory methods
            IMapProvider Recmap = IMapFactory.MapFactory(null, "Recursion");

            //first test the hunt algo 
            Console.WriteLine("Hunt Algo Tests starting... \n");
            for (int i = 0; i < 50; i+= 2)
            {
                Console.WriteLine($"{i + 5} by {i + 5} tests");
                int runs = 0;
                List<double> results = new List<double>();
                while (runs < 5)
                {
                    IMapProvider Huntmap = IMapFactory.MapFactory(null, "Hunt");
                    TimeSpan span = Timeit(() => { Huntmap.CreateMap(i+5, i+5); });
                    results.Add(span.TotalMilliseconds);
                    runs++;
                }
                Console.WriteLine("Average timespan: " + results.Average() + "\n");
            }
        }

        public static TimeSpan Timeit(Action bench)
        {
            var timer = new Stopwatch();
            timer.Start();
            bench.Invoke();
            timer.Stop();
            TimeSpan timeTaken = timer.Elapsed;
            return timeTaken;
        }

        private void WriteToFile(string path, string data)
        {

        }

    }
}
