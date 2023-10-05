using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Maze;
using Microsoft.Xna.Framework.Input;
using System.Windows;

namespace MonoGame
{
    public class PlayerSprite : DrawableGameComponent
    {

        private Player _player;
        private Texture2D _oddish;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Game _game;

        private MapVector _currentPosition;
        private InputManager _inputManager;

        public PlayerSprite(Player player, Game game) : base(game)
        {
            this._player = player;
            this._game = game;
            this._inputManager = InputManager.Instance;

            //sets initial position of player to their starting location
            this._currentPosition = new MapVector(_player.StartX, _player.StartY);
        }
        
        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _oddish = _game.Content.Load<Texture2D>("oddish");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                try
                {
                   if(this._player.Facing == Direction.N) { this._player.TurnRight(); }
                   this._player.MoveForward();
                    
                }
                catch
                {
                    //do nothing
                }
                
            }

            _inputManager.Update();

            base.Update(gameTime);
        }
        
        public override void Draw(GameTime gameTime)
        {

            _spriteBatch.Begin();
            _spriteBatch.Draw(_oddish,new Vector2(this._player.Position.X * 32, this._player.Position.Y * 32), Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
