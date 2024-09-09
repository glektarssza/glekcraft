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
        var monitors = glfw.Monitors;
        Console.WriteLine("Found {0} monitors", monitors.Length);
        Console.WriteLine("=== Monitors ===");
        foreach (var monitor in monitors) {
            Console.WriteLine("{0} {1}", monitor.IsPrimary ? "*" : " ", monitor.Name ?? "Unknown");
        }
        return (int)ExitCode.Success;
    }
}
