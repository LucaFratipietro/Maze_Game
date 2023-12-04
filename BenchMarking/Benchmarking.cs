using System.Diagnostics;
using Maze;
using MazeHuntKillSpace;

using System.IO;
using System.Text;

namespace performance
{
    public class Benchmarking
    {
        public static void Main(string[] args)
        {

            //find path to results file, will write to this later
            string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string huntFile = System.IO.Path.Combine(sCurrentDirectory, $@"..\..\..\..\BenchMarking\results\hunt.csv");
            string hunt_results_path = Path.GetFullPath(huntFile);

            //find path to results file, will write to this later
            string recFile = System.IO.Path.Combine(sCurrentDirectory, $@"..\..\..\..\BenchMarking\results\rec.csv");
            string rec_results_path = Path.GetFullPath(recFile);

            //delete files if they already exist
            DeleteFile(hunt_results_path);
            DeleteFile(rec_results_path);

            //first test the hunt algo 
            Console.WriteLine("Hunt Algo Tests starting... \n");
            for (int i = 0; i < 200; i += 2)
            {
                Console.WriteLine($"{i + 5} by {i + 5} tests");
                int runs = 0;
                List<double> results = new List<double>();
                while (runs < 5)
                {
                    IMapProvider Huntmap = IMapFactory.MapFactory(null, "Hunt");
                    TimeSpan span = Timeit(() => { Huntmap.CreateMap(i + 5, i + 5); });
                    results.Add(span.TotalMilliseconds);
                    runs++;
                }
                Console.WriteLine("Average timespan: " + results.Average() + "\n");

                //append data to csv
                string dataToAppend = CSVStringBuilder($"{i + 5}", $"{results.Average()}");
                WriteToFile(hunt_results_path, dataToAppend);
            }

            // then recursive algo
            Console.WriteLine("Recursive Algo Tests starting... \n");
            for (int i = 0; i < 200; i += 2)
            {
                Console.WriteLine($"{i + 5} by {i + 5} tests");
                int runs = 0;
                List<double> results = new List<double>();
                while (runs < 5)
                {
                    IMapProvider Huntmap = IMapFactory.MapFactory(null, "Recursion");
                    TimeSpan span = Timeit(() => { Huntmap.CreateMap(i + 5, i + 5); });
                    results.Add(span.TotalMilliseconds);
                    runs++;
                }
                Console.WriteLine("Average timespan: " + results.Average() + "\n");

                //append data to csv
                string dataToAppend = CSVStringBuilder($"{i + 5}", $"{results.Average()}");
                WriteToFile(rec_results_path, dataToAppend);
            }
        }

        private static TimeSpan Timeit(Action bench)
        {
            var timer = new Stopwatch();
            timer.Start();
            bench.Invoke();
            timer.Stop();
            TimeSpan timeTaken = timer.Elapsed;
            return timeTaken;
        }

        private static void WriteToFile(string path, string data)
        {
            try
            {
                File.AppendAllText(path, data);
                Console.WriteLine("Data succesfully added to csv");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Data could not be written to the CSV file.");
                return;
            }
        }

        private static string CSVStringBuilder(string size, string average)
        {
            String separator = ",";
            StringBuilder output = new StringBuilder();
            String[] headings = { size, average };
            output.AppendLine(string.Join(separator, headings));

            return output.ToString();
        }

        private static void DeleteFile(string path)
        {
            try
            {
                // Check if file exists with its full path
                if (File.Exists(path))
                {
                    // If file found, delete it
                    File.Delete(path);
                    Console.WriteLine("File deleted.");
                }
                else Console.WriteLine("File not found");
            }
            catch (IOException ioExp)
            {
                Console.WriteLine(ioExp.Message);
            }
        }

    }
}
