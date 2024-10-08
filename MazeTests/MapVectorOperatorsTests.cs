﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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

            //Subtracting two positive vectors

            MapVector vectorOne = new MapVector(5, 7);
            MapVector vectorTwo = new MapVector(2, 9);

            MapVector subtractedVector = vectorOne - vectorTwo;

            Assert.AreEqual(3, subtractedVector.X);
            Assert.AreEqual(-2, subtractedVector.Y);

            //Subtracting positive vecotr with negative vector

            MapVector vectorNegative = new MapVector(-3, -2);
            
            subtractedVector = vectorOne - vectorNegative;

            Assert.AreEqual(8, subtractedVector.X);
            Assert.AreEqual(9, subtractedVector.Y);
            

        }

        [TestMethod()]
        public void MultiplicationOperatorTest()
        {

            //Multiplying by a positive scalar

            MapVector vectorOne = new MapVector(4, 6);

            MapVector multipliedVecotr = vectorOne * 3;

            Assert.AreEqual(12, multipliedVecotr.X);
            Assert.AreEqual(18, multipliedVecotr.Y);

            //Multiplying by a negative scalar

            MapVector negativeMultiplied = vectorOne * -2;

            Assert.AreEqual(-8, negativeMultiplied.X);
            Assert.AreEqual(-12, negativeMultiplied.Y);

        }

        [TestMethod()]
        public void CastingMapVectorTest()
        {

            //Casting to North pointing vector

            Direction north = Direction.N;
            MapVector northVector = (MapVector)north;

            Assert.AreEqual(0, northVector.X);
            Assert.AreEqual(-1, northVector.Y);

            //Casting to North pointing vector

            Direction south = Direction.S;
            MapVector southVector = (MapVector)south;

            Assert.AreEqual(0, southVector.X);
            Assert.AreEqual(1, southVector.Y);

            //Casting to North pointing vector

            Direction west = Direction.W;
            MapVector westVector = (MapVector)west;

            Assert.AreEqual(-1, westVector.X);
            Assert.AreEqual(0, westVector.Y);

            //Casting to North pointing vector

            Direction east = Direction.E;
            MapVector eastVector = (MapVector)east;

            Assert.AreEqual(1, eastVector.X);
            Assert.AreEqual(0, eastVector.Y);




        }
    }
}
