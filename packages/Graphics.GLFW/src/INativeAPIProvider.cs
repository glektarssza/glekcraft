namespace Glekcraft.Graphics.GLFW;

/// <summary>
/// An interface that defines the shape of objects that provide access to the
/// native GLFW APIs.
/// </summary>
public interface INativeAPIProvider {
    #region Delegates

    /// <summary>
    /// A delegate for handling when the native library encounters an error.
    /// </summary>
    /// <param name="code">
    /// The error code of the error encountered by the native library.
    /// </param>
    /// <param name="description">
    /// A description, if available, of the error encountered by the native
    /// library.
    /// </param>
    public delegate void ErrorCallback(ErrorCode code, string? description);

    #endregion

    #region Methods

    /// <summary>
    /// Initialize the native library.
    /// </summary>
    /// <returns>
    /// <c>true</c> if the native library initialized successfully; <c>false</c>
    /// otherwise.
    /// </returns>
    public bool Init();

    /// <summary>
    /// Shut down the native library.
    /// </summary>
    public void Terminate();

    /// <summary>
    /// Set an initialization hint for the native library.
    /// </summary>
    /// <param name="hint">
    /// The hint to set the value of.
    /// </param>
    /// <param name="value">
    /// The value to set the given hint to.
    /// </param>
    public void InitHint(InitHint hint, int value);

    /// <summary>
    /// Set an initialization hint for the native library.
    /// </summary>
    /// <param name="hint">
    /// The hint to set the value of.
    /// </param>
    /// <param name="value">
    /// The value to set the given hint to.
    /// </param>
    public void InitHint(InitHint hint, bool value);

    /// <summary>
    /// Get the version of the native library being used.
    /// </summary>
    /// <returns>
    /// The version of the native library being used.
    /// </returns>
    public Version? GetVersion();

    /// <summary>
    /// Get the version string of the native library being used.
    /// </summary>
    /// <returns>
    /// The version string of the native library being used.
    /// </returns>
    public string? GetVersionString();

    /// <summary>
    /// Get the last error that the native library encountered.
    /// </summary>
    /// <param name="description">
    /// A location to store the description of the error in, if any.
    /// </param>
    /// <returns>
    /// The error code of the last error that the native library encountered.
    /// </returns>
    public ErrorCode GetError(out string? description);

    /// <summary>
    /// Set the method to call when the native library encounters an error.
    /// </summary>
    /// <param name="callback">
    /// The method to call when the native library encounters an error or
    /// <c>null</c> to remove an existing callback.
    /// </param>
    /// <returns>
    /// The existing callback, if any.
    /// </returns>
    public ErrorCallback? SetErrorCallback(ErrorCallback? callback);

    #endregion
}
