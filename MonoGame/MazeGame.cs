using Maze;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using forms = System.Windows.Forms;
using MFile = MazeFromFile.MazeFromFile;

namespace MonoGame;

public class MazeGame : Game
{
    private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

    //enum of menuActions
    private enum MenuActions { File = 1, Recursion = 2, Exit = 3}

    //loading textures
    private Texture2D _wall;
    private Texture2D _path;
    private Texture2D _goal;

    //loading fonts
    private SpriteFont _font;

    //needed for menu to function
    private List<MenuActions> _menuActions = new List<MenuActions>() { MenuActions.File, MenuActions.Recursion, MenuActions.Exit};
    private int _menuIndex = 0;
    private int _chosenWidth;
    private int _chosenHeight;

    //objects needed for game to run
    private bool _initalMapLoad = false;
    private bool _inMenu = true;
    private Map _map;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private InputManager _inputManager;



    public MazeGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        this._inputManager = InputManager.Instance;
    }

    protected override void Initialize()
    {

        //set the arrow keys to handle menu interaction
        //key arrow up goes up in menu, if already at the top, loop back to the bottom, Down is the reverse
        _inputManager.AddKeyHandler(Keys.Up, () =>
        {
            if (_menuIndex == 0) { _menuIndex = _menuActions.Count - 1; return; }
            _menuIndex--;
        });

        _inputManager.AddKeyHandler(Keys.Down, () =>
        {
            if (_menuIndex == _menuActions.Count - 1) { _menuIndex = 0; return; }
            _menuIndex++;
        });

        base.Initialize();

    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // Load wall, path and goal sprites/content here

        _wall = Content.Load<Texture2D>("wall");
        _path = Content.Load<Texture2D>("path");
        _goal = Content.Load<Texture2D>("goal");

        // load spritefont

        _font = Content.Load<SpriteFont>("File");

        base.LoadContent();

    }

    protected override void Update(GameTime gameTime)
    {

        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) { _logger.Info("Game Exit -- Manuel Exit using ESC");  Exit(); }

        base.Update(gameTime);
        


    }

    protected override void Draw(GameTime gameTime)
    {
        _inputManager.Update();

        //when in inMenu boolean is true, draw the menu
        if (_inMenu)
        {
            drawMenu(gameTime);
            base.Draw(gameTime);
            return;
        }

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

    private void drawMenu(GameTime gameTme)
    {
        GraphicsDevice.Clear(Color.DarkSlateBlue);
        _spriteBatch.Begin();

        _spriteBatch.DrawString(_font, "Menu: Choose how to load the maze", new Vector2(0, 0), Color.Black);

        //loop through list of possible MapActions
        for (int i = 0; i < _menuActions.Count; i++)
        {
            if(i == _menuIndex)
            {
                _spriteBatch.DrawString(_font, _menuActions[i].ToString(), new Vector2(0, (i + 1) * 15), Color.BurlyWood);
            }
            else { _spriteBatch.DrawString(_font, _menuActions[i].ToString(), new Vector2(0, (i + 1) * 15), Color.Black); }
        }
        _spriteBatch.End();
    }

    private void initializeFileMap()
    {
        //player must pick a valid map .txt from their file directory to load

        var filePath = "";
        using (forms.OpenFileDialog openFileDialog = new forms.OpenFileDialog())
        {
            openFileDialog.InitialDirectory = ".";
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files(*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == forms.DialogResult.OK)
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
        _graphics.PreferredBackBufferHeight = _map.Height * 32;
        _graphics.ApplyChanges();

        //Pass player object to PlayerSprite
        PlayerSprite playerS = new PlayerSprite((Player)_map.Player, this, _map.Goal);

        //add player sprite as component to mono game

        Components.Add(playerS);
    }
}
