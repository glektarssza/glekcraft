namespace Glekcraft;

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
