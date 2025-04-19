namespace Glekcraft;

using Glekcraft.Graphics.GLFW;

/// <summary>
/// The class containing the program entry point.
/// </summary>
public class Program {
    /// <summary>
    /// The program entry point.
    /// </summary>
    /// <returns>
    /// A status code for the operating system.
    /// </returns>
    public static int Main() {
        var version = LibGLFW.GetNativeVersion();
        Console.WriteLine("Using GLFW v{0}", version);
        Console.WriteLine("v{0}", LibGLFW.GetNativeVersionString());
        return (int)ExitCode.Ok;
    }
}
