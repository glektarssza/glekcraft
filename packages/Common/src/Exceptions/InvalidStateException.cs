namespace Glekcraft.Common;

/// <summary>
/// An exception that is produced when an operation fails due to an object being
/// in an invalid state.
/// </summary>
public class InvalidStateException : OperationFailedException {
    #region Public Properties

    /// <summary>
    /// The name of the object that was in an invalid state.
    /// </summary>
    public string ObjectName {
        get;
    }

    #endregion

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
    public InvalidStateException(string objectName, string operationName) : base(operationName) =>
        ObjectName = objectName;

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
    public InvalidStateException(string objectName, string operationName, string? message) : base(operationName, message) =>
        ObjectName = objectName;

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
    public InvalidStateException(string objectName, string operationName, string? message, Exception? inner) : base(operationName, message, inner) =>
        ObjectName = objectName;

    #endregion
}
