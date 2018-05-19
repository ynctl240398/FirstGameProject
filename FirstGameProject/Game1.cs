using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace FirstGameProject
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Texture2D backgroundTexture, meteorTexture;

        //sound
        private SoundEffect explosion;
        private SoundEffect newMeteor;
        private Song backMusic;

        //ship
        private Ship player;
        private int lastTickCount;
        private KeyboardState keyboard;
        private SpriteFont gameFont;
        private int rockCount;
        private const int STARTMETEORCOUNT = 10;
        private const int ADDMETEORTIME = 5000;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //thêm vào dịch vụ spritebatch service 
            Services.AddService(typeof(SpriteBatch), spriteBatch);

            // Load tất cả textures gồm ảnh nền và objects
            meteorTexture = Content.Load<Texture2D>("RockRain");
            backgroundTexture = Content.Load<Texture2D>("SpaceBackground");         

            // Load game font
            gameFont = Content.Load<SpriteFont>("font");

            // Load các sound trong game
            explosion = Content.Load<SoundEffect>("explosion");
            newMeteor = Content.Load<SoundEffect>("newmeteor");
            backMusic = Content.Load<Song>("backMusic");

            // Chơi nhạc nền
            MediaPlayer.Play(backMusic);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here

            keyboard = Keyboard.GetState();

            if (player == null)
            {
                Start();
            }

            DoGameLogic();

            base.Update(gameTime);
        }

        private void Start()
        {
            for (int i = 0; i < STARTMETEORCOUNT; i++)
            {
                Components.Add(new Meteor(this, ref meteorTexture));
            }
            player = new Ship(this, ref meteorTexture);
                Components.Add(player);
            lastTickCount = System.Environment.TickCount;
            rockCount = STARTMETEORCOUNT;
        }

        private void CheckfornewMeteor()
        {
            if (System.Environment.TickCount - lastTickCount > ADDMETEORTIME)
            {
                lastTickCount = System.Environment.TickCount;
                Components.Add(new Meteor(this, ref meteorTexture));
                newMeteor.Play();
                rockCount++;
            }
        }

        private void RemoveallMeteor()
        {
            for (int i = 0; i < Components.Count; i++)
            {
                if (Components[i] is Meteor)
                {
                    Components.RemoveAt(i);
                    i--;
                }
            }
        }

        private void DoGameLogic()
        {
            bool hasCollision = false;
            Rectangle shipRectengle = player.GetBounds();
            foreach (GameComponent gc in Components)
            {
                if (gc is Meteor)
                {
                    hasCollision = ((Meteor)gc).CheckCollision(shipRectengle);
                    if (hasCollision)
                    {
                        explosion.Play();
                        RemoveallMeteor();
                        Start();
                        break;
                    }
                }
            }
            CheckfornewMeteor();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.Deferred,BlendState.AlphaBlend);
            spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, graphics.GraphicsDevice.DisplayMode.Width, graphics.GraphicsDevice.DisplayMode.Height), Color.LightGray);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            base.Draw(gameTime);
            spriteBatch.End();

            spriteBatch.Begin();
            spriteBatch.DrawString(gameFont, "Rock: " + rockCount.ToString(), new Vector2(15, 15), Color.YellowGreen);
            spriteBatch.End();
        }
    }
}
