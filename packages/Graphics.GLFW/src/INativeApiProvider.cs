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

    /// <summary>
    /// A callback that is invoked when a monitor is connected or disconnected
    /// from the system.
    /// </summary>
    /// <param name="monitor">
    /// The handle to the monitor which was connected or disconnected.
    /// </param>
    /// <param name="event">
    /// The event which occurred.
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

    #region Public Methods - Monitors

    /// <summary>
    /// Get a list of monitor handles connected to the system.
    /// </summary>
    /// <returns>
    /// A list of monitor handles connected to the system.
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
    /// Get the position of a monitor in the virtual desktop coordinate system.
    /// </summary>
    /// <param name="monitor">
    /// The handle to the monitor to get the position of.
    /// </param>
    /// <returns>
    /// The position of the monitor in the virtual desktop coordinate system.
    /// </returns>
    public (int xPos, int yPos)? GetMonitorPosition(IntPtr monitor);

    /// <summary>
    /// Get the work area of a monitor in the virtual desktop coordinate system.
    /// </summary>
    /// <param name="monitor">
    /// The handle to the monitor to get the work area of.
    /// </param>
    /// <returns>
    /// The work area of the monitor in the virtual desktop coordinate system.
    /// </returns>
    public (int xPos, int yPos, int width, int height)? GetMonitorWorkArea(IntPtr monitor);

    /// <summary>
    /// Get the physical size of a monitor, in millimeters.
    /// </summary>
    /// <param name="monitor">
    /// The handle to the monitor to get the physical size of.
    /// </param>
    /// <returns>
    /// The physical size of the monitor, in millimeters.
    /// </returns>
    public (int widthMM, int heightMM)? GetMonitorPhysicalSize(IntPtr monitor);

    /// <summary>
    /// Get the content scale of a monitor.
    /// </summary>
    /// <param name="monitor">
    /// The handle to the monitor to get the content scale of.
    /// </param>
    /// <returns>
    /// The content scale of the monitor.
    /// </returns>
    public (float xScale, float yScale)? GetMonitorContentScale(IntPtr monitor);

    /// <summary>
    /// Get the name of a monitor.
    /// </summary>
    /// <param name="monitor">
    /// The handle to the monitor to get the name of.
    /// </param>
    /// <returns>
    /// The name of the monitor.
    /// </returns>
    public string? GetMonitorName(IntPtr monitor);

    /// <summary>
    /// Set the user pointer of a monitor.
    /// </summary>
    /// <param name="monitor">
    /// The handle to the monitor to set the user pointer of.
    /// </param>
    /// <param name="userPtr">
    /// The user pointer to set for the monitor.
    /// </param>
    public void SetMonitorUserPointer(IntPtr monitor, IntPtr userPtr);

    /// <summary>
    /// Get the user pointer of a monitor.
    /// </summary>
    /// <param name="monitor">
    /// The handle to the monitor to get the user pointer of.
    /// </param>
    /// <returns>
    /// The user pointer of the monitor.
    /// </returns>
    public IntPtr GetMonitorUserPointer(IntPtr monitor);

    /// <summary>
    /// Set the callback to invoke when a monitor is connected or disconnected
    /// from the system.
    /// </summary>
    /// <param name="callback">
    /// The callback to invoke when a monitor is connected or disconnected from
    /// the system or <c>null</c> to clear the current callback.
    /// </param>
    /// <returns>
    /// The previously set callback or <c>null</c> if no callback was previously
    /// set.
    /// </returns>
    public MonitorCallback? SetMonitorCallback(MonitorCallback? callback);

    /// <summary>
    /// Get the available video modes for a monitor.
    /// </summary>
    /// <param name="monitor">
    /// The handle to the monitor to get the available video modes for.
    /// </param>
    /// <returns>
    /// The available video modes for the monitor.
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
