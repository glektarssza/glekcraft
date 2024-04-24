namespace Glekcraft.Common;

/// <summary>
/// An exception that is produced when an argument is invalid because it was
/// <c>null</c>.
/// </summary>
public class ArgumentNullException : InvalidArgumentException {
    #region Constructors/Finalizer

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="argumentName">
    /// The name of the argument that was invalid.
    /// </param>
    public ArgumentNullException(string argumentName) : base(argumentName) {
        // NOTE: Does nothing
    }

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="argumentName">
    /// The name of the argument that was invalid.
    /// </param>
    /// <param name="message">
    /// An optional string describing the nature of the exception.
    /// </param>
    public ArgumentNullException(string argumentName, string? message) : base(argumentName, message) {
        // NOTE: Does nothing
    }

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
    public ArgumentNullException(string argumentName, string? message, Exception? inner) : base(argumentName, message, inner) {
        // NOTE: Does nothing
    }

    #endregion
}
