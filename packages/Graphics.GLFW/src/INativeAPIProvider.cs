namespace Glekcraft.Graphics.GLFW;

/// <summary>
///
/// </summary>
public interface INativeAPIProvider {
    #region Public Delegates

    /// <summary>
    /// A delegate that can be called when the native library encounters an
    /// error.
    /// </summary>
    /// <param name="code">
    /// The error code of the error encountered by the native library.
    /// </param>
    /// <param name="description">
    /// The description of the error encountered by the native library if one
    /// was available; <c>null</c> otherwise.
    /// </param>
    public delegate void ErrorCallback(ErrorCode code, string? description);

    #endregion

    #region Public Methods

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
    /// Initialize the native library.
    /// </summary>
    /// <returns>
    /// <c>true</c> on successful initialization; <c>false</c> otherwise.
    /// </returns>
    public bool Init();

    /// <summary>
    /// Shut down the native library.
    /// </summary>
    public void Terminate();

    /// <summary>
    /// Get the version of the native library being used.
    /// </summary>
    /// <returns>
    /// The version of the native library being used on success; <c>null</c> on
    /// failure.
    /// </returns>
    public Version? GetVersion();

    /// <summary>
    /// Get the version string of the native library being used.
    /// </summary>
    /// <returns>
    /// The version string of the native library being used on success;
    /// <c>null</c> on failure.
    /// </returns>
    public string? GetVersionString();

    /// <summary>
    /// Get the last error encountered by the native library.
    /// </summary>
    /// <param name="description">
    /// The location to store the description of the last error encountered by
    /// the native library.
    /// </param>
    /// <returns>
    /// The error code of the last error encountered by the native library.
    /// </returns>
    public ErrorCode GetError(out string? description);

    /// <summary>
    /// Set the callback to be called by the native library when it encounters
    /// an error.
    /// </summary>
    /// <param name="callback">
    /// The callback to be called by the native library when it encounters
    /// an error or <c>null</c> to clear the current callback.
    /// </param>
    /// <returns>
    /// The current callback being called by the native library when it
    /// encounters an error or <c>null</c> is no callback is currently set.
    /// </returns>
    public ErrorCallback? SetErrorCallback(ErrorCallback? callback);

    /// <summary>
    /// Get the platform currently being used by the native library.
    /// </summary>
    /// <returns>
    /// The platform currently being used by the native library.
    /// </returns>
    public Platform GetPlatform();

    /// <summary>
    /// Check if a platform is supported by the native library.
    /// </summary>
    /// <param name="platform">
    /// The platform to check.
    /// </param>
    /// <returns>
    /// <c>true</c> if a platform is supported by the native library;
    /// <c>false</c> otherwise.
    /// </returns>
    public bool IsPlatformSupported(Platform platform);

    #endregion
}