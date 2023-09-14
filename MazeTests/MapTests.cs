using Microsoft.VisualStudio.TestTools.UnitTesting;
using Maze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace Maze.Tests
{
    [TestClass()]
    public class MapTests
    {
        [TestMethod()]
        public void MapConstructorTest()
        {

            //Moq an IMapProvider as it is needed to create a Map object
            var mapProvider = new Mock<IMapProvider>();

            //CreateMap just needs to return the map5x5.txt direction 2d array so we can test our map methods
            Direction[,] directions = new Direction[2, 2];
            directions[0, 0] = Direction.E | Direction.S;
            directions[0, 1] = Direction.S | Direction.W;
            directions[1, 0] = Direction.N;
            directions[1, 1] = Direction.N;

            mapProvider.Setup(m => m.CreateMap()).Returns(directions);
            var map = new Map(mapProvider.Object);

            Assert.IsNotNull(map);

        }

        [TestMethod()]
        public void CreateMap()
        {
            //arrange moq

            //Moq an IMapProvider as it is needed to create a Map object
            var mapProvider = new Mock<IMapProvider>();

            //CreateMap just needs to return the map5x5.txt direction 2d array so we can test our map methods
            Direction[,] directions = new Direction[2, 2];
            directions[0, 0] = Direction.E | Direction.S;
            directions[0, 1] = Direction.S | Direction.W;
            directions[1, 0] = Direction.N;
            directions[1, 1] = Direction.N;

            mapProvider.Setup(m => m.CreateMap()).Returns(directions);
            var testMap = new Map(mapProvider.Object);

            //act

            testMap.CreateMap();

            //assert

            //Check if Width and Height are set in map

            Assert.AreEqual(5, testMap.Width);
            Assert.AreEqual(5, testMap.Height);

            //Check if mapGrid was made empty at certain map positions

            Assert.AreEqual(testMap.MapGrid[1, 1], Block.Empty);
            Assert.AreEqual(testMap.MapGrid[3, 1], Block.Empty);
            Assert.AreEqual(testMap.MapGrid[2, 3], Block.Empty);

            //Check if mapGrid was made empty at certain map positions

            Assert.AreEqual(testMap.MapGrid[2, 2], Block.Solid);
            Assert.AreEqual(testMap.MapGrid[3, 2], Block.Solid);


            //Check if player has been place somewhere

            //Check if goal was place in either of the two deadend positions





        }

        [TestMethod()]
        public void CreateMapTestWithHeightAndWidth()
        {
            //NOT IMPLEMENTED YET
        }

        [TestMethod()]
        public void SaveDirectionMapTest()
        {
            //NOT IMPLEMENTED YET
        }
    }
}