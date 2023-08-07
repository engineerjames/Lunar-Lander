namespace LunarLanderGame.Components
{
    using LunarLanderGame.Logging;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    using System;

    internal class Lander : DrawableGameComponent
    {
        private Sprite _lander;
        // private Sprite _thruster;

        // Physics related variables
        private Vector2 _landerVelocity;
        private Vector2 _landerThrust;
        private Vector2 _landerAcceleration;
        private float _landerMassInKg;

        private ILogger _logger;

        public Lander( Game game, TextureManager textureManager, Vector2 initialPosition, ILogger logger ) : base( game )
        {
            _lander = new Sprite( game, "lander", textureManager, initialPosition );
            _lander.SetScale( new Vector2( 0.1f, 0.1f ) );
            _lander.SetPosition( initialPosition );

            // Physics related variables
            _landerVelocity = new Vector2( 0.0f, 15.0f );
            _landerThrust = Vector2.Zero;
            _landerAcceleration = Vector2.Zero;
            _landerMassInKg = 100.0f;

            _logger = logger;
        }

        public override void Initialize( )
        {
            Game.Components.Add( _lander );

            base.Initialize();
        }

        protected override void LoadContent( )
        {
            base.LoadContent();
        }

        public override void Draw( GameTime gameTime )
        {
            _lander.Draw( gameTime );
            //_thruster.Draw( gameTime );

            base.Draw( gameTime );
        }

        public override void Update( GameTime gameTime )
        {
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

            ApplyPhysics( gameTime );

            base.Update( gameTime );
        }

        private void ApplyPhysics(GameTime gameTime)
        {
            Vector2 gravity = new Vector2( 0, 9.8f );

            Vector2 accelerationFromThrust = _landerThrust / _landerMassInKg;

            if ( accelerationFromThrust.Length() < gravity.Length() && _landerThrust.Length() > 0 )
            {
                _logger.Log( ILogger.LogLevel.Info, "Can't fight gravity!" );
            }

            // Currently, just gravity--but we will need to figure out the thruster
            _landerAcceleration = ( gravity + accelerationFromThrust );

            // m/s^2 * s == m/s
            _landerVelocity += _landerAcceleration * (float)( gameTime.ElapsedGameTime.TotalSeconds );

            _logger.Log( ILogger.LogLevel.Info, $"Velocity: {_landerVelocity.Length()} m/s" );

            // m/s * s == m
            _lander.SetPosition( _lander.GetPosition() + _landerVelocity * (float)( gameTime.ElapsedGameTime.TotalSeconds ) );
        }
    }
}
