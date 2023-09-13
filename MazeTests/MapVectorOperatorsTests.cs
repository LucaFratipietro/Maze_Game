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
    public class MapVectorOperatorsTests
    {
        [TestMethod()]
        public void PlusOperatorTest()
        {
            MapVector vectorOne = new MapVector(3,4);
            MapVector vectorTwo = new MapVector(5,7);

            MapVector addedVector = vectorOne + vectorTwo;

            Assert.AreEqual(8, addedVector.X);
            Assert.AreEqual(11, addedVector.Y);
        }

        [TestMethod()]
        public void SubtractOperatorTest()
        {

            MapVector vectorOne = new MapVector(5, 7);
            

        }
    }
}
