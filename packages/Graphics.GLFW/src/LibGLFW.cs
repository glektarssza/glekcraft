namespace Glekcraft.Graphics.GLFW;

/// <summary>
/// The main entry point into the library.
/// </summary>
public sealed class LibGLFW : IDisposable {
    #region Public Delegates

    /// <summary>
    /// A delegate for handling errors being surfaced from the native library.
    /// </summary>
    /// <param name="code">
    /// The error code raised by the native library.
    /// </param>
    /// <param name="description">
    /// An optional, human-readable description of the error that occurred.
    /// </param>
    public delegate void HandleNativeError(ErrorCode code, string? description);

    #endregion

    #region Internal Static Properties

    /// <summary>
    /// The current instance if the library is initialized.
    /// </summary>
    internal static LibGLFW? Instance {
        get;
        private set;
    }

    #endregion

    #region Public Static Properties

    /// <summary>
    /// Whether the library is initialized.
    /// </summary>
    public static bool IsInitialized =>
        Instance != null;

    #endregion

    #region Public Static Methods

    /// <summary>
    /// Get the version of the native library being used.
    /// </summary>
    /// <param name="provider">
    /// The object which will provide access to the native library. If not
    /// provided a default API provider will be used.
    /// </param>
    /// <returns>
    /// The version of the native library being used.
    /// </returns>
    public static Version GetNativeVersion(INativeAPIProvider? provider = null) =>
        (provider ?? new DefaultNativeAPIProvider()).GetVersion();

    /// <summary>
    /// Get the version string of the native library being used.
    /// </summary>
    /// <param name="provider">
    /// The object which will provide access to the native library. If not
    /// provided a default API provider will be used.
    /// </param>
    /// <returns>
    /// The version string of the native library being used.
    /// </returns>
    public static string GetNativeVersionString(INativeAPIProvider? provider = null) =>
        (provider ?? new DefaultNativeAPIProvider()).GetVersionString();

    /// <summary>
    /// Initialize the library if it is not already initialized.
    /// </summary>
    /// <param name="provider">
    /// The object which will provide access to the native library. If not
    /// provided a default API provider will be used.
    /// </param>
    /// <returns>
    /// The existing instance if one the library is still initialized; a new
    /// instance otherwise.
    /// </returns>
    public static LibGLFW Initialize(INativeAPIProvider? provider = null) {
        if (Instance != null && !Instance.IsDisposed && Instance.IsCurrentInstance) {
            return Instance;
        }
        var apiProvider = provider ?? new DefaultNativeAPIProvider();
        var errorCode = ErrorCode.NoError;
        string? errorDesc = null;
        apiProvider.SetErrorCallback((code, description) => {
            errorCode = code;
            errorDesc = description;
        });
        // TODO: Configure API provider
        if (!apiProvider.Init()) {
            apiProvider.SetErrorCallback(null);
            throw new GLFWException(errorCode, errorDesc, "Failed to initialize native library");
        }
        Instance = new(apiProvider);
        // TODO: Instance post-construction initialization
        return Instance;
    }

    #endregion

    #region Public Events

    /// <summary>
    /// An event that is emitted when an error occurs in the native library.
    /// </summary>
    public event HandleNativeError? NativeErrorOccurred;

    #endregion

    #region Internal Properties

    /// <summary>
    /// The native API provider object.
    /// </summary>
    internal INativeAPIProvider APIProvider {
        get;
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Whether this instance is the instance currently managing the native
    /// library.
    /// </summary>
    public bool IsCurrentInstance =>
        Instance == this;

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
    /// The object that is going to provide access to the native library.
    /// </param>
    private LibGLFW(INativeAPIProvider apiProvider) =>
        this.APIProvider = apiProvider;

    /// <summary>
    /// The finalizer.
    /// </summary>
    ~LibGLFW() =>
        this.Dispose(false);

    #endregion

    #region Public Methods

    /// <summary>
    /// Dispose of this instance.
    /// </summary>
    public void Dispose() {
        GC.SuppressFinalize(this);
        this.Dispose(true);
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Dispose of this instance.
    /// </summary>
    /// <param name="managed">
    /// Whether this method is being called from managed code or from unmanaged
    /// code (e.g. the garbage collector).
    /// </param>
    private void Dispose(bool managed) {
        if (this.IsDisposed) {
            return;
        }
        if (managed && this.IsCurrentInstance) {
            // TODO: Any other tear down
            this.APIProvider.Terminate();
            Instance = null;
        }
        this.IsDisposed = true;
    }

    #endregion
}
