namespace Glekcraft.Graphics.GLFW;

/// <summary>
/// An exception that is thrown when an error occurs in the native library.
/// </summary>
public class GLFWException : Exception {
    #region Public Properties

    /// <summary>
    /// The error code that occurred in the native library.
    /// </summary>
    public ErrorCode ErrorCode {
        get;
    }

    /// <summary>
    /// A human-readable description of the error that occurred in the native
    /// library.
    /// </summary>
    public string? ErrorDescription {
        get;
    }

    #endregion

    #region Constructors/Finalizer

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="code">
    /// The error code that occurred in the native library.
    /// </param>
    public GLFWException(ErrorCode code) : base() {
        this.ErrorCode = code;
        this.ErrorDescription = null;
    }

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="code">
    /// The error code that occurred in the native library.
    /// </param>
    /// <param name="inner">
    /// The exception that triggered the new instance to be created.
    /// </param>
    public GLFWException(ErrorCode code, Exception? inner) : base(null, inner) {
        this.ErrorCode = code;
        this.ErrorDescription = null;
    }

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="code">
    /// The error code that occurred in the native library.
    /// </param>
    /// <param name="description">
    /// An optional human-readable description of the error that occurred in the
    /// native library.
    /// </param>
    public GLFWException(ErrorCode code, string? description) : base() {
        this.ErrorCode = code;
        this.ErrorDescription = description;
    }

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="code">
    /// The error code that occurred in the native library.
    /// </param>
    /// <param name="description">
    /// An optional human-readable description of the error that occurred in the
    /// native library.
    /// </param>
    /// <param name="inner">
    /// The exception that triggered the new instance to be created.
    /// </param>
    public GLFWException(ErrorCode code, string? description, Exception? inner) : base(null, inner) {
        this.ErrorCode = code;
        this.ErrorDescription = description;
    }

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="code">
    /// The error code that occurred in the native library.
    /// </param>
    /// <param name="description">
    /// An optional human-readable description of the error that occurred in the
    /// native library.
    /// </param>
    /// <param name="message">
    /// An optional description of the error that occurred.
    /// </param>
    public GLFWException(ErrorCode code, string? description, string? message) : base(message) {
        this.ErrorCode = code;
        this.ErrorDescription = description;
    }

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="code">
    /// The error code that occurred in the native library.
    /// </param>
    /// <param name="description">
    /// An optional human-readable description of the error that occurred in the
    /// native library.
    /// </param>
    /// <param name="message">
    /// An optional description of the error that occurred.
    /// </param>
    /// <param name="inner">
    /// The exception that triggered the new instance to be created.
    /// </param>
    public GLFWException(ErrorCode code, string? description, string? message, Exception? inner) : base(message, inner) {
        this.ErrorCode = code;
        this.ErrorDescription = description;
    }

    #endregion
}
