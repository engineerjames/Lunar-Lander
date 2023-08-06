namespace LunarLanderGame
{
    using LunarLanderGame.Logging;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    using System.Collections.Generic;

    internal class TextureManager : DrawableGameComponent
    {
        private ContentManager contentManager;
        private Dictionary<string, Texture2D> textures;
        private ILogger logger;

        public TextureManager( Game game, ContentManager contentManager, ILogger logger )
             : base( game )
        {
            this.contentManager = contentManager;
            textures = new Dictionary<string, Texture2D>();
            this.logger = logger;
        }

        protected override void LoadContent( )
        {
            LoadAllTextures( );

            base.LoadContent();
        }

        public bool AllTexturesLoaded { get; private set; } = false;

        // Load a single texture and store it in the dictionary
        private void LoadTexture( string assetName )
        {
            logger.Log( ILogger.LogLevel.Info, $"Loading asset {assetName}..." );

            if ( !textures.ContainsKey( assetName ) )
            {
                Texture2D texture = contentManager.Load<Texture2D>( assetName );
                textures.Add( assetName, texture );
            }
        }

        // Load all textures from the content file
        private void LoadAllTextures( )
        {
            logger.Log( ILogger.LogLevel.Info, $"Loading all textures {assetName}..." );

            // Add all your texture asset names here -- TODO: Load from input file?
            string [] textureAssetNames = new string []
            {
                "lander"
            };

            foreach ( string assetName in textureAssetNames )
            {
                LoadTexture( assetName );
            }

            logger.Log( ILogger.LogLevel.Info, $"All textures loaded." );
        }

        // Retrieve a texture by its asset name
        public Texture2D GetTexture( string assetName )
        {
            if ( textures.TryGetValue( assetName, out Texture2D texture ) )
            {
                return texture;
            }

            // If the texture is not found, you can handle it here (e.g., log a warning)
            logger.Log( ILogger.LogLevel.Error, $"Unable to find texture with assetName: {assetName}" );
            return null;
        }
    }
}
