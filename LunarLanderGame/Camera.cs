namespace LunarLanderGame
{
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework;

    public class Camera : DrawableGameComponent
    {
        public Matrix Transform { get; private set; }
        public Vector2 Position { get; set; }
        private Vector2 center;

        public Camera( Game game ) : base( game )
        {
            center = Vector2.Zero;
            Position = Vector2.Zero;
        }

        public void SetViewPort( Viewport viewport )
        {
            center = new Vector2( viewport.Width / 2, viewport.Height / 2 );
        }

        public void Update( Vector2 spritePosition )
        {
            Position = spritePosition - center;
            Transform = Matrix.CreateTranslation( new Vector3( -Position.X, -Position.Y, 0 ) );
        }
    }

}
