using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo( "LunarLanderTests" )]

internal class Program
{
    private static void Main( string [] args )
    {
        using var game = new LunarLanderGame.Game1();
        game.Run();
    }
}