namespace Glekcraft.Graphics.GLFW;

/// <summary>
/// The main class for interacting with the GLFW library.
/// </summary>
public sealed class LibGLFW : IDisposable {
    #region Internal Static Properties

    /// <summary>
    /// The singleton instance of this class.
    /// </summary>
    internal static LibGLFW? Instance {
        get;
        private set;
    }

    #endregion

    #region Public Static Properties

    /// <summary>
    /// Whether the native library has been initialized.
    /// </summary>
    public static bool IsInitialized => Instance != null;

    #endregion

    #region Public Static Methods

    /// <summary>
    /// Initialize the GLFW library.
    /// </summary>
    /// <param name="hints">
    /// The initialization hints to use or <c>null</c> to use the default hints.
    /// </param>
    /// <param name="apiProvider">
    /// The native API provider to use or <c>null</c> to use the default
    /// provider.
    /// </param>
    /// <returns>
    /// If the native library is already initialized, the existing instance of
    /// this class is returned; a new instance otherwise.
    /// </returns>
    /// <exception cref="GLFWException">
    /// Thrown if a new instance needs to be created and the native library
    /// fails to initialize.
    /// </exception>
    public static LibGLFW Init(InitHints? hints = null, INativeApiProvider? apiProvider = null) {
        if (Instance != null) {
            return Instance;
        }
        var nativeApi = apiProvider ?? new DefaultNativeApiProvider();
        //-- Apply initialization hints
        if (hints.HasValue) {
            nativeApi.InitHint(InitHint.JoystickHatButtons, hints.Value.JoystickHatButtons);
            nativeApi.InitHint(InitHint.AnglePlatformType, (int)hints.Value.AnglePlatformType);
            nativeApi.InitHint(InitHint.Platform, (int)hints.Value.Platform);
            nativeApi.InitHint(InitHint.CocoaChdirResources, hints.Value.CocoaChdirResources);
            nativeApi.InitHint(InitHint.CocoaMenubar, hints.Value.CocoaMenubar);
            nativeApi.InitHint(InitHint.X11XcbVulkanSurface, hints.Value.X11XcbVulkanSurface);
            nativeApi.InitHint(InitHint.WaylandLibdecor, hints.Value.WaylandLibdecor ? 0x00038001 : 0x00038002);
        }
        if (!nativeApi.Init()) {
            var errorCode = nativeApi.GetError(out var description);
            throw new GLFWException(errorCode, description, "Failed to initialize the native GLFW library");
        }
        Instance = new LibGLFW(nativeApi);
        Instance.PostInit();
        return Instance;
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// The native API provider this instance is using.
    /// </summary>
    public INativeApiProvider NativeApi {
        get;
    }

    /// <summary>
    /// The error code of the last error that was set by the native library.
    /// </summary>
    public ErrorCode LastErrorCode {
        get;
        private set;
    }

    /// <summary>
    /// The description of the last error that was set by the native library.
    /// </summary>
    public string? LastErrorDescription {
        get;
        private set;
    }

    /// <summary>
    /// The version of the native library being used.
    /// </summary>
    public Version? NativeVersion => NativeApi.GetVersion();

    /// <summary>
    /// The version string of the native library being used.
    /// </summary>
    public string? NativeVersionString => NativeApi.GetVersionString();

    /// <summary>
    /// Whether this instance is the one managing the native library.
    /// </summary>
    public bool IsCurrentInstance => Instance == this;

    /// <summary>
    /// Whether this instance has been disposed.
    /// </summary>
    public bool IsDisposed {
        get;
        private set;
    }

    #endregion

    #region Constructors/Finalizer

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="apiProvider">
    /// The native API provider to use.
    /// </param>
    private LibGLFW(INativeApiProvider apiProvider) {
        NativeApi = apiProvider;
        LastErrorCode = ErrorCode.NoError;
        _ = NativeApi.SetErrorCallback((code, description) => {
            LastErrorCode = code;
            LastErrorDescription = description;
        });
    }

    /// <summary>
    /// The finalizer.
    /// </summary>
    ~LibGLFW() =>
        Dispose(false);

    #endregion

    #region Public Methods

    /// <summary>
    /// Clear the error code of the last error that was set by the native
    /// library.
    /// </summary>
    public void ClearLastErrorCode() =>
        LastErrorCode = ErrorCode.NoError;

    /// <summary>
    /// Clear the description of the last error that was set by the native
    /// library.
    /// </summary>
    public void ClearLastErrorDescription() =>
        LastErrorDescription = null;

    /// <summary>
    /// Clear the error code and description of the last error that was set by
    /// the native library.
    /// </summary>
    public void ClearLastError() {
        ClearLastErrorCode();
        ClearLastErrorDescription();
    }

    /// <summary>
    /// Dispose of this instance.
    /// </summary>
    public void Terminate() =>
        Dispose();

    /// <summary>
    /// Dispose of this instance.
    /// </summary>
    public void Dispose() {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    #endregion

    #region Private Methods

#pragma warning disable CA1822 // Mark members as static
    /// <summary>
    /// Perform any post-initialization steps that require the
    /// <see cref="Instance" /> property to be set.
    /// </summary>
    private void PostInit() {
        // TODO
    }
#pragma warning restore CA1822 // Mark members as static

    /// <summary>
    /// Dispose of this instance.
    /// </summary>
    /// <param name="managed">
    /// Whether this method is being called from managed code or from unmanaged
    /// code (e.g. the garbage collector).
    /// </param>
    private void Dispose(bool managed) {
        if (IsDisposed) {
            return;
        }
        if (managed && IsCurrentInstance) {
            _ = NativeApi.SetErrorCallback(null);
            NativeApi.Terminate();
        }
        if (IsCurrentInstance) {
            Instance = null;
        }
        IsDisposed = true;
    }

    #endregion
}
