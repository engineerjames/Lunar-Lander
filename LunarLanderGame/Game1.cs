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

        private Sprite _lander;

        // Temporary physics until we get a Lander class
        private Vector2 _landerVelocity;
        private Vector2 _landerThrust;
        private Vector2 _landerAcceleration;
        private float _landerMassInKg;

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

            _landerVelocity = new Vector2( 0.0f, 15.0f );
            _landerThrust = Vector2.Zero;
            _landerAcceleration = Vector2.Zero;
            _landerMassInKg = 100.0f;

            _planetGenerator = new PlanetGenerator();
            _planet = _planetGenerator.GetDefaultPlanet( this, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight );

            _lander = new Sprite( this, "lander", 
                                  _textureManager, new Vector2( _graphics.PreferredBackBufferWidth / 2,
                                                                _graphics.PreferredBackBufferHeight / 2 ) );
            _lander.SetScale( new Vector2( 0.1f, 0.1f ) );
        }

        protected override void LoadContent( )
        {
        }

        protected override void Initialize( )
        {
            _logger.Log( ILogger.LogLevel.Info, "Game initializing." );

            // Add all game components
            Components.Add( _textureManager );
            Components.Add( _lander );
            Components.Add( _planet );

            _spriteBatch = new SpriteBatch( GraphicsDevice );
            Services.AddService( _spriteBatch );

            base.Initialize();
        }        

        protected override void Update( GameTime gameTime )
        {
            // We only control the _lander here
            ProcessPlayerInput( gameTime );

            ApplyPhysics( gameTime );

            base.Update( gameTime );
        }

        private void ApplyPhysics( GameTime gameTime )
        {
            // m/s^2 - y coordinate faces down
            //
            // From top left corner of screen
            //  ----x+
            // |
            // |
            // |
            // y+

            Vector2 gravity = new Vector2( 0, 9.8f );

            Vector2 accelerationFromThrust = _landerThrust / _landerMassInKg;

            if ( accelerationFromThrust.Length() < gravity.Length() && _landerThrust.Length() > 0 )
            {
                _logger.Log( ILogger.LogLevel.Info, "Can't fight gravity!" );
            }

            // Currently, just gravity--but we will need to figure out the thruster
            _landerAcceleration = gravity + accelerationFromThrust;

            // m/s^2 * s == m/s
            _landerVelocity += _landerAcceleration * (float)( gameTime.ElapsedGameTime.TotalSeconds );

            _logger.Log( ILogger.LogLevel.Info, $"Velocity: {_landerVelocity.Length()} m/s" );

            // m/s * s == m
            _lander.SetPosition( _lander.GetPosition() + _landerVelocity * (float)( gameTime.ElapsedGameTime.TotalSeconds ) );
        }

        private void ProcessPlayerInput( GameTime gameTime )
        {
            if ( GamePad.GetState( PlayerIndex.One ).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown( Keys.Escape ) )
                Exit();

            if ( Keyboard.GetState().IsKeyDown( Keys.W ) )
            {
                // TODO: Add thrust based on rotation
                _landerThrust = new Vector2( 0.0f, -2000.0f );
            }
            else
            {
                _landerThrust = Vector2.Zero;
            }

            if ( Keyboard.GetState().IsKeyDown( Keys.A ) )
            {
                // Rotate CCW                
                float newRotation = (float)( _lander.GetRotation() - 100 * gameTime.ElapsedGameTime.TotalSeconds );
                _lander.SetRotation( newRotation );
            }

            if ( Keyboard.GetState().IsKeyDown( Keys.D ) )
            {
                // Rotate CCW                
                float newRotation = (float)( _lander.GetRotation() + 100 * gameTime.ElapsedGameTime.TotalSeconds );
                _lander.SetRotation( newRotation );
            }

        }

        protected override void Draw( GameTime gameTime )
        {
            GraphicsDevice.Clear( Color.Black );

            base.Draw( gameTime );
        }
    }
}