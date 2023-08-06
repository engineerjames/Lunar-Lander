namespace LunarLanderGame
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class PlanetGenerator
    {
        // 
        //
        //
        // *                 *
        // *                 *
        public Planet GetDefaultPlanet( int width, int height )
        {
            var planet = new Planet();

            Color vertexColor = Color.White;

            int planetHeight = 50;

            // Setup vertices
            // Left triangle first
            planet.vertices.Add( new VertexPositionColor( new Vector3( 0.0f, height - planetHeight, 0.0f ), vertexColor ) );
            planet.vertices.Add( new VertexPositionColor( new Vector3( width, height, 0.0f ), vertexColor ) );
            planet.vertices.Add( new VertexPositionColor( new Vector3( 0.0f, height, 0.0f ), vertexColor ) );

            // Right triangle
            //planet.vertices.Add( new VertexPositionColor( new Vector3( width, height, 0.0f ), vertexColor ) );
            //planet.vertices.Add( new VertexPositionColor( new Vector3( 0.0f, height - planetHeight, 0.0f ), vertexColor ) );
            //planet.vertices.Add( new VertexPositionColor( new Vector3( width, height - planetHeight, 0.0f ), vertexColor ) );

            return planet;
        }
    }
}
