namespace Glekcraft.Graphics.GLFW;

/// <summary>
/// The main entry point into the library.
/// </summary>
public sealed class LibGLFW : IDisposable {
    #region Private Static Fields

    /// <summary>
    /// The current instance.
    /// </summary>
    private static LibGLFW? Instance;

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
        // TODO: Set up temporary error handling
        // TODO: Configure API provider
        if (!apiProvider.Init()) {
            // TODO: Custom exception
            throw new InvalidOperationException("Failed to initialize native library");
        }
        Instance = new(apiProvider);
        // TODO: Instance post-construction initialization
        return Instance;
    }

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
