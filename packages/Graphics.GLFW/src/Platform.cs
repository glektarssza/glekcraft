namespace Glekcraft.Graphics.GLFW;

/// <summary>
/// An enumeration of known platforms that the native library can support.
/// </summary>
public enum Platform : int {
    /// <summary>
    /// An initialization hint which indicates to automatically select an
    /// appropriate platform from all available platforms except the
    /// <see cref="Null"/> platform.
    /// </summary>
    Any = 0x00060000,

    /// <summary>
    /// A platform which interacts with Win32 on Windows.
    /// </summary>
    Win32 = 0x00060001,

    /// <summary>
    /// A platform which interacts with Cocoa on macOS.
    /// </summary>
    Cocoa = 0x00060002,

    /// <summary>
    /// A platform which interacts with Wayland on Linux.
    /// </summary>
    Wayland = 0x00060003,

    /// <summary>
    /// A platform which interacts with X11 on Linux.
    /// </summary>
    X11 = 0x00060004,

    /// <summary>
    /// A dummy platform which emulates a single 1080p monitor.
    /// </summary>
    Null = 0x00060005
}
