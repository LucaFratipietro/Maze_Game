using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze
{
    internal class Player : IPlayer
    {

        public Player() {
            this.Facing = Direction.N;
        }
        public Direction Facing { get; set; }

        public MapVector Position {  get; set; }

        public int StartX {  get; set; }

        public int StartY { get; set; }

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
