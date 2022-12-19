namespace Glekcraft;

using System.Drawing;

using Silk.NET.Input;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

/// <summary>
/// The class containing the program entry point.
/// </summary>
public sealed class Program {
    /// <summary>
    /// The program entry point.
    /// </summary>
    public static void Main() {
        using var game = new Game();
        game.Initialize();
        game.Run();
        game.Dispose();
    }
}
