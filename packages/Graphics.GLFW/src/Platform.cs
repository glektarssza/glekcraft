namespace Glekcraft.Graphics.GLFW;

/// <summary>
/// An enumeration of platforms supported by the native library.
/// </summary>
public enum Platform : int {
    /// <summary>
    /// A value indicating any platform is acceptable.
    /// </summary>
    Any = 0x00060000,

    /// <summary>
    /// A value indicating a Win32-compatible platform is desired/in use.
    /// </summary>
    Win32 = 0x00060001,

    /// <summary>
    /// A value indicating a Cocoa-compatible platform is desired/in use.
    /// </summary>
    Cocoa = 0x00060002,

    /// <summary>
    /// A value indicating a Cocoa-compatible platform is desired/in use.
    /// </summary>
    Wayland = 0x00060003,

    /// <summary>
    /// A value indicating an X11-compatible platform is desired/in use.
    /// </summary>
    X11 = 0x00060004,

    /// <summary>
    /// A special value indicating a stubbed out platform is desired/in use.
    /// </summary>
    Null = 0x00060005
}
