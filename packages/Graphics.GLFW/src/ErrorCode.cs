namespace Glekcraft.Graphics.GLFW;

// TODO: Add more detailed documentation on what the different errors can mean
//-- See https://www.glfw.org/docs/latest/group__errors.html for the full docs

/// <summary>
/// An enumeration of known error codes that can be surfaced from the native
/// library.
/// </summary>
/// <seealso href="https://www.glfw.org/docs/latest/group__errors.html" />
public enum ErrorCode : int {
    /// <summary>
    /// No errors have occurred.
    /// </summary>
    NoError = 0,

    /// <summary>
    /// The native library has not been initialized yet.
    /// </summary>
    NotInitialized = 0x00010001,

    /// <summary>
    /// The calling thread does not have an active graphics context on it.
    /// </summary>
    NoCurrentContext = 0x00010002,

    /// <summary>
    /// The enumeration value provided was not valid.
    /// </summary>
    InvalidEnum = 0x00010003,

    /// <summary>
    /// The value provided was not valid.
    /// </summary>
    InvalidValue = 0x00010004,

    /// <summary>
    /// A memory allocation within the native library failed.
    /// </summary>
    OutOfMemory = 0x00010005,

    /// <summary>
    /// Support for the requested graphics API could not be found on the system.
    /// </summary>
    ApiUnavailable = 0x00010006,

    /// <summary>
    /// Support for the requested version of the requested graphics API could
    /// not be found on the system.
    /// </summary>
    VersionUnavailable = 0x00010007,

    /// <summary>
    /// A catch-all error for platform-specific errors that do not fit into
    /// another, more specific, category.
    /// </summary>
    PlatformError = 0x00010008,

    /// <summary>
    /// The requested pixel format is not supported by the system.
    /// </summary>
    FormatUnavailable = 0x00010009,

    /// <summary>
    /// A window that lacks a graphics context was passed to a function that
    /// requires one.
    /// </summary>
    NoWindowContext = 0x0001000A,

    /// <summary>
    /// The requested standard cursor is not supported by the system.
    /// </summary>
    CursorUnavailable = 0x0001000B,

    /// <summary>
    /// The requested feature is not provided by the system and thus the native
    /// library is unable to provide it.
    /// </summary>
    FeatureUnavailable = 0x0001000C,

    /// <summary>
    /// The requested feature has not yet been implemented by the native library
    /// for this system.
    /// </summary>
    FeatureUnimplemented = 0x0001000D,

    /// <summary>
    /// No platform supported by the native library could be found.
    /// </summary>
    /// <remarks>
    /// If the platform is set to <see cref="Platform.Any" /> then this error
    /// means that the native library could not find a supported platform except
    /// for the <see cref="Platform.Null" /> platform. If the platform was set
    /// to a specific platform using the initialization hints then it means that
    /// platform was either not supported by the native library or the native
    /// library was not able to find it.
    ///
    /// This error can also occur when calling system functions if the native
    /// library was initialized for a different platform that the function is
    /// for.
    /// </remarks>
    PlatformUnavailable = 0x0001000E
}
