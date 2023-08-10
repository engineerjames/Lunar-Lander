namespace LunarLanderGame.Components
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    using System.Collections.Generic;

    public class CameraComponent : DrawableGameComponent
    {
        private Camera camera;
        private Lander _lander;

        public CameraComponent( Game game, Lander lander) : base( game )
        {
            _lander = lander;
            camera = new Camera( game );
        }

        public override void Initialize( )
        {
            base.Initialize();
        }

        protected override void LoadContent( )
        {
            base.LoadContent();
        }

        public Matrix GetTransformationMatrix()
        {
            return camera.Transform;
        }

        public void SetViewPort(Viewport viewport)
        {
            camera.SetViewPort( viewport );
        }

        public override void Update( GameTime gameTime )
        {
            // Update camera logic here
            Vector2 spritePosition = _lander.GetPosition();
            camera.Update( spritePosition );

            base.Update( gameTime );
        }
    }
}
