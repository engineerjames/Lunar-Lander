namespace LunarLanderGame.Components
{
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework;
    using System;

    public class Sprite : DrawableGameComponent
    {
        // TODO: Define these in one place.
        private static float DEGREES_TO_RADIANS = (float)( Math.PI / 180.0 );
        private static float RADIANS_TO_DEGREES = (float)( 180.0 / Math.PI );

        private Texture2D texture;
        private Vector2 position;
        private Color color;
        private float rotation;
        private Vector2 origin;
        private Vector2 scale;
        private TextureManager textureManager;
        private string textureName;

        bool isEnabled;

        public enum Origin
        {
            CENTER,
            CENTER_RIGHT,
            CENTER_LEFT,
            CENTER_TOP,
            CENTER_BOTTOM
        }

        private Origin _origin;

        public Sprite( Game game, string textureName, TextureManager textureManager, Vector2 initialPosition, Origin origin )
            : base( game )
        {
            this.position = initialPosition;
            color = Color.White;
            rotation = 0f;
            scale = Vector2.One;

            this.textureName = textureName;
            this.textureManager = textureManager;

            _origin = origin;

            isEnabled = true;
        }

        public override void Draw( GameTime gameTime )
        {
            if ( !isEnabled ) { return; }

            SpriteBatch spriteBatch = Game.Services.GetService<SpriteBatch>();

            spriteBatch.Begin();
            spriteBatch.Draw( texture, position, null, color, rotation, origin, scale, SpriteEffects.None, 0f );
            spriteBatch.End();

            base.Draw( gameTime );
        }

        protected override void LoadContent( )
        {
            texture = textureManager.GetTexture( textureName );

            switch ( _origin )
            {
                case Origin.CENTER:
                    origin = new Vector2( texture.Width / 2, texture.Height / 2 );
                    break;
                case Origin.CENTER_RIGHT:
                    origin = new Vector2( texture.Width, texture.Height / 2 );
                    break;
                case Origin.CENTER_LEFT:
                    origin = new Vector2( 0.0f, texture.Height / 2 );
                    break;
                case Origin.CENTER_TOP:
                    origin = new Vector2( texture.Width / 2, texture.Height );
                    break;
                case Origin.CENTER_BOTTOM:
                    origin = new Vector2( texture.Width / 2, 0.0f );
                    break;
                default:
                    // TODO: Add logging
                    throw new Exception( "Invalid Origin setting." );
            }

            base.LoadContent();
        }

        public Vector2 GetSize( )
        {
            return new Vector2( texture.Width, texture.Height );
        }

        // Helper methods to manipulate the sprite's properties
        public void SetPosition( Vector2 newPosition )
        {
            position = newPosition;
        }

        public Vector2 GetPosition( )
        {
            return position;
        }


        public void SetEnabled( bool isEnabled )
        {
            this.isEnabled = isEnabled;
        }

        public void SetColor( Color newColor )
        {
            color = newColor;
        }

        public Color GetColor( )
        {
            return color;
        }

        public void SetRotation( float newRotationDegrees )
        {
            // Internally the rotation is stored in radians, but we expect users to
            // give us the rotation in units of degrees
            rotation = newRotationDegrees * DEGREES_TO_RADIANS;
        }

        public float GetRotation( )
        {
            // Convert back to degrees from radians
            return rotation * RADIANS_TO_DEGREES;
        }

        public void SetOrigin( Vector2 newOrigin )
        {
            origin = newOrigin;
        }

        public Vector2 GetOrigin( )
        {
            return origin;
        }

        public void SetScale( Vector2 newScale )
        {
            scale = newScale;
        }
    }
}
