using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Maze;

namespace MonoGame
{
    public class PlayerSprite : DrawableGameComponent
    {

        private Player _player;
        private Texture2D _oddish;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Game _game;

        public PlayerSprite(Player player, Game game) : base(game)
        {
            this._player = player;
            this._game = game;
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
        
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

    }
}
