using Maze;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Windows.Forms;
using MFile = MazeFromFile.MazeFromFile;

namespace MonoGame;

public class MazeGame : Game
{
    Texture2D oddish;

    private Map _map;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public MazeGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        //player must pick a valid map .txt from their file directory to load

        /*var filePath = "";
        using(OpenFileDialog openFileDialog = new OpenFileDialog())
        {
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files(*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;
            
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //gets the path for the map you want to load
                filePath = openFileDialog.FileName;
            }
        }*/

        string sCurrentDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
        string sFile = System.IO.Path.Combine(sCurrentDirectory, $@"..\..\..\..\map9x7.txt");
        string sFilePath = Path.GetFullPath(sFile);

        //Once map is chosen, create map object for the game
        IMapProvider mapPro = new MazeFromFile.MazeFromFile(sFilePath);
        _map = new Map(mapPro);
        _map.CreateMap();
        
        //when Map is set, start initializing game
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here

        oddish = Content.Load<Texture2D>("oddish");

    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == Microsoft.Xna.Framework.Input.ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        _spriteBatch.Draw(oddish, new Vector2(0, 0), Color.CornflowerBlue);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
