﻿using MazeFromFile;
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
                        Console.Write(" S ");
                    }
                    else
                    {
                        Console.Write(" E ");
                    }
                }
                Console.WriteLine("\n\n");
            }
            
            
            
            
            
            /*Direction[,] directions = mazeCreator.CreateMap();
            for(int i = 0; i < directions.GetLength(0); i++)
            {
                for(int j = 0; j < directions.GetLength(1); j++)
                {
                    Console.Write(directions[i, j].ToString() + " ");
                }
                Console.WriteLine(" ");
            }*/
        }
    }
}