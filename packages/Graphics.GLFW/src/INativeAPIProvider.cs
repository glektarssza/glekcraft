namespace Glekcraft.Graphics.GLFW;

/// <summary>
/// An interface which provides the shape of the native APIs to be wrapped by
/// this library.
/// </summary>
public interface INativeAPIProvider {
    #region Public Delegates

    /// <summary>
    /// A callback which is triggered when an error occurs in the native
    /// library.
    /// </summary>
    /// <param name="code">
    /// The error code which occurred.
    /// </param>
    /// <param name="description">
    /// An optional human-readable description of the error that occurred.
    /// </param>
    public delegate void ErrorCallback(ErrorCode code, string description);

    #endregion

    #region Public Methods

    /// <summary>
    /// Configure initialization hints for the native library.
    /// </summary>
    public void InitHint(int hint, bool value);

    /// <summary>
    /// Configure initialization hints for the native library.
    /// </summary>
    public void InitHint(int hint, int value);

    /// <summary>
    /// Set the functions to be used by the native library for allocating heap
    /// memory.
    /// </summary>
    /// <param name="allocator">
    /// A structure containing the functions to be used by the native library or
    /// <c>null</c> to use the default one.
    /// </param>
    public void InitAllocator(NativeAllocator? allocator);

    /// <summary>
    /// Initialize Vulkan.
    /// </summary>
    /// <param name="loader">
    /// A pointer to the <c>vkGetInstanceProcAddr</c> function to use or
    /// <see cref="IntPtr.Zero" />.
    /// </param>
    public void InitVulkanLoader(IntPtr loader);

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
    /// Get the version of the native library being used.
    /// </summary>
    /// <returns>
    /// The version of the native library being used.
    /// </returns>
    public Version GetVersion();

    /// <summary>
    /// Get the version string of the native library being used.
    /// </summary>
    /// <returns>
    /// The version string of the native library being used.
    /// </returns>
    public string GetVersionString();

    /// <summary>
    /// Get the last error that occurred in the native library.
    /// </summary>
    /// <param name="description">
    /// The location to store the human-readable description of the error in.
    /// </param>
    /// <returns>
    /// The last error that occurred in the native library.
    /// </returns>
    public ErrorCode GetError(out string? description);

    /// <summary>
    /// Set the method to be called when an error occurs in the native library.
    /// </summary>
    /// <param name="callback">
    /// The callback to be triggered when an error occurs in the native library
    /// or <c>null</c> to clear the current callback.
    /// </param>
    /// <returns>
    /// The previously set callback or <c>null</c> if there was no previously
    /// set callback.
    /// </returns>
    public ErrorCallback? SetErrorCallback(ErrorCallback? callback);

    /// <summary>
    /// Get the platform the native library is currently using.
    /// </summary>
    /// <returns>
    /// The platform the native library is currently using.
    /// </returns>
    public Platform GetPlatform();

    /// <summary>
    /// Check whether the given platform is supported by the native library.
    /// </summary>
    /// <returns>
    /// <c>true</c> if the given platform is supported by the native library;
    /// <c>false</c> otherwise.
    /// </returns>
    public bool IsPlatformSupported(Platform platform);

    #endregion
}
