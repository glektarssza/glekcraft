namespace Glekcraft.Common;

/// <summary>
/// An exception that is produced when an argument is invalid.
/// </summary>
public class InvalidArgumentException : Exception {
    #region Public Properties

    /// <summary>
    /// The name of the argument that was invalid.
    /// </summary>
    public string ArgumentName {
        get;
    }

    #endregion

    #region Constructors/Finalizer

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="argumentName">
    /// The name of the argument that was invalid.
    /// </param>
    public InvalidArgumentException(string argumentName) : base() =>
        ArgumentName = argumentName;

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="argumentName">
    /// The name of the argument that was invalid.
    /// </param>
    /// <param name="message">
    /// An optional string describing the nature of the exception.
    /// </param>
    public InvalidArgumentException(string argumentName, string? message) : base(message) =>
        ArgumentName = argumentName;

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="argumentName">
    /// The name of the argument that was invalid.
    /// </param>
    /// <param name="message">
    /// An optional string describing the nature of the exception.
    /// </param>
    /// <param name="inner">
    /// An optional <see cref="Exception" /> that caused the new instance to be
    /// created.
    /// </param>
    public InvalidArgumentException(string argumentName, string? message, Exception? inner) : base(message, inner) =>
        ArgumentName = argumentName;

    #endregion
}
