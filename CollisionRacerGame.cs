using CollisionRacer.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CollisionRacer
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class CollisionRacerGame : GameHost
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;

        private Texture2D _playerTexture;
        private Texture2D _trackTexture;

        private CarPlayer player;

        private Color _colorMagicRed = new Color(255,0,0);

        public MonoLog Log;
        private SpriteFont _spriteFont;

        private Color[,] _TrackColors;
        public CollisionRacerGame()
        {
            _graphics = new GraphicsDeviceManager(this);
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
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _trackTexture = Content.Load<Texture2D>("PixelPerfectCollision");
            _playerTexture = Content.Load<Texture2D>("car");
            _spriteFont = Content.Load<SpriteFont>("MonoLog");
            SetGame();
        }

        private void SetGame()
        {
            // read colors from level texture
            _TrackColors = TextureTo2DArray(_trackTexture);

            player = new CarPlayer(this, new Vector2(160,400), _playerTexture);
            GameObjects.Add(player);
            
            Log = new MonoLog(this, _spriteFont, Color.Black);
            GameObjects.Add(Log);
            
            

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            ColorCheck();
            UpdateAll(gameTime);
            base.Update(gameTime);
        }

        private void ColorCheck()
        {
            Color color = _TrackColors[(int)player.PositionX, (int)player.PositionY];
            if (color.Equals(_colorMagicRed))
            {
                Log.Write("new color(" + color.R + "," + color.G + "," +color.B +")");
            }
        }

        private Color[,] TextureTo2DArray(Texture2D texture)
        {
            Color[] colors1D = new Color[texture.Width * texture.Height];
            texture.GetData(colors1D);

            Color[,] colors2D = new Color[texture.Width, texture.Height];
            for (int x = 0; x < texture.Width; x++)
                for (int y = 0; y < texture.Height; y++)
                    colors2D[x, y] = colors1D[x + y * texture.Width];

            return colors2D;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _spriteBatch.Draw(_trackTexture, new Vector2(0, 0), Color.White);
            foreach (SpriteObject spriteObject in GameObjects)
            {
                spriteObject.Draw(gameTime, _spriteBatch);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        
    }
}
