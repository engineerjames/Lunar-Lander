namespace LunarLanderGame.Components
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    using System.Collections.Generic;

    public class CameraComponent : DrawableGameComponent
    {
        private Camera camera;
        private SpriteBatch spriteBatch;
        private Lander _lander;
        private GraphicsDevice _graphicsDevice;
        private bool isLoaded = false;

        private List<DrawableGameComponent> drawables;

        public CameraComponent( Game game, Lander lander, GraphicsDevice graphicsDevice) : base( game )
        {
            _lander = lander;
            _graphicsDevice = graphicsDevice;
            camera = new Camera( game );
            drawables = new List<DrawableGameComponent>();
        }

        public void AddDrawable(DrawableGameComponent drawable )
        {
            drawables.Add( drawable );
        }

        public override void Initialize( )
        {
            base.Initialize();
        }

        protected override void LoadContent( )
        {
            spriteBatch = new SpriteBatch( _graphicsDevice );
            camera.SetViewPort( _graphicsDevice.Viewport );

            base.LoadContent();

            isLoaded = true;
        }

        public override void Update( GameTime gameTime )
        {
            // Update camera logic here
            Vector2 spritePosition = _lander.GetPosition();
            camera.Update( spritePosition );

            base.Update( gameTime );
        }

        public override void Draw( GameTime gameTime )
        {
            // Monogame calls Draw independently of LoadContent
            if (!isLoaded)
            {
                return;
            }

            spriteBatch.Begin( transformMatrix: camera.Transform );

            foreach ( DrawableGameComponent drawable in drawables )
            {
                drawable.Draw( gameTime );
            }

            spriteBatch.End();

            base.Draw( gameTime );
        }
    }
}
