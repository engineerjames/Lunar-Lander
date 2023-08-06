namespace LunarLanderGame
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    class PlanetGenerator
    {
        // 
        //
        //
        // *                 *
        // *                 *
        public Planet GetDefaultPlanet( Game game, int width, int height )
        {
            var planet = new Planet(game);

            Color vertexColor = Color.Gray;

            int planetHeight = 150;

            // Setup vertices
            // Left triangle first
            planet.vertices.Add( new VertexPositionColor( new Vector3( 0.0f, height - planetHeight, 0.0f ), vertexColor ) );
            planet.vertices.Add( new VertexPositionColor( new Vector3( width, height, 0.0f ), vertexColor ) );
            planet.vertices.Add( new VertexPositionColor( new Vector3( 0.0f, height, 0.0f ), vertexColor ) );

            // Right triangle
            planet.vertices.Add( new VertexPositionColor( new Vector3( 0.0f, height - planetHeight, 0.0f ), vertexColor ) );
            planet.vertices.Add( new VertexPositionColor( new Vector3( width, height - planetHeight, 0.0f ), vertexColor ) );
            planet.vertices.Add( new VertexPositionColor( new Vector3( width, height, 0.0f ), vertexColor ) );

            return planet;
        }
    }
}
