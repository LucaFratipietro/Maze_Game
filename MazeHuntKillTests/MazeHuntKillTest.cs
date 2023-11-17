namespace MazeHuntKillTests;

using Maze;
using MazeHuntKillSpace;

[TestClass]
public class MazeHuntKillTest
{

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateMapInvalidWidth()
    {
        //arrange
        MazeHuntKill MR = new MazeHuntKill(7);
        
        //Act
        MR.CreateMap(2, 5);

    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateMapInvalidHeight()
    {
        //arrange
        MazeHuntKill MR = new MazeHuntKill(7);

        //Act
        MR.CreateMap(5, -3);

    }

    //testing if seeded map creates proper direction grid described below
    [TestMethod]
    public void CreateMapSeeded()
    {
        //arrange
        MazeHuntKill MR = new MazeHuntKill(7);
        Direction[,] compareGrid = SeededDirectionGrid();

        //act
        Direction[,] testGrid = MR.CreateMap(7, 7);

        //assert
        //see if direction grid match
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Assert.AreEqual(testGrid[i, j], compareGrid[i, j]);
            }
        }
    }

    [TestMethod]
    [ExpectedException(typeof(NotImplementedException))]
    public void CreateMapNoWidthOrHeight()
    {
        //arrange
        MazeRecursion MR = new MazeRecursion(5);
        Direction[,] compareGrid = SeededDirectionGrid();

        //act
        MR.CreateMap();
    }

    //7x7 7 seed directionGrid
    private Direction[,] SeededDirectionGrid()
    {
        Direction[,] directions = new Direction[3, 3];
        directions[0, 0] = Direction.S | Direction.E;
        directions[0, 1] = Direction.E | Direction.S | Direction.W;
        directions[0, 2] = Direction.W;
        directions[1, 0] = Direction.N | Direction.S;
        directions[1, 1] = Direction.N | Direction.E;
        directions[1, 2] = Direction.W | Direction.S;
        directions[2, 0] = Direction.N;
        directions[2, 1] = Direction.E;
        directions[2, 2] = Direction.N | Direction.W;
        return directions;
    }

}