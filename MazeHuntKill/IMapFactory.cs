using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Maze;

namespace MazeHuntKillSpace
{
    public class IMapFactory
    {

        //Factory method
        public static IMapProvider MapFactory(int? seed, string type)
        {
            if (seed != null)
            {
                if(type == "Recursion")
                {
                    return new MazeRecursion(seed);
                }
                else if(type == "Hunt")
                {
                    return new MazeHuntKill(seed);
                }
                return null;
            }
            else
            {
                if (type == "Recursion")
                {
                    return new MazeRecursion(null);
                }
                else if (type == "Hunt")
                {
                    return new MazeHuntKill(null);
                }
                return null;
            }
        }

    }
}
