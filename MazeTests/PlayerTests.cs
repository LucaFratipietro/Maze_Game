using Microsoft.VisualStudio.TestTools.UnitTesting;
using Maze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Tests
{
    [TestClass()]
    public class PlayerTests
    {
        [TestMethod()]
        public void PlayerTest()
        {
            Block[,] mapGrid = new Block[5, 5];

            Player player  = new Player(mapGrid);

            Assert.IsNotNull(player);
            Assert.AreEqual(Direction.N, player.Facing);
        }

        [TestMethod()]
        public void GetRotationTest()
        {
            //if player is currently Facing north
            Block[,] mapGrid = new Block[5, 5];
            Player player = new Player(mapGrid);

            float radian = player.GetRotation();

            Assert.AreEqual(0, radian);

            //if player is facing S

            player.Facing = Direction.S;

            radian  = player.GetRotation();

            Assert.AreEqual(3.14159265, radian, 0.001);

            // if player is facing W

            player.Facing = Direction.W;

            radian = player.GetRotation();

            Assert.AreEqual(4.712388, radian, 0.001);

            // if player is facing E

            player.Facing = Direction.E;

            radian = player.GetRotation();

            Assert.AreEqual(1.57079632, radian, 0.001);

            // if player is not facing a direction, should throw error

            player.Facing = Direction.None;

            try
            {
                player.GetRotation();
                Assert.Fail("Should have thrown an exception");
            }
            catch { };


        }

        [TestMethod()]
        public void MoveBackwardFacingNorthTest()
        {

            //arrange
            //create a 5x5 mapGrid for testing in reverse U-shape
            //done in helper method to avoid repeating in other methods

            Block[,] mapGrid = reverseUMapGrid();

            Player player = new Player(mapGrid);

            //Legal backwards movement from n facing positon

            player.Position = new MapVector(1,1);

            player.MoveBackward();

            Assert.AreEqual(1, player.Position.X);
            Assert.AreEqual(2, player.Position.Y);

            //illegal backwards movment from n facing position

            player.Position = new MapVector(2, 1);

            try
            {
                player.MoveBackward();
                Assert.Fail("Should have thrown illegal move exception");
            }
            catch {
                Assert.AreEqual(2, player.Position.X);
                Assert.AreEqual(1, player.Position.Y);
            }

        }

        [TestMethod()]
        public void MoveBackwardFacingSouthTest()
        {

            Block[,] mapGrid = reverseUMapGrid();

            Player player = new Player(mapGrid);
            player.Facing = Direction.S;

            //Legal backwards movement from s facing positon

            player.Position = new MapVector(1, 2);

            player.MoveBackward();

            Assert.AreEqual(1, player.Position.X);
            Assert.AreEqual(1, player.Position.Y);

            //illegal backwards movment from s facing position

            player.Position = new MapVector(1, 1);

            try
            {
                player.MoveBackward();
                Assert.Fail("Should have thrown illegal move exception");
            }
            catch
            {
                Assert.AreEqual(1, player.Position.X);
                Assert.AreEqual(1, player.Position.Y);
            }

        }

        [TestMethod()]
        public void MoveBackwardFacingWestTest()
        {

            Block[,] mapGrid = reverseUMapGrid();

            Player player = new Player(mapGrid);
            player.Facing = Direction.W;

            //Legal backwards movement from west facing positon

            player.Position = new MapVector(1, 1);

            player.MoveBackward();

            Assert.AreEqual(2, player.Position.X);
            Assert.AreEqual(1, player.Position.Y);

            //illegal backwards movment from west facing position

            player.Position = new MapVector(1, 2);

            try
            {
                player.MoveBackward();
                Assert.Fail("Should have thrown illegal move exception");
            }
            catch
            {
                Assert.AreEqual(1, player.Position.X);
                Assert.AreEqual(2, player.Position.Y);
            }

        }

        [TestMethod()]
        public void MoveBackwardFacingEastTest()
        {

            Block[,] mapGrid = reverseUMapGrid();

            Player player = new Player(mapGrid);
            player.Facing = Direction.E;

            //Legal backwards movement from east facing positon

            player.Position = new MapVector(3, 1);

            player.MoveBackward();

            Assert.AreEqual(2, player.Position.X);
            Assert.AreEqual(1, player.Position.Y);

            //illegal backwards movment from east facing position

            player.Position = new MapVector(3, 2);

            try
            {
                player.MoveBackward();
                Assert.Fail("Should have thrown illegal move exception");
            }
            catch
            {
                Assert.AreEqual(3, player.Position.X);
                Assert.AreEqual(2, player.Position.Y);
            }

        }

        [TestMethod()]
        public void MoveForwardFacingNorthTest()
        {

            //arrange
            //create a 5x5 mapGrid for testing in reverse U-shape
            //done in helper method to avoid repeating in other methods

            Block[,] mapGrid = reverseUMapGrid();

            Player player = new Player(mapGrid);

            //Legal forwards movement from n facing positon

            player.Position = new MapVector(1, 2);

            player.MoveForward();

            Assert.AreEqual(1, player.Position.X);
            Assert.AreEqual(1, player.Position.Y);

            //illegal forwards movment from n facing position

            player.Position = new MapVector(1, 1);

            try
            {
                player.MoveForward();
                Assert.Fail("Should have thrown illegal move exception");
            }
            catch
            {
                Assert.AreEqual(1, player.Position.X);
                Assert.AreEqual(1, player.Position.Y);
            }

        }

        [TestMethod()]
        public void MoveForwardFacingSouthTest()
        {

            Block[,] mapGrid = reverseUMapGrid();

            Player player = new Player(mapGrid);
            player.Facing = Direction.S;

            //Legal forwards movement from S facing positon

            player.Position = new MapVector(1, 1);

            player.MoveForward();

            Assert.AreEqual(1, player.Position.X);
            Assert.AreEqual(2, player.Position.Y);

            //illegal forwards movment from S facing position

            player.Position = new MapVector(1, 3);

            try
            {
                player.MoveForward();
                Assert.Fail("Should have thrown illegal move exception");
            }
            catch
            {
                Assert.AreEqual(1, player.Position.X);
                Assert.AreEqual(3, player.Position.Y);
            }

        }

        [TestMethod()]
        public void MoveForwardFacingWestTest()
        {

            Block[,] mapGrid = reverseUMapGrid();

            Player player = new Player(mapGrid);
            player.Facing = Direction.W;

            //Legal forwards movement from W facing positon

            player.Position = new MapVector(3, 1);

            player.MoveForward();

            Assert.AreEqual(2, player.Position.X);
            Assert.AreEqual(1, player.Position.Y);

            //illegal forwards movment from W facing position

            player.Position = new MapVector(1, 1);

            try
            {
                player.MoveForward();
                Assert.Fail("Should have thrown illegal move exception");
            }
            catch
            {
                Assert.AreEqual(1, player.Position.X);
                Assert.AreEqual(1, player.Position.Y);
            }

        }

        [TestMethod()]
        public void MoveForwardFacingEastTest()
        {

            Block[,] mapGrid = reverseUMapGrid();

            Player player = new Player(mapGrid);
            player.Facing = Direction.E;

            //Legal forwards movement from E facing positon

            player.Position = new MapVector(1, 1);

            player.MoveForward();

            Assert.AreEqual(2, player.Position.X);
            Assert.AreEqual(1, player.Position.Y);

            //illegal forwards movment from E facing position

            player.Position = new MapVector(3, 2);

            try
            {
                player.MoveForward();
                Assert.Fail("Should have thrown illegal move exception");
            }
            catch
            {
                Assert.AreEqual(3, player.Position.X);
                Assert.AreEqual(2, player.Position.Y);
            }

        }

        [TestMethod()]
        public void TurnLeftTest()
        {

            //arrange
            Block[,] mapGrid = new Block[5, 5];
            Player player = new Player(mapGrid);

            //if player is facing North

            player.TurnLeft();

            Assert.AreEqual(Direction.W, player.Facing);

            //if player is facing West

            player.Facing = Direction.W;
            player.TurnLeft();

            Assert.AreEqual(Direction.S, player.Facing);

            //if player is facing South

            player.Facing = Direction.S;
            player.TurnLeft();

            Assert.AreEqual(Direction.E, player.Facing);

            //if player is facing East

            player.Facing = Direction.E;
            player.TurnLeft();

            Assert.AreEqual(Direction.N, player.Facing);

            //if player is not facing a direction, should throw excpetion

            try
            {
                player.TurnLeft();
                Assert.Fail("Should have thrown an exception");
            }
            catch { };

        }

        [TestMethod()]
        public void TurnRightTest()
        {

            //arrange
            Block[,] mapGrid = new Block[5, 5];
            Player player = new Player(mapGrid);

            //if player is facing North

            player.TurnRight();

            Assert.AreEqual(Direction.E, player.Facing);

            //if player is facing West

            player.Facing = Direction.W;
            player.TurnRight();

            Assert.AreEqual(Direction.N, player.Facing);

            //if player is facing South

            player.Facing = Direction.S;
            player.TurnRight();

            Assert.AreEqual(Direction.W, player.Facing);

            //if player is facing East

            player.Facing = Direction.E;
            player.TurnRight();

            Assert.AreEqual(Direction.S, player.Facing);

            //if player is not facing a direction, should throw excpetion

            try
            {
                player.TurnRight();
                Assert.Fail("Should have thrown an exception");
            }
            catch { };

        }

        private Block[,] reverseUMapGrid()
        {

            Block[,] mapGrid = new Block[5, 5];

            //dear god there must be a better way to do this

            mapGrid[1, 1] = Block.Empty;
            mapGrid[1, 2] = Block.Empty;
            mapGrid[1, 3] = Block.Empty;
            mapGrid[2, 1] = Block.Empty;
            mapGrid[2, 3] = Block.Empty;
            mapGrid[3, 1] = Block.Empty;
            mapGrid[3, 3] = Block.Empty;

            return mapGrid;

        }
    }
}