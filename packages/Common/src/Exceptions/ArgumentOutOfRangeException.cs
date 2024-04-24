namespace Glekcraft.Common;

/// <summary>
/// An exception that is produced when an argument is invalid because it was
/// outside of the allowed range.
/// </summary>
/// <typeparam name="T">
/// The type of the argument.
/// </typeparam>
public class ArgumentOutOfRangeException<T> : InvalidArgumentException {
    #region Public Properties

    /// <summary>
    /// The actual value of the argument.
    /// </summary>
    public T ActualValue {
        get;
    }

    /// <summary>
    /// The minimum value of the argument.
    /// </summary>
    public T MinimumValue {
        get;
    }

    /// <summary>
    /// The maximum value of the argument.
    /// </summary>
    public T MaximumValue {
        get;
    }

    #endregion

    #region Constructors/Finalizer

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="actualValue">
    /// The actual value of the argument.
    /// </param>
    /// <param name="minimumValue">
    /// The minimum value of the argument.
    /// </param>
    /// <param name="maximumValue">
    /// The maximum value of the argument.
    /// </param>
    /// <param name="argumentName">
    /// The name of the argument that was invalid.
    /// </param>
    public ArgumentOutOfRangeException(T actualValue, T minimumValue, T maximumValue, string argumentName) : base(argumentName) {
        ActualValue = actualValue;
        MinimumValue = minimumValue;
        MaximumValue = maximumValue;
    }

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="actualValue">
    /// The actual value of the argument.
    /// </param>
    /// <param name="minimumValue">
    /// The minimum value of the argument.
    /// </param>
    /// <param name="maximumValue">
    /// The maximum value of the argument.
    /// </param>
    /// <param name="argumentName">
    /// The name of the argument that was invalid.
    /// </param>
    /// <param name="message">
    /// An optional string describing the nature of the exception.
    /// </param>
    public ArgumentOutOfRangeException(T actualValue, T minimumValue, T maximumValue, string argumentName, string? message) : base(argumentName, message) {
        ActualValue = actualValue;
        MinimumValue = minimumValue;
        MaximumValue = maximumValue;
    }

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="actualValue">
    /// The actual value of the argument.
    /// </param>
    /// <param name="minimumValue">
    /// The minimum value of the argument.
    /// </param>
    /// <param name="maximumValue">
    /// The maximum value of the argument.
    /// </param>
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
    public ArgumentOutOfRangeException(T actualValue, T minimumValue, T maximumValue, string argumentName, string? message, Exception? inner) : base(argumentName, message, inner) {
        ActualValue = actualValue;
        MinimumValue = minimumValue;
        MaximumValue = maximumValue;
    }

    #endregion
}
