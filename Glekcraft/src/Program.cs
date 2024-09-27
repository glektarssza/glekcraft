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
        Console.WriteLine("Starting application...");
        var glfw = LibGLFW.Init();
        Console.WriteLine("GLFW initialized!");
        Console.WriteLine("Using GLFW v{0}", glfw.NativeVersionString);
        var monitors = glfw.Monitors;
        Console.WriteLine("Found {0} monitors!", monitors.Count);
        Console.WriteLine("=== Monitors ===");
        foreach (var monitor in monitors) {
            if (monitor.IsPrimary) {
                Console.Write("* ");
            } else {
                Console.Write("  ");
            }
            Console.WriteLine("{0}", monitor.Name ?? "Unknown");
        }
        return (int)ExitCode.Success;
    }
}
