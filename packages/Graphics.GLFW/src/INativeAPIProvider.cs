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

    /// <summary>
    /// A delegate that can be called when a monitor event occurs.
    /// </summary>
    /// <param name="monitor">
    /// The monitor that the event occurred on.
    /// </param>
    /// <param name="event">
    /// The event that occurred on the monitor.
    /// </param>
    public delegate void MonitorCallback(IntPtr monitor, MonitorEvent @event);

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

    #region Public Methods - Monitors

    /// <summary>
    /// Get an array of handles to all connected monitors.
    /// </summary>
    /// <returns>
    /// An array of handles to all connected monitors.
    /// </returns>
    public IntPtr[] GetMonitors();

    /// <summary>
    /// Get the handle to the primary monitor.
    /// </summary>
    /// <returns>
    /// The handle to the primary monitor.
    /// </returns>
    public IntPtr GetPrimaryMonitor();

    /// <summary>
    /// Get the position of the monitor in screen coordinates.
    /// </summary>
    /// <param name="monitor">
    /// The handle to the monitor to get the position of.
    /// </param>
    /// <returns>
    /// The position of the monitor in screen coordinates.
    /// </returns>
    public (int xPos, int yPos) GetMonitorPos(IntPtr monitor);

    /// <summary>
    /// Get the work area of the monitor in screen coordinates.
    /// </summary>
    /// <param name="monitor">
    /// The handle to the monitor to get the work area of.
    /// </param>
    /// <returns>
    /// The work area of the monitor in screen coordinates.
    /// </returns>
    public (int xPos, int yPos, int width, int height) GetMonitorWorkarea(IntPtr monitor);

    /// <summary>
    /// Get the physical size of the monitor, in millimeters.
    /// </summary>
    /// <param name="monitor">
    /// The handle to the monitor to get the physical size of.
    /// </param>
    /// <returns>
    /// The physical size of the monitor, in millimeters.
    /// </returns>
    public (int widthMM, int heightMM) GetMonitorPhysicalSize(IntPtr monitor);

    /// <summary>
    /// Get the content scale of the monitor.
    /// </summary>
    /// <param name="monitor">
    /// The handle to the monitor to get the content scale of.
    /// </param>
    /// <returns>
    /// The content scale of the monitor.
    /// </returns>
    public (float xScale, float yScale) GetMonitorContentScale(IntPtr monitor);

    /// <summary>
    /// Get the name of the monitor.
    /// </summary>
    /// <param name="monitor">
    /// The handle to the monitor to get the name of.
    /// </param>
    /// <returns>
    /// The name of the monitor.
    /// </returns>
    public string? GetMonitorName(IntPtr monitor);

    /// <summary>
    /// Set the user pointer of the monitor.
    /// </summary>
    /// <param name="monitor">
    /// The handle to the monitor to set the user pointer of.
    /// </param>
    /// <param name="userPointer">
    /// The user pointer to set the user pointer to or
    /// <see cref="IntPtr.Zero" /> to clear the user pointer.
    /// </param>
    public void SetMonitorUserPointer(IntPtr monitor, IntPtr userPointer);

    /// <summary>
    /// Get the user pointer of the monitor.
    /// </summary>
    /// <param name="monitor">
    /// The handle to the monitor to get the user pointer of.
    /// </param>
    /// <returns>
    /// The user pointer of the monitor.
    /// </returns>
    public IntPtr GetMonitorUserPointer(IntPtr monitor);

    /// <summary>
    /// Set the callback to be called when a monitor event occurs.
    /// </summary>
    /// <param name="callback">
    /// The callback to be called when a monitor event occurs or <c>null</c>
    /// to clear the current callback.
    /// </param>
    /// <returns>
    /// The current callback being called when a monitor event occurs or
    /// <c>null</c> if no callback is currently set.
    /// </returns>
    public MonitorCallback? SetMonitorCallback(MonitorCallback? callback);

    /// <summary>
    /// Get the supported video modes of a monitor.
    /// </summary>
    /// <param name="monitor">
    /// The handle to the monitor to get the supported video modes of.
    /// </param>
    /// <returns>
    /// An array of supported video modes of the monitor.
    /// </returns>
    public VideoMode[] GetVideoModes(IntPtr monitor);

    /// <summary>
    /// Get the current video mode of a monitor.
    /// </summary>
    /// <param name="monitor">
    /// The handle to the monitor to get the current video mode of.
    /// </param>
    /// <returns>
    /// The current video mode of the monitor.
    /// </returns>
    public VideoMode? GetVideoMode(IntPtr monitor);

    /// <summary>
    /// Set the gamma of a monitor.
    /// </summary>
    /// <param name="monitor">
    /// The handle to the monitor to set the gamma of.
    /// </param>
    /// <param name="gamma">
    /// The gamma to set the monitor to.
    /// </param>
    public void SetGamma(IntPtr monitor, float gamma);

    /// <summary>
    /// Get the gamma ramp of a monitor.
    /// </summary>
    /// <param name="monitor">
    /// The handle to the monitor to get the gamma ramp of.
    /// </param>
    /// <returns>
    /// The gamma ramp of the monitor.
    /// </returns>
    public GammaRamp? GetGammaRamp(IntPtr monitor);

    /// <summary>
    /// Set the gamma ramp of a monitor.
    /// </summary>
    /// <param name="monitor">
    /// The handle to the monitor to set the gamma ramp of.
    /// </param>
    /// <param name="ramp">
    /// The gamma ramp to set the monitor to.
    /// </param>
    public void SetGammaRamp(IntPtr monitor, GammaRamp ramp);

    #endregion
}
