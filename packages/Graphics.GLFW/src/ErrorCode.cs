namespace Glekcraft.Graphics.GLFW;

/// <summary>
/// An enumeration of error codes that can be returned from the native library.
/// </summary>
public enum ErrorCode : int {
    /// <summary>
    /// No error has occurred.
    /// </summary>
    NoError = 0,

    /// <summary>
    /// The native library has not been initialized.
    /// </summary>
    NotInitialized = 0x00010001,

    /// <summary>
    /// No context is current for this thread.
    /// </summary>
    NoCurrentContext = 0x00010002,

    /// <summary>
    /// One of the arguments to the function was an invalid enum value.
    /// </summary>
    InvalidEnum = 0x00010003,

    /// <summary>
    /// One of the arguments to the function was an invalid value.
    /// </summary>
    InvalidValue = 0x00010004,

    /// <summary>
    /// A memory allocation failed.
    /// </summary>
    OutOfMemory = 0x00010005,

    /// <summary>
    /// The requested graphics API is unavailable.
    /// </summary>
    ApiUnavailable = 0x00010006,

    /// <summary>
    /// The requested graphics API version is unavailable.
    /// </summary>
    VersionUnavailable = 0x00010007,

    /// <summary>
    /// A platform-specific error occurred.
    /// </summary>
    PlatformError = 0x00010008,

    /// <summary>
    /// The requested graphics format is unavailable.
    /// </summary>
    FormatUnavailable = 0x00010009,

    /// <summary>
    /// The specified window does not have an OpenGL or OpenGL ES context.
    /// </summary>
    NoWindowContext = 0x0001000A,

    /// <summary>
    /// The specified cursor shape is unavailable.
    /// </summary>
    CursorUnavailable = 0x0001000B,

    /// <summary>
    /// The specified feature is not supported by the platform.
    /// </summary>
    FeatureUnavailable = 0x0001000C,

    /// <summary>
    /// The requested feature is not implemented by the platform.
    /// </summary>
    FeatureUnimplemented = 0x0001000D,

    /// <summary>
    /// The requested platform is unavailable.
    /// </summary>
    PlatformUnavailable = 0x0001000E,
}
