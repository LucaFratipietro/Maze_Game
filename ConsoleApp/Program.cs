using MazeFromFile;
using System.Security.Cryptography.X509Certificates;

namespace Maze
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //find path to file
            string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\..\map9x13.txt");
            string sFilePath = Path.GetFullPath(sFile);

            IMapProvider mazeCreator = new MazeFromFile.MazeFromFile(sFilePath);

            //pass this to maze.cs later

            Map map = new Map(mazeCreator);

            map.CreateMap();

            Console.WriteLine("MAP WIDTH: " + map.Width);
            Console.WriteLine("MAP HEIGHT: " + map.Height);

            for(int i = 0; i < map.Height; i++) {
                for(int j =  0; j < map.Width; j++)
                {
                    if (map.MapGrid[i,j] == Block.Solid)
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

            //testing for InsideBoundry and MapVector stuff

            MapVector V1 = new MapVector(8, 4);
            MapVector V2 = new MapVector(4, 7);
            MapVector V3 = new MapVector(4, 7);

            Console.WriteLine(V1.InsideBoundary(9, 13));
            Console.WriteLine(V1.Magnitude());

            MapVector added = V1 + V2;
            MapVector sub = V1 - V2;

            Console.WriteLine(added);
            Console.WriteLine(sub);

            //scalar 

            Console.WriteLine(V2 * 6);

            //Equals

            Console.WriteLine(V2.Equals(V3));
            Console.WriteLine(V2.Equals(V1));
            Console.WriteLine(V2.Equals(map));


        }
    }
}