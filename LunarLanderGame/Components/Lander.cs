namespace LunarLanderGame.Components
{
    using Microsoft.Xna.Framework;

    internal class Lander : DrawableGameComponent
    {
        private Sprite _lander;
        private Sprite _thruster;

        // Physics related variables
        private Vector2 _landerVelocity;
        private Vector2 _landerThrust;
        private Vector2 _landerAcceleration;
        private float _landerMassInKg;

        public Lander( Game game ) : base( game )
        {
        }

        protected override void LoadContent( )
        {
            base.LoadContent();
        }

        public override void Draw( GameTime gameTime )
        {
            base.Draw( gameTime );
        }

        public override void Update( GameTime gameTime )
        {
            base.Update( gameTime );
        }

        private void ApplyPhysics(GameTime gameTime)
        {

        }
    }
}
