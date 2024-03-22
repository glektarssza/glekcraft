namespace Glekcraft.Graphics.GLFW;

/// <summary>
/// A custom exception for when the native GLFW library encounters an error.
/// </summary>
public class GLFWException : Exception {
    #region Public Properties

    /// <summary>
    /// The error code raised by the native library.
    /// </summary>
    public ErrorCode NativeCode {
        get;
    }

    /// <summary>
    /// A description of the error raised by the native library.
    /// </summary>
    public string? NativeDescription {
        get;
    }

    #endregion

    #region Constructors/Finalizer

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="code">
    /// The error code raised by the native library.
    /// </param>
    /// <param name="description">
    /// A description of the error raised by the native library.
    /// </param>
    public GLFWException(ErrorCode code, string? description) : base() {
        NativeCode = code;
        NativeDescription = description;
    }

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="code">
    /// The error code raised by the native library.
    /// </param>
    /// <param name="description">
    /// A description of the error raised by the native library.
    /// </param>
    /// <param name="message">
    /// A string describing what went wrong.
    /// </param>
    public GLFWException(ErrorCode code, string? description, string? message) : base(message) {
        NativeCode = code;
        NativeDescription = description;
    }

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="code">
    /// The error code raised by the native library.
    /// </param>
    /// <param name="description">
    /// A description of the error raised by the native library.
    /// </param>
    /// <param name="message">
    /// A string describing what went wrong.
    /// </param>
    /// <param name="inner">
    /// The exception that triggered the creation of the new instance.
    /// </param>
    public GLFWException(ErrorCode code, string? description, string? message, Exception? inner) : base(message, inner) {
        NativeCode = code;
        NativeDescription = description;
    }

    #endregion
}
