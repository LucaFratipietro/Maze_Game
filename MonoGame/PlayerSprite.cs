using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Maze;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;

namespace MonoGame
{
    public class PlayerSprite : DrawableGameComponent
    {

        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        private Player _player;
        private MapVector _goal;
        private Texture2D _oddish;
        private Texture2D _path;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Game _game;

        private MapVector _previousPosition;
        private InputManager _inputManager;

        public PlayerSprite(Player player, Game game, MapVector goal) : base(game)
        {

            //sets important variables for PlayerSprite function
            this._player = player;
            this._game = game;
            this._goal = goal;
            this._inputManager = InputManager.Instance;

            _logger.Info($"Player Starting Position -- X:{_player.Position.X} Y:{_player.Position.Y}");
            _logger.Info($"Goal Position -- X:{_goal.X} Y:{_goal.Y}");

        }
        
        public override void Initialize()
        {

            //Add actions to Inputmanager for each key
            _inputManager.AddKeyHandler(Keys.Right, () => _player.TurnRight());
            _inputManager.AddKeyHandler(Keys.Left, () => _player.TurnLeft());
            _inputManager.AddKeyHandler(Keys.Up, MoveForward);
            _inputManager.AddKeyHandler(Keys.Down, MoveBackwards);

            base.Initialize();
        }

        protected override void LoadContent()
        {

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _oddish = _game.Content.Load<Texture2D>("oddish");
            _path = _game.Content.Load<Texture2D>("path");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {

            _inputManager.Update();

            //if player moves into the goal, end the game
            if (this._player.Position.Equals(this._goal)) { _logger.Info("Game Exit -- Player reached goal");  _game.Exit(); }

            base.Update(gameTime);

        }
        
        public override void Draw(GameTime gameTime)
        {

            _spriteBatch.Begin();

            if(_previousPosition != null && _previousPosition != _player.Position)
            {
                _spriteBatch.Draw(_path, new Vector2(this._previousPosition.X * 32, this._previousPosition.Y * 32), Color.White);
                _previousPosition = _player.Position; //note, maybe not smart to have playerLogic in draw
                _logger.Debug("Path fixed!");

            }

            //redraw path on where sprite is located and than draw sprite
            _spriteBatch.Draw(_path, new Vector2(this._player.Position.X * 32, this._player.Position.Y * 32), Color.White);
            _spriteBatch.Draw(
                _oddish,
                new Vector2(this._player.Position.X * 32 + 16, this._player.Position.Y * 32 + 16),
                null,
                Color.White,
                this._player.GetRotation(),
                new Vector2(_oddish.Width / 2, _oddish.Height / 2),
                1.0f,
                SpriteEffects.None,
                0);
            _spriteBatch.End();

            _logger.Debug("Player ReDrawn");

            base.Draw(gameTime);
        }
        private void MoveForward()
        {
            try
            {
                //Gets current player position
                MapVector position = _player.Position;

                this._player.MoveForward();
                _logger.Info($"Player Moved forward -- New Position X:{_player.Position.X} Y:{_player.Position.Y}");

                //if player is succesfully moved forward, save previous position 
                this._previousPosition = position;
            }
            catch {
                _logger.Info("Player failed to move forward");
            }
        }

        private void MoveBackwards()
        {
            try
            {
                //Gets current player position
                MapVector position = _player.Position;

                this._player.MoveBackward();
                _logger.Info($"Player Moved backwards -- New Position X:{_player.Position.X} Y:{_player.Position.Y}");

                //if player is succesfully moved backwards, save previous position 
                this._previousPosition = position;
            }
            catch
            {
                _logger.Info("Player failed to move backwards");
            }
        }

    }
}
