using Maze;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;
using forms = System.Windows.Forms;
using MFile = MazeFromFile.MazeFromFile;

namespace MonoGame;

public class MazeGame : Game
{
    private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

    //loading textures
    private Texture2D _wall;
    private Texture2D _path;
    private Texture2D _goal;

    //objects needed for game to run
    private bool _initalMapLoad = false;
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

        var filePath = "";
        using(forms.OpenFileDialog openFileDialog = new forms.OpenFileDialog())
        {
            openFileDialog.InitialDirectory = ".";
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files(*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;
            
            if(openFileDialog.ShowDialog() == forms.DialogResult.OK)
            {
                //gets the path for the map you want to load
                filePath = openFileDialog.FileName;
            }
        }

        //Once map is chosen, create map object for the game
        IMapProvider mapPro = new MazeFromFile.MazeFromFile(filePath);
        _map = new Map(mapPro);
        _map.CreateMap();
        _logger.Info($"Map Loaded: {_map.Width} x {_map.Height} map loaded");

        //sets window resolution to match loded map
        _graphics.PreferredBackBufferWidth = _map.Width * 32;
        _graphics.PreferredBackBufferHeight =  _map.Height * 32;
        _graphics.ApplyChanges();

        //Pass player object to PlayerSprite
        PlayerSprite playerS = new PlayerSprite((Player)_map.Player, this, _map.Goal);

        //add player sprite as component to mono game

        Components.Add(playerS);

        //when Map is set, start initializing game
        base.Initialize();

    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // Load wall, path and goal sprites/content here

        _wall = Content.Load<Texture2D>("wall");
        _path = Content.Load<Texture2D>("path");
        _goal = Content.Load<Texture2D>("goal");

        base.LoadContent();

    }

    protected override void Update(GameTime gameTime)
    {

        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) { _logger.Info("Game Exit -- Manuel Exit using ESC");  Exit(); }
            

        base.Update(gameTime);


    }

    protected override void Draw(GameTime gameTime)
    {
        //draw map on inital run of Draw, but don't redraw it again afterwords, all redrawing of player and paths done in player sprites
        if (!_initalMapLoad)
        {
            GraphicsDevice.Clear(Color.DarkSlateBlue);
            _initalMapLoad = true;
            drawMap(gameTime);
        }

        base.Draw(gameTime);
    }

    private void drawMap(GameTime gameTime)
    {

        //draws the intial map state
        _spriteBatch.Begin();

        //loop through given MapGrid in map to determine where to draw each sprite

        for (int i = 0; i < _map.Height; i++)
        {
            for (int j = 0; j < _map.Width; j++)
            {

                

                if (_map.MapGrid[j, i] == Block.Solid)
                {
                    _spriteBatch.Draw(_path, new Vector2(j * 32, i * 32), Color.White);
                    _spriteBatch.Draw(_wall, new Vector2(j * 32, i * 32), Color.White);
                }
                else
                {
                    _spriteBatch.Draw(_path, new Vector2(j * 32, i * 32), Color.White);
                }

                //Places Goal on Map

                if (i == _map.Goal.Y && j == _map.Goal.X)
                {
                    _spriteBatch.Draw(_goal, new Vector2(j * 32, i * 32), Color.White);
                }

            }
        }

        _logger.Debug("Map Drawn");

        _spriteBatch.End();

    }
}
