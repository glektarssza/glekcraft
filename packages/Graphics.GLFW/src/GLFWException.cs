namespace Glekcraft.Graphics.GLFW;

/// <summary>
/// An exception thrown when an error is encountered by the native library.
/// </summary>
public class GLFWException : Exception {
    #region Public Properties

    /// <summary>
    /// The error code of the error encountered by the native library.
    /// </summary>
    public ErrorCode NativeErrorCode {
        get;
    }

    /// <summary>
    /// The description of the error encountered by the native library.
    /// </summary>
    public string? NativeErrorDescription {
        get;
    }

    #endregion

    #region Constructors/Finalizer

    /// <summary>
    /// Create a new instance.
    /// </summary>
    public GLFWException() : base() {
        NativeErrorCode = LibGLFW.LastErrorCode ?? ErrorCode.NoError;
        NativeErrorDescription = LibGLFW.LastErrorDescription;
    }

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="message">
    /// A description of the nature of the exception.
    /// </param>
    public GLFWException(string message) : base(message) {
        NativeErrorCode = LibGLFW.LastErrorCode ?? ErrorCode.NoError;
        NativeErrorDescription = LibGLFW.LastErrorDescription;
    }

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="message">
    /// A description of the nature of the exception.
    /// </param>
    /// <param name="inner">
    /// The exception that caused the new instance to be created.
    /// </param>
    public GLFWException(string message, Exception inner) : base(message, inner) {
        NativeErrorCode = LibGLFW.LastErrorCode ?? ErrorCode.NoError;
        NativeErrorDescription = LibGLFW.LastErrorDescription;
    }

    #endregion
}
