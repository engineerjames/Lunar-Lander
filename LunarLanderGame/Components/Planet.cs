namespace LunarLanderGame.Components
{
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class Planet : DrawableGameComponent
    {
        public List<VertexPositionColor> vertices;
        private BasicEffect _basicEffect;


        public Planet( Game game ) : base( game )
        {
            vertices = new List<VertexPositionColor>();
        }

        protected override void LoadContent( )
        {
            _basicEffect = new BasicEffect( GraphicsDevice )
            {
                VertexColorEnabled = true,
            };

            base.LoadContent();
        }

        public override void Draw( GameTime gameTime )
        {
            // Draw planet (TODO: Register as a component instead)
            GraphicsDevice.RasterizerState = new RasterizerState() { FillMode = FillMode.Solid, MultiSampleAntiAlias = true };
            CameraComponent cameraComponent = Game.Services.GetService<CameraComponent>();

            _basicEffect.View = cameraComponent.GetTransformationMatrix();
            _basicEffect.World = Matrix.Identity;
            _basicEffect.Projection = Matrix.CreateOrthographicOffCenter( 0, GraphicsDevice.Viewport.Width,
                                                                        GraphicsDevice.Viewport.Height, 0, 0, 1 );
           
            
            _basicEffect.CurrentTechnique.Passes [ 0 ].Apply();
            GraphicsDevice.DrawUserPrimitives( PrimitiveType.TriangleList, vertices.ToArray(), 0, 2 );

            base.Draw( gameTime );
        }


    }
}
