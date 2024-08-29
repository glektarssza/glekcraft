namespace Glekcraft.Graphics.GLFW;

/// <summary>
/// An enumeration of error codes that can be produced by the native library.
/// </summary>
public enum ErrorCode : int {
    /// <summary>
    /// There have been no errors.
    /// </summary>
    NoError = 0,

    /// <summary>
    /// The native library has not been initialized.
    /// </summary>
    NotInitialized = 0x00010001,

    /// <summary>
    /// No context is current on the calling thread.
    /// </summary>
    NoCurrentContext = 0x00010002,

    /// <summary>
    /// The enumeration value provide was invalid.
    /// </summary>
    InvalidEnum = 0x00010003,

    /// <summary>
    /// The value provide was invalid.
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
    /// The underlying platform encountered an error.
    /// </summary>
    PlatformError = 0x00010008,

    /// <summary>
    /// The request graphics format is unavailable.
    /// </summary>
    FormatUnavailable = 0x00010009,

    /// <summary>
    /// The given window does not have an OpenGL or OpenGL ES context.
    /// </summary>
    NoWindowContext = 0x0001000A,

    /// <summary>
    /// The requested cursor shape is not available.
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
    /// The requested platform is not available.
    /// </summary>
    PlatformUnavailable = 0x0001000E
}
