namespace LunarLanderGame.Components
{
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;

    public class Planet : DrawableGameComponent
    {
        public List<VertexPositionColor> vertices;
        private BasicEffect _basicEffect;
        private Lander _lander;


        public Planet( Game game, Lander lander ) : base( game )
        {
            vertices = new List<VertexPositionColor>();
            _lander = lander;
        }

        protected override void LoadContent( )
        {
            _basicEffect = new BasicEffect( GraphicsDevice )
            {
                VertexColorEnabled = true,
            };

            base.LoadContent();
        }

        public override void Update( GameTime gameTime )
        {
            if ( _lander != null && !( _lander.HasExploded() || _lander.HasLanded() ) )
            {
                var position = _lander.GetPosition();

                // Just hard-code the ceiling for now;
                if ( position.Y > 900 )
                {
                    if ( _lander.GetSpeed() > 20.0f )
                    {
                        Debug.WriteLine( $"YOU DIED at {position.Y}" );
                        _lander.Explode();
                    }
                    else
                    {
                        Debug.WriteLine( $"Succesfully landed at {position.Y}" );
                        _lander.Land();
                    }
                }
            }

            base.Update( gameTime );
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
