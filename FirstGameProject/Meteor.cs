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
    public class Meteor : DrawableGameComponent
    {
        protected Texture2D texture;
        protected Rectangle spriteRectangle;
        protected Vector2 position;
        protected int ySpeed;
        protected int xSpeed;
        protected Random random;
        protected SpriteBatch sBatch;

        protected const int MeteorWidth = 45;
        protected const int MeteorHeight = 45;
        
        protected void PutinStartPosition()
        {
            position.X = random.Next(Game.Window.ClientBounds.Width - MeteorWidth);
            position.Y = 0;
            ySpeed = 1 + random.Next(9);
            xSpeed = random.Next(3) - 1;
        }

        public Meteor(Game game, ref Texture2D theTexture)
            :base(game)
        {
            texture = theTexture;
            position = new Vector2();

            sBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            spriteRectangle = new Rectangle(20, 16, MeteorWidth, MeteorHeight);

            random = new Random(this.GetHashCode());
            PutinStartPosition();
        }

        public override void Update(GameTime gameTime)
        {
            if ((position.Y >= Game.Window.ClientBounds.Height) || (position.X >= Game.Window.ClientBounds.Width) || (position.X <= 0))
                PutinStartPosition();

            position.X += xSpeed;
            position.Y += ySpeed;

            base.Update(gameTime);
        }

        public bool CheckCollision(Rectangle rect)
        {
            Rectangle spriterect = new Rectangle((int)position.X, (int)position.Y, MeteorWidth, MeteorHeight);

            return spriterect.Intersects(rect);
        }

        public override void Draw(GameTime gameTime)
        {
            sBatch.Draw(texture, position, spriteRectangle, Color.White);

            base.Draw(gameTime);
        }
    }
}
