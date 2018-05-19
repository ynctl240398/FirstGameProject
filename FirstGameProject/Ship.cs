using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FirstGameProject
{
    class Ship : DrawableGameComponent
    {
        protected Texture2D texture;
        protected Rectangle spriteRectangle;
        protected Vector2 position;
        protected SpriteBatch sBatch;

        protected const int ShipWidth = 30;
        protected const int ShipHeight = 30;

        protected Rectangle screenBounds;

        public Ship(Game game, ref Texture2D theTexture)
            : base(game)
        {
            texture = theTexture;
            position = new Vector2();

            sBatch = (SpriteBatch)Game.Services.GetService(typeof (SpriteBatch));

            spriteRectangle = new Rectangle(31, 83, ShipWidth, ShipHeight);

            screenBounds = new Rectangle(0, 0, Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height);
        }

        public void PutinStartPosition()
        {
            position.X = screenBounds.Width / 2;
            position.Y = screenBounds.Height - ShipHeight;
        }

        public void ShipMove()
        {
            KeyboardState keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.Up))
            {
                position.Y -= 3;
            }
            if (keyboard.IsKeyDown(Keys.Down))
            {
                position.Y += 3;
            }
            if (keyboard.IsKeyDown(Keys.Left))
            {
                position.X -= 3;
            }
            if (keyboard.IsKeyDown(Keys.Right))
            {
                position.Y += 3;
            }
        }

        public void CheckShip()
        {
            if (position.X < screenBounds.Left)
            {
                position.X = screenBounds.Left;
            }
            if (position.X > screenBounds.Width - ShipWidth)
            {
                position.X = screenBounds.Width - ShipWidth;
            }
            if (position.Y < screenBounds.Top)
            {            
                position.Y = screenBounds.Top;
            }            
            if (position.Y > screenBounds.Height - ShipHeight)
            {            
                position.Y = screenBounds.Height - ShipHeight;
            }
        }

        public Rectangle GetBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, ShipWidth, ShipHeight);
        }

        public override void Draw(GameTime gameTime)
        {
            sBatch.Draw(texture, position, spriteRectangle, Color.White);

            base.Draw(gameTime);
        }
    }
}
