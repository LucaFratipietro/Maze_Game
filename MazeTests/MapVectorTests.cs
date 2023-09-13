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

            //assert
            Assert.IsNotNull(map);
        }

        [TestMethod()]
        [DataRow(3,2,4,5)] //Vector inside boundary
        [DataRow(0,-2,4,5)] //Vector Y < 0 
        [DataRow (-1,3,4,5)] //Vector X < 0
        [DataRow(3,10,4,5)] //Vector larger than width
        public void InsideBoundaryTest(int vX, int vY, int width, int height)
        {
            //arrange
            MapVector mapV = new MapVector(vX,vY);

            //act 
            bool insideValid = mapV.InsideBoundary(width,height);

            //assert
            if(vX < 0 || vY < 0 || vX >= width || vY >= height)
            {
                Assert.IsFalse(insideValid);
            }
            else
            {
                Assert.IsTrue(insideValid);
            }
            
        }

        [TestMethod()]
        public void MagnitudeTest()
        {
            //Vector of positive X and Y
            MapVector mapVector = new MapVector(5, 7);

            Double magnitude = mapVector.Magnitude();

            Assert.AreEqual(8.60232, magnitude, 0.01);

            //Vector of positive X and negative Y
            MapVector mapVector2 = new MapVector(4, -6);

            Double magnitude2 = mapVector2.Magnitude();

            Assert.AreEqual(7.2111025, magnitude2, 0.01);

        }

        [TestMethod()]
        public void EqualsTest()
        {
            
            //Assert Equals a different object
            Object obj = new Object();
            MapVector mapVector = new MapVector(3, 4);

            Assert.IsFalse(mapVector.Equals(obj));

            //Assert Equals on a vector of different X/Y
            MapVector mapVectorDifferentX = new MapVector(5, 4);

            Assert.IsFalse(mapVector.Equals(mapVectorDifferentX));
            
            //Assert Equals on vector of same X/Y
            MapVector mapVectorSameXY = new MapVector(3, 4);

            Assert.IsTrue(mapVector.Equals(mapVectorSameXY));

        }

    }
}