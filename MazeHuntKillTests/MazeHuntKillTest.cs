namespace MazeHuntKillTests;

using Maze;

[TestClass]
public class MazeHuntKillTest
{

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateMapInvalidWidth()
    {
        //arrange
        MazeHuntKill MR = new MazeHuntKill();
        
        //Act
        MR.CreateMap(2, 5);

    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateMapInvalidHeight()
    {
        //arrange
        MazeHuntKill MR = new MazeHuntKill();

        //Act
        MR.CreateMap(5, -3);

    }

}