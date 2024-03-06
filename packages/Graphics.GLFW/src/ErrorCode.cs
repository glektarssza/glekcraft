namespace Glekcraft.Graphics.GLFW;

/// <summary>
/// An enumeration of known error codes that can be returned from the native
/// library.
/// </summary>
public enum ErrorCode : int {
    /// <summary>
    /// There have been no errors.
    /// </summary>
    NoError = 0,

    /// <summary>
    /// The native library is not initialized.
    /// </summary>
    NotInitialized = 0x00010001,

    /// <summary>
    /// The calling thread has no active graphics context.
    /// </summary>
    NoCurrentContext = 0x00010002,

    /// <summary>
    /// The enumeration value provided was invalid.
    /// </summary>
    InvalidEnum = 0x00010003,

    /// <summary>
    /// The value provided was invalid.
    /// </summary>
    InvalidValue = 0x00010004,

    /// <summary>
    /// A memory allocation failed.
    /// </summary>
    OutOfMemory = 0x00010005,

    /// <summary>
    /// The requested graphics API was not available.
    /// </summary>
    APIUnavailable = 0x00010006,

    /// <summary>
    /// The requested version of the requested graphics API was not available.
    /// </summary>
    VersionUnavailable = 0x00010007,

    /// <summary>
    /// A generic platform-related error.
    /// </summary>
    PlatformError = 0x00010008,

    /// <summary>
    /// The requested graphics format is not available.
    /// </summary>
    FormatUnavailable = 0x00010009,

    /// <summary>
    /// The window passed has no graphics context associated with it.
    /// </summary>
    NoWindowContext = 0x0001000A
}
