using MazeFromFile;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Maze
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Map Generator!");

            List<double> results = new List<double>();
            for (int i = 0; i < 10; i++)
            {
                IMapProvider imap = new MazeRecImprovments(null);
                var timer = new Stopwatch();
                timer.Start();
                imap.CreateMap(151, 151);
                timer.Stop();
                TimeSpan timeTaken = timer.Elapsed;

                results.Add(timeTaken.TotalMilliseconds);
            }
            
         
            Console.WriteLine("Maps done");
            Console.WriteLine("Time: " + results.Average());

            /*Console.WriteLine("Please type the full name of the map you want to load (with file exstension, and place it in the root folder of this project to make this a bit easier) ");

            bool validPath = false;
            while (!validPath)
            {
                Console.Write("Map file name here: ");
                string mapName = Console.ReadLine();
                if (String.IsNullOrEmpty(mapName))
                {
                    Console.WriteLine("Please actually type something");
                    continue;
                }
                try
                {
                    //find path to file
                    string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    string sFile = System.IO.Path.Combine(sCurrentDirectory, $@"..\..\..\..\map9x7.txt");
                    string sFilePath = Path.GetFullPath(sFile);

                    IMapProvider mazeCreator = new MazeFromFile.MazeFromFile(sFilePath);
                    Console.WriteLine("Map Found! ...Building maze...");
                    Map map = new Map(mazeCreator);
                    validPath = true;
                    map.CreateMap();

                    //uses helper method to print out map
                    PrintMap(map);

                }
                catch (Exception e)
                {
                    Console.WriteLine("Something went wrong with the path, make sure it is in the base project directory and written properly");
                    continue;
                }
            }*/
        }


            private static void PrintMap(Map map)
            {
                for (int i = 0; i < map.Height; i++)
                {
                    for (int j = 0; j < map.Width; j++)
                    {

                        //Places Player on Map
                        if (i == map.Player.StartY && j == map.Player.StartX)
                        {
                            Console.Write(" P ");
                            continue;
                        }

                        //Places Goal on Map

                        if (i == map.Goal.Y && j == map.Goal.X)
                        {
                            Console.Write(" G ");
                            continue;
                        }

                        if (map.MapGrid[j, i] == Block.Solid)
                        {
                            Console.Write(" O ");
                        }
                        else
                        {
                            Console.Write(" . ");
                        }
                    }
                    Console.WriteLine("\n");
                }
            }
    }
}