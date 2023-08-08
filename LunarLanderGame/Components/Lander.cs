namespace LunarLanderGame.Components
{
    using LunarLanderGame.Logging;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    using System;

    public class Lander : DrawableGameComponent
    {
        private Sprite _lander;
        // private Sprite _thruster;

        // Physics related variables
        private Vector2 _landerVelocity;
        private Vector2 _landerAcceleration;
        private float _landerMassInKg;
        private float _thrustMagnitude;

        private ILogger _logger;

        private static float DEGREES_TO_RADIANS = (float)( Math.PI / 180.0 );

        public Lander( Game game, TextureManager textureManager, Vector2 initialPosition, ILogger logger ) : base( game )
        {
            _lander = new Sprite( game, "lander", textureManager, initialPosition );
            _lander.SetScale( new Vector2( 0.1f, 0.1f ) );
            _lander.SetPosition( initialPosition );

            // Physics related variables
            _landerVelocity = new Vector2( 0.0f, 15.0f );
            _thrustMagnitude = 0.0f;
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

        public void SetRotation( float angleInDegrees )
        {
            _lander.SetRotation( angleInDegrees );
        }

        public void SetThrustMagnitudeInNewtons( float thrustInNewtons )
        {
            if ( thrustInNewtons < 0.0f )
            {
                _logger.Log( ILogger.LogLevel.Error, "Cannot set a negative thrust magnitude." );
            }

            _thrustMagnitude = Math.Abs( thrustInNewtons );
        }

        public float GetThrustMagnitudeInNewtons( )
        {
            return _thrustMagnitude;
        }

        public float GetRotation( )
        {
            return _lander.GetRotation();
        }

        public Vector2 CalculateVectorizedThrust( )
        {
            // The rotation we obtain here is of the LANDER - 0° means the thrust vector is pointing straight up.
            // However, rotations in a vector sense are then 90° out of phase (0° points right across the X-axis).
            //
            // Also, Math.Cos and Math.Sin expect the angle to be provided in radians.
            float theta = ( 90 - GetRotation() ) * DEGREES_TO_RADIANS;
            float Vx = (float)( _thrustMagnitude * Math.Cos( theta ) );
            float Vy = (float)( _thrustMagnitude * Math.Sin( theta ) );

            // Since our coordinate system has Y facing down, we always invert the "Y" coordinate.
            _logger.Log( ILogger.LogLevel.Info, $"Vx: {Vx}, Vy: {-Vy}" );

            return new Vector2( Vx, -Vy );
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
                _thrustMagnitude = 2000.0f;
            }
            else
            {
                _thrustMagnitude = 0.0f;
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

        private void ApplyPhysics( GameTime gameTime )
        {
            Vector2 gravity = new Vector2( 0, 9.8f );

            Vector2 vectorizedThrust = CalculateVectorizedThrust();

            Vector2 accelerationFromThrust = vectorizedThrust / _landerMassInKg;

            if ( accelerationFromThrust.Length() < gravity.Length() && vectorizedThrust.Length() > 0 )
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
