namespace Glekcraft.Graphics.GLFW;

/// <summary>
/// An enumeration of error codes supported by the native library.
/// </summary>
public enum ErrorCode : int {
    /// <summary>
    /// There has been no error.
    /// </summary>
    NoError = 0,

    /// <summary>
    /// The native library has not initialized.
    /// </summary>
    NotInitialized = 0x00010001,

    /// <summary>
    /// The native library has no graphics context current on the current
    /// thread.
    /// </summary>
    NoCurrentContext = 0x00010002,

    /// <summary>
    /// The given enumeration value was invalid.
    /// </summary>
    InvalidEnum = 0x00010003,

    /// <summary>
    /// The given value was invalid.
    /// </summary>
    InvalidValue = 0x00010004,

    /// <summary>
    /// The native library failed to allocate memory.
    /// </summary>
    OutOfMemory = 0x00010005,

    /// <summary>
    /// Support for the requested graphics API could not be found on the system.
    /// </summary>
    APIUnavailable = 0x00010006,

    /// <summary>
    /// Support for the requested version of the requested graphics API could
    /// not be found on the system.
    /// </summary>
    VersionUnavailable = 0x00010007,

    /// <summary>
    /// The underlying platform produced an error.
    /// </summary>
    PlatformError = 0x00010008,

    /// <summary>
    /// Support for the requested pixel format could not be found on the system.
    /// </summary>
    FormatUnavailable = 0x00010009,

    /// <summary>
    /// The given window did not have an OpenGL or OpenGL ES context associated
    /// with it.
    /// </summary>
    NoWindowContext = 0x0001000A,

    /// <summary>
    /// The specified cursor shape is not provided by the platform.
    /// </summary>
    CursorUnavailable = 0x0001000B,

    /// <summary>
    /// The requested feature is not provided by the platform.
    /// </summary>
    FeatureUnavailable = 0x0001000C,

    /// <summary>
    /// The requested feature is not implemented by the platform.
    /// </summary>
    FeatureUnimplemented = 0x0001000D,

    /// <summary>
    /// The requested platform or no matching platform was found.
    /// </summary>
    PlatformUnavailable = 0x0001000E
}
