namespace Glekcraft.Common;

/// <summary>
/// An exception that is produced when an operation fails due to an object being
/// in an invalid state.
/// </summary>
public class ObjectDisposedException : InvalidStateException {
    #region Constructors/Finalizer

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="objectName">
    /// The name of the object that was in an invalid state.
    /// </param>
    /// <param name="operationName">
    /// The name of the operation that failed.
    /// </param>
    public ObjectDisposedException(string objectName, string operationName) : base(objectName, operationName) {
        // NOTE: Does nothing
    }

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="objectName">
    /// The name of the object that was in an invalid state.
    /// </param>
    /// <param name="operationName">
    /// The name of the operation that failed.
    /// </param>
    /// <param name="message">
    /// An optional string describing the nature of the exception.
    /// </param>
    public ObjectDisposedException(string objectName, string operationName, string? message) : base(objectName, operationName, message) {
        // NOTE: Does nothing
    }

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="objectName">
    /// The name of the object that was in an invalid state.
    /// </param>
    /// <param name="operationName">
    /// The name of the operation that failed.
    /// </param>
    /// <param name="message">
    /// An optional string describing the nature of the exception.
    /// </param>
    /// <param name="inner">
    /// An optional <see cref="Exception" /> that caused the new instance to be
    /// created.
    /// </param>
    public ObjectDisposedException(string objectName, string operationName, string? message, Exception? inner) : base(objectName, operationName, message, inner) {
        // NOTE: Does nothing
    }

    #endregion
}
