namespace Glekcraft;

using Glekcraft.Graphics.GLFW;

/// <summary>
/// The class containing the program entry point.
/// </summary>
public static class Program {
    /// <summary>
    /// The program entry point.
    /// </summary>
    /// <returns>
    /// A status code indicating whether the program completed successfully.
    /// </returns>
    public static int Main() {
        using var glfw = LibGLFW.Initialize();
        Console.WriteLine("Hello, World!");
        Console.WriteLine("Using GLFW v{0}", glfw.NativeVersionString);
        return (int)ExitCode.Success;
    }
}
