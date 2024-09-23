namespace Glekcraft.Graphics.GLFW;

/// <summary>
/// An enumeration of known ANGLE platform types.
/// </summary>
public enum AnglePlatform : int {
    /// <summary>
    /// No specific ANGLE platform.
    /// </summary>
    None = 0x00037001,

    /// <summary>
    /// The OpenGL ANGLE platform.
    /// </summary>
    OpenGL = 0x00037002,

    /// <summary>
    /// The OpenGL ES ANGLE platform.
    /// </summary>
    OpenGLES = 0x00037003,

    /// <summary>
    /// The Direct3D 9 ANGLE platform.
    /// </summary>
    D3D9 = 0x00037004,

    /// <summary>
    /// The Direct3D 11 ANGLE platform.
    /// </summary>
    D3D11 = 0x00037005,

    /// <summary>
    /// The Vulkan ANGLE platform.
    /// </summary>
    Vulkan = 0x00037007,

    /// <summary>
    /// The Metal ANGLE platform.
    /// </summary>
    /// <remarks>
    /// Only available on macOS.
    /// </remarks>
    Metal = 0x00037008
}
