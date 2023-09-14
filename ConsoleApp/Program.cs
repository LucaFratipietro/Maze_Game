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
            string sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\..\..\map9x7.txt");
            string sFilePath = Path.GetFullPath(sFile);

            IMapProvider mazeCreator = new MazeFromFile.MazeFromFile(sFilePath);

            //pass this to maze.cs later

            Map map = new Map(mazeCreator);

            map.CreateMap();

            Console.WriteLine("MAP WIDTH: " + map.Width);
            Console.WriteLine("MAP HEIGHT: " + map.Height);

            for (int i = 0; i < map.Height; i++)
            {
                for (int j = 0; j < map.Width; j++)
                {

                    //Places Player on Map
                    if (i == map.Player.StartX && j == map.Player.StartY)
                    {
                        Console.Write(" P ");
                        continue;
                    }

                    //Places Goal on Map

                    if(i == map.Goal.X && j == map.Goal.Y)
                    {
                        Console.Write(" G ");
                        continue;
                    }

                    if (map.MapGrid[i, j] == Block.Solid)
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