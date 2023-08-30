using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze
{
    internal class Player : IPlayer
    {
        public Direction Facing => throw new NotImplementedException();

        public MapVector Position => throw new NotImplementedException();

        public int StartX => throw new NotImplementedException();

        public int StartY => throw new NotImplementedException();

        public float GetRotation()
        {
            throw new NotImplementedException();
        }

        public void MoveBackward()
        {
            throw new NotImplementedException();
        }

        public void MoveForward()
        {
            throw new NotImplementedException();
        }

        public void TurnLeft()
        {
            throw new NotImplementedException();
        }

        public void TurnRight()
        {
            throw new NotImplementedException();
        }
    }
}
