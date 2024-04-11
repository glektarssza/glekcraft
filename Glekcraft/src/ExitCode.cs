namespace Glekcraft;

/// <summary>
/// A code that can be returned to the operating system to indicate the status
/// with which the program exited.
/// </summary>
public enum ExitCode : int {
    /// <summary>
    /// The program exited normally.
    /// </summary>
    Success = 0,

    /// <summary>
    /// The program did not exit normally.
    /// </summary>
    Failure = 1
}
