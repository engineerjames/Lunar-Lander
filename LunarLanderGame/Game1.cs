namespace LunarLanderGame
{
    using LunarLanderGame.Components;
    using LunarLanderGame.Logging;
    using LunarLanderGame.Planets;
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

        private Lander _lander;

        // Planet
        private PlanetGenerator _planetGenerator;
        private Planet _planet;

        public Game1( )
        {
            _graphics = new GraphicsDeviceManager( this );
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _logger = new ConsoleLogger();

            _textureManager = new TextureManager( this, Content, _logger );

            this.IsFixedTimeStep = true;
            this.TargetElapsedTime = TimeSpan.FromSeconds( 1d / 60d );

            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;

            _planetGenerator = new PlanetGenerator();
            _planet = _planetGenerator.GetDefaultPlanet( this, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight );

            _lander = new Lander( this, _textureManager, new Vector2( _graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2 ), _logger );
        }

        protected override void LoadContent( )
        {
        }

        protected override void Initialize( )
        {
            _logger.Log( ILogger.LogLevel.Info, "Game initializing." );

            // Add all game components
            Components.Add( _textureManager );
            Components.Add( _planet );
            Components.Add( _lander );

            _spriteBatch = new SpriteBatch( GraphicsDevice );
            Services.AddService( _spriteBatch );
            Services.AddService( _textureManager );

            base.Initialize();
        }        

        protected override void Update( GameTime gameTime )
        {
            // We only control the _lander here
            ProcessPlayerInput( gameTime );

            base.Update( gameTime );
        }

        private void ProcessPlayerInput( GameTime gameTime )
        {
            if ( GamePad.GetState( PlayerIndex.One ).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown( Keys.Escape ) )
                Exit();
        }

        protected override void Draw( GameTime gameTime )
        {
            GraphicsDevice.Clear( Color.Black );

            base.Draw( gameTime );
        }
    }
}