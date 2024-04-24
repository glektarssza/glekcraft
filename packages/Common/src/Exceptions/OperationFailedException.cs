namespace Glekcraft.Common;

/// <summary>
/// An exception that is produced when an operation fails.
/// </summary>
public class OperationFailedException : Exception {
    #region Public Properties

    /// <summary>
    /// The name of the operation that failed.
    /// </summary>
    public string OperationName {
        get;
    }

    #endregion

    #region Constructors/Finalizer

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="operationName">
    /// The name of the operation that failed.
    /// </param>
    public OperationFailedException(string operationName) : base() =>
        OperationName = operationName;

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="operationName">
    /// The name of the operation that failed.
    /// </param>
    /// <param name="message">
    /// An optional string describing the nature of the exception.
    /// </param>
    public OperationFailedException(string operationName, string? message) : base(message) =>
        OperationName = operationName;

    /// <summary>
    /// Create a new instance.
    /// </summary>
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
    public OperationFailedException(string operationName, string? message, Exception? inner) : base(message, inner) =>
        OperationName = operationName;

    #endregion
}
