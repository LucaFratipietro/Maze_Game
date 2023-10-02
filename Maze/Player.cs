using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze
{
    public class Player : IPlayer
    {

        public Player(Block[,] mapGrid) {
            this.Facing = Direction.N;
            this._mapGrid = mapGrid;
        }
        public Direction Facing { get; private set; }

        public MapVector Position {  get; private set; }

        public int StartX {  get; private set; }

        public int StartY { get; private set; }

        readonly private Block[,] mapGrid;


        //returns the radian of the direction the player object is facing
        public float GetRotation()
        {
            //Assuming that direction N is 0 and turning clockwise

            if (this.Facing == Direction.None) { throw new Exception("Player not currently facing a direction"); }

            switch(Facing)
            {
                case Direction.E:
                    return (float)Math.PI / 2f;
                case Direction.S:
                    return (float)Math.PI;
                case Direction.W:
                    return 3f * (float)Math.PI / 2f;
                default:
                    return 0f;
            }

        }

        public void MoveBackward()
        {

            //Backwards is determined by the player current direction facing

            int playerX = Position.X;
            int playerY = Position.Y;

            //if facing North by example, to move backwards
            //new position would be Position.Y + 1, as you only move in the maze in
            //incremenets of 1 and to move south you add 1 to the vector

            switch(Facing)
            {
                case Direction.N:
                    playerY++;
                    break;
                case Direction.S:
                    playerY--;
                    break;
                case Direction.W:
                    playerX++;
                    break;
                case Direction.E:
                    playerX--;
                    break;
            }

            //determine if these new positions are legal

            if (this.mapGrid[playerX, playerY] == Block.Solid) {
                throw new Exception("Illegal Position, current porposed position is Solid");
            }
            if(!new MapVector(playerX, playerY).InsideBoundary(this.mapGrid.GetLength(0), this.mapGrid.GetLength(1))){
                throw new Exception("Illegal Postion, outside the mapGrid boundary");
            }
            
            //if it passes these checks, move is legal and player position is modified
            this.Position = new MapVector(playerX, playerY);

        }

        public void MoveForward()
        {
            //Forwards is determined by the player current direction facing

            int playerX = Position.X;
            int playerY = Position.Y;

            
            switch (Facing)
            {
                case Direction.N:
                    playerY--;
                    break;
                case Direction.S:
                    playerY++;
                    break;
                case Direction.W:
                    playerX--;
                    break;
                case Direction.E:
                    playerX++;
                    break;
            }

            //determine if these new positions are legal

            if (this.mapGrid[playerX, playerY] == Block.Solid)
            {
                throw new Exception("Illegal Position, current porposed position is Solid");
            }
            if (!new MapVector(playerX, playerY).InsideBoundary(this.mapGrid.GetLength(0), this.mapGrid.GetLength(1)))
            {
                throw new Exception("Illegal Postion, outside the mapGrid boundary");
            }

            //if it passes these checks, move is legal and player position is modified
            this.Position = new MapVector(playerX, playerY);
        }


        //depending on player current facing direction, moves it to the postion on the left
        public void TurnLeft()
        {

            if (this.Facing == Direction.None) { throw new Exception("Player not currently facing a direction"); }

            switch (Facing)
            {
                case Direction.N:
                    this.Facing = Direction.W;
                    break;
                case Direction.W:
                    this.Facing = Direction.S;
                    break;
                case Direction.S:
                    this.Facing = Direction.E;
                    break;
                case Direction.E:
                    this.Facing = Direction.N;
                    break;
            }

        }


        //depending on player current facing direction, moves it to the postion on the right
        public void TurnRight()
        {
            switch (Facing)
            {
                case Direction.N:
                    this.Facing = Direction.E;
                    break;
                case Direction.W:
                    this.Facing = Direction.N;
                    break;
                case Direction.S:
                    this.Facing = Direction.W;
                    break;
                case Direction.E:
                    this.Facing = Direction.S;
                    break;
            }
        }
    }
}
