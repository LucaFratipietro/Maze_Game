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
    public class MapVectorTests
    {
        [TestMethod()]
        public void MapVectorTest()
        {
            //arrange
            MapVector map = new MapVector(6, 8);
            Assert.IsNotNull(map);
        }

        [TestMethod()]
        public void InsideBoundaryTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void MagnitudeTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EqualsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Assert.Fail();
        }
    }
}