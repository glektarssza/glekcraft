namespace Glekcraft.Graphics.GLFW;

/// <summary>
/// An interface that defines that shape of an object that provides access to
/// the native GLFW APIs.
/// </summary>
public interface INativeAPIProvider {
    #region Public Delegates

    /// <summary>
    /// A callback that is invoked when an error occurs in the native library.
    /// </summary>
    /// <param name="error">
    /// The error code that occurred.
    /// </param>
    /// <param name="description">
    /// A description of the error that occurred.
    /// </param>
    public delegate void ErrorCallback(ErrorCode error, string? description);

    #endregion

    #region Public Methods

    /// <summary>
    /// Set an initialization hint for the native library.
    /// </summary>
    /// <param name="hint">
    /// The initialization hint to set.
    /// </param>
    /// <param name="value">
    /// The value to set the initialization hint to.
    /// </param>
    /// <remarks>
    /// This method should just be an alias for
    /// <see cref="InitHint(GLFW.InitHint, int)" />.
    /// </remarks>
    /// <seealso cref="InitHint(GLFW.InitHint, int)" />
    public void InitHint(InitHint hint, bool value);

    /// <summary>
    /// Set an initialization hint for the native library.
    /// </summary>
    /// <param name="hint">
    /// The initialization hint to set.
    /// </param>
    /// <param name="value">
    /// The value to set the initialization hint to.
    /// </param>
    /// <seealso cref="InitHint(GLFW.InitHint, bool)" />
    public void InitHint(InitHint hint, int value);

    /// <summary>
    /// Initialize the native library.
    /// </summary>
    /// <returns>
    /// <c>true</c> if the native library was initialized successfully;
    /// <c>false</c> otherwise.
    /// </returns>
    /// <see cref="Terminate" />
    public bool Init();

    /// <summary>
    /// Shut down the native library.
    /// </summary>
    /// <see cref="Init" />
    public void Terminate();

    /// <summary>
    /// Get the version of the native library being used.
    /// </summary>
    /// <returns>
    /// The version of the native library being used.
    /// </returns>
    /// <seealso cref="GetVersionString" />
    public Version? GetVersion();

    /// <summary>
    /// Get the version string of the native library being used.
    /// </summary>
    /// <returns>
    /// The version string of the native library being used.
    /// </returns>
    /// <seealso cref="GetVersion" />
    public string? GetVersionString();

    /// <summary>
    /// Get the last error that occurred in the native library.
    /// </summary>
    /// <param name="description">
    /// A description of the error that occurred.
    /// </param>
    /// <returns>
    /// The error code that occurred.
    /// </returns>
    public ErrorCode GetError(out string? description);

    /// <summary>
    /// Set a callback to be invoked when an error occurs in the native library.
    /// </summary>
    /// <param name="callback">
    /// The callback to invoke when an error occurs.
    /// </param>
    /// <returns>
    /// The previous error callback, if any.
    /// </returns>
    public ErrorCallback? SetErrorCallback(ErrorCallback? callback);

    /// <summary>
    /// Get the platform type that the native library is currently using.
    /// </summary>
    /// <returns>
    /// The platform type that the native library is currently using.
    /// </returns>
    public PlatformType GetPlatform();

    /// <summary>
    /// Check if a platform type is supported by the native library.
    /// </summary>
    /// <param name="platform">
    /// The platform type to check
    /// </param>
    /// <returns>
    /// <c>true</c> if the platform type is supported; <c>false</c> otherwise.
    /// </returns>
    public bool IsPlatformSupported(PlatformType platform);

    #endregion
}
