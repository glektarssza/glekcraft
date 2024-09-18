namespace Glekcraft.Graphics.GLFW;

/// <summary>
/// An interface that defines the shape of objects that can provide access to
/// the native library's APIs.
/// </summary>
public interface INativeApiProvider {
    #region Public Delegates

    /// <summary>
    /// A callback that is invoked when an error occurs in the native library.
    /// </summary>
    /// <param name="code">
    /// The error code of the error that occurred.
    /// </param>
    /// <param name="description">
    /// An optional description of the error that occurred.
    /// </param>
    public delegate void ErrorCallback(ErrorCode code, string? description);

    #endregion

    #region Public Methods - Core

    /// <summary>
    /// Set an initialization hint.
    /// </summary>
    /// <param name="hint">
    /// The hint to set.
    /// </param>
    /// <param name="value">
    /// The value to set the hint to.
    /// </param>
    public void InitHint(InitHint hint, bool value);

    /// <summary>
    /// Set an initialization hint.
    /// </summary>
    /// <param name="hint">
    /// The hint to set.
    /// </param>
    /// <param name="value">
    /// The value to set the hint to.
    /// </param>
    public void InitHint(InitHint hint, int value);

    /// <summary>
    /// Initialize the native library.
    /// </summary>
    /// <returns>
    /// <c>true</c> if the native library was initialized successfully;
    /// <c>false</c> otherwise.
    /// </returns>
    public bool Init();

    /// <summary>
    /// Terminate the native library.
    /// </summary>
    public void Terminate();

    /// <summary>
    /// Get the version of the native library.
    /// </summary>
    /// <returns>
    /// The version of the native library.
    /// </returns>
    public Version? GetVersion();

    /// <summary>
    /// Get the version string of the native library.
    /// </summary>
    /// <returns>
    /// The version string of the native library.
    /// </returns>
    public string? GetVersionString();

    /// <summary>
    /// Get the last error encountered by the native library.
    /// </summary>
    /// <param name="description">
    /// A location to store the description of the error.
    /// </param>
    /// <returns>
    /// The error code of the last error encountered by the native library.
    /// </returns>
    public ErrorCode GetError(out string? description);

    /// <summary>
    /// Set the callback to invoke when an error occurs in the native library.
    /// </summary>
    /// <param name="callback">
    /// The callback to invoke when an error occurs in the native library or
    /// <c>null</c> to clear the current callback.
    /// </param>
    /// <returns>
    /// The previously set callback or <c>null</c> if no callback was previously
    /// set.
    /// </returns>
    public ErrorCallback? SetErrorCallback(ErrorCallback? callback);

    /// <summary>
    /// Get the platform the native library is using.
    /// </summary>
    /// <returns>
    /// The platform the native library is using.
    /// </returns>
    public Platform GetPlatform();

    /// <summary>
    /// Check if support for a platform is compiled into the native library.
    /// </summary>
    /// <param name="platform">
    /// The platform to check.
    /// </param>
    /// <returns>
    /// <c>true</c> support for a platform is compiled into the native library;
    /// <c>false</c> otherwise.
    /// </returns>
    public bool IsPlatformSupported(Platform platform);

    #endregion
}
