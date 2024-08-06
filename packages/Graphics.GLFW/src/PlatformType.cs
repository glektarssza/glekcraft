namespace Glekcraft.Graphics.GLFW;

/// <summary>
/// An enumeration of platform types supported by the native library.
/// </summary>
public enum PlatformType : int {
    /// <summary>
    /// Any platform.
    /// </summary>
    Any = 0x00060000,

    /// <summary>
    /// The Windows Win32 platform.
    /// </summary>
    Win32 = 0x00060001,

    /// <summary>
    /// The macOS Cocoa platform.
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
    /// The dummy null platform.
    /// </summary>
    Null = 0x00060005
}
