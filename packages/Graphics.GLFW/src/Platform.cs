namespace Glekcraft.Graphics.GLFW;

/// <summary>
/// An enumeration of known platform types.
/// </summary>
public enum Platform : int {
    /// <summary>
    /// Automatic platform selection.
    /// </summary>
    Any = 0x00060000,

    /// <summary>
    /// The Windows platform.
    /// </summary>
    Win32 = 0x00060001,

    /// <summary>
    /// The macOS platform.
    /// </summary>
    Cocoa = 0x00060002,

    /// <summary>
    /// The Linux Wayland platform.
    /// </summary>
    Wayland = 0x00060003,

    /// <summary>
    /// The Linux X11 platform.
    /// </summary>
    X11 = 0x00060004,

    /// <summary>
    /// The dummy platform.
    /// </summary>
    Null = 0x00060005
}
