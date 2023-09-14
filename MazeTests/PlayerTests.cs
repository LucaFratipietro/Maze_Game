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
        public void MoveBackwardTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void MoveForwardTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void TurnLeftTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void TurnRightTest()
        {
            Assert.Fail();
        }
    }
}