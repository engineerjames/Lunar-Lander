namespace LunarLanderGame
{
    using LunarLanderGame.Logging;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using System;

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private TextureManager _textureManager;
        private ILogger _logger;

        private Sprite _lander;

        // Temporary physics until we get a Lander class
        private Vector2 _landerVelocity;
        private Vector2 _landerThrust;
        private Vector2 _landerAcceleration;
        private float _landerMassInKg = 100.0f;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _logger = new ConsoleLogger();

            _textureManager = new TextureManager(Content, _logger);

            this.IsFixedTimeStep = true;
            this.TargetElapsedTime = TimeSpan.FromSeconds(1d / 60d); 
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
            // We only control the _lander here
            ProcessPlayerInput(gameTime);

            ApplyPhysics(gameTime);

            base.Update(gameTime);
        }

        private void ApplyPhysics(GameTime gameTime)
        {
            // m/s^2 - y coordinate faces down
            //
            // From top left corner of screen
            //  ----x+
            // |
            // |
            // |
            // y+

            Vector2 gravity = new Vector2(0, 9.8f);

            Vector2 accelerationFromThrust = _landerThrust / _landerMassInKg;

            if (accelerationFromThrust.Length() < gravity.Length() && _landerThrust.Length() > 0)
            {
                _logger.Log(ILogger.LogLevel.Info, "Can't fight gravity!");
            }

            // Currently, just gravity--but we will need to figure out the thruster
            _landerAcceleration = gravity + accelerationFromThrust;

            // m/s^2 * s == m/s
            _landerVelocity += _landerAcceleration * (float)(gameTime.ElapsedGameTime.TotalSeconds);

            // m/s * s == m
            _lander.SetPosition(_lander.GetPosition() + _landerVelocity * (float)( gameTime.ElapsedGameTime.TotalSeconds ));
        }

        private void ProcessPlayerInput(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                // TODO: Add thrust based on rotation
                _landerThrust = new Vector2(0.0f, -1200.0f);
            }
            else
            {
                 _landerThrust = Vector2.Zero;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                // Rotate CCW                
                float newRotation = (float)( _lander.GetRotation() - 100 * gameTime.ElapsedGameTime.TotalSeconds );
                _lander.SetRotation(newRotation);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                // Rotate CCW                
                float newRotation = (float)( _lander.GetRotation() + 100 * gameTime.ElapsedGameTime.TotalSeconds );
                _lander.SetRotation(newRotation);
            }

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}