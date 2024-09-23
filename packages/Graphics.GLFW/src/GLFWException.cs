namespace Glekcraft.Graphics.GLFW;

/// <summary>
/// An exception that is produced when the native library encounters an error.
/// </summary>
public class GLFWException : Exception {
    #region Public Properties

    /// <summary>
    /// The error code of the error that was surfaced by the native library.
    /// </summary>
    public ErrorCode NativeErrorCode {
        get;
    }

    /// <summary>
    /// The description of the error that was surfaced by the native library.
    /// </summary>
    public string? NativeErrorDescription {
        get;
    }

    #endregion

    #region Constructors/Finalizer

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="errorCode">
    /// The error code of the error that was surfaced by the native library.
    /// </param>
    /// <param name="errorDescription">
    /// The description of the error that was surfaced by the native library.
    /// </param>
    public GLFWException(ErrorCode errorCode, string? errorDescription) : base() {
        NativeErrorCode = errorCode;
        NativeErrorDescription = errorDescription;
    }

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="errorCode">
    /// The error code of the error that was surfaced by the native library.
    /// </param>
    /// <param name="errorDescription">
    /// The description of the error that was surfaced by the native library.
    /// </param>
    /// <param name="message">
    /// The message describing the error.
    /// </param>
    public GLFWException(ErrorCode errorCode, string? errorDescription, string? message) : base(message) {
        NativeErrorCode = errorCode;
        NativeErrorDescription = errorDescription;
    }

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="errorCode">
    /// The error code of the error that was surfaced by the native library.
    /// </param>
    /// <param name="errorDescription">
    /// The description of the error that was surfaced by the native library.
    /// </param>
    /// <param name="message">
    /// The message describing the error.
    /// </param>
    /// <param name="inner">
    /// The inner exception that caused the new instance to be created.
    /// </param>
    public GLFWException(ErrorCode errorCode, string? errorDescription, string? message, Exception? inner) : base(message, inner) {
        NativeErrorCode = errorCode;
        NativeErrorDescription = errorDescription;
    }

    #endregion
}
