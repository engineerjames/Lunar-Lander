namespace LunarLanderGame
{
    using LunarLanderGame.Logging;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private TextureManager _textureManager;
        private ILogger _logger;

        private Sprite _lander;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _logger = new ConsoleLogger();

            _textureManager = new TextureManager(Content, _logger);

        }

        protected override void Initialize()
        {
            base.Initialize();

            _logger.Log(ILogger.LogLevel.Info, "Game initialized.");
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService(_spriteBatch);
        }

        protected override void LoadContent()
        {  
            _textureManager.LoadAllTextures();

            _lander = new Sprite(this,
                     _textureManager.GetTexture("lander"),
                     new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2));
            _lander.SetScale(new Vector2(0.1f, 0.1f));

            Components.Add(_lander);

            _logger.Log(ILogger.LogLevel.Info, "Content loaded.");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // We only control the _lander here


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}